using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Decode.TP302F;
using TrackerService.Data;

namespace TrackerService;

/// <summary>
/// Represents a client connected to the server.
/// </summary>
public class Client {
	// fields ----------------------------------------------------------------------------------- //
	private readonly TcpClient _client;
	private readonly bool verbose;
	private readonly Thread _thread;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Represents a client connected to the server.
	/// </summary>
	/// <param name="client">
	/// The TCP client representing the connection to the client.
	/// </param>
	public Client(TcpClient client, bool verbose = false) {
		Console.WriteLine($"+ Client connected: {client.Client.RemoteEndPoint}");

		this._client = client;
		this.verbose = verbose;

		// Start a new thread to handle the client's data, so as to not block the main thread
		this._thread = new(this.HandleData);
		this._thread.Start();
	}

	// methods ---------------------------------------------------------------------------------- //
	/// <summary>
	/// Method to receive and handle data from the client, directing it to the correct to the
	/// correct decoding method.
	/// </summary>
	private void HandleData() {
		// Get the network stream (data) sent by the client
		NetworkStream stream = this._client.GetStream();

		// Get the current time to use as the connection time
		DateTime connectionTime = DateTime.Now;

		string imei = "";

		// Loop to read data from the client
		while (true) {
			// Create an array to temporarily store the data
			byte[] data = new byte[1024];

			// Copy the data from the stream to the buffer from the first byte to the last
			// Get how many bytes are in the buffer from the first byte to the last
			int bytesCount = stream.Read(data, 0, data.Length);

			// If the bytes read is 0, the client disconnected, and we can stop reading
			if (bytesCount == 0) {
				Console.WriteLine($"- Client disconnected: {this._client.Client.RemoteEndPoint}");

				return;
			}

			// Create a new packet object from the data received
			Packet packet = new(data);

			try {
				if (imei == "") {
					imei = new LoginRequest(packet.Payload).IMEI;
				}
			} catch (Exception exception) {
				Console.WriteLine(exception.Message);

				return;
			}

			// Create new log files to store the raw and decoded data respectively
			StreamWriter logRaw = new(
				$"logs/{imei} - {connectionTime:yyyy-MM-dd HH-mm-ss}",
				true,
				Encoding.ASCII
			);
			StreamWriter logText = new(
				$"logs/{imei} - {connectionTime:yyyy-MM-dd HH-mm-ss}.log",
				true,
				Encoding.UTF8
			);

			if (this.verbose) {
				Console.WriteLine($"=== {imei} : {this._client.Client.RemoteEndPoint} ===");
			}

			// Switch statement to handle the different message types
			switch (packet.MessageType) {
				case 1: // Login request
					LoginRequest loginRequest = new(packet.Payload);

					logText.WriteLine("--- Login Request ---");
					logText.WriteLine(loginRequest.ToString());

					if (this.verbose) {
						Console.WriteLine("--- Login Request ---");
						Console.WriteLine(loginRequest.ToString());
					}

					break;
				case 3: // Heartbeat request
					logText.WriteLine("--- Heartbeat Request ---");

					if (this.verbose) {
						Console.WriteLine("--- Heartbeat Request ---");
					}

					break;
				case 5: // Record
					int index = 0;
					int recordCount = packet.Payload[index++];
					DateTime timeStamp = DateTime.UnixEpoch.AddSeconds(
						packet.Payload[index++] << 24 |
						packet.Payload[index++] << 16 |
						packet.Payload[index++] << 8 |
						packet.Payload[index++]
					);

					logText.WriteLine("--- Record ---");
					logText.WriteLine($"Time Stamp: {timeStamp}");
					logText.WriteLine($"Record Count: {recordCount}");

					if (this.verbose) {
						Console.WriteLine("--- Record ---");
						Console.WriteLine($"Time Stamp: {timeStamp}");
						Console.WriteLine($"Record Count: {recordCount}");
					}

					for (int _ = 0; _ < recordCount; _++) {
						int recordType = packet.Payload[index++];

						switch (recordType) {
							case 1: // GPS location
								GPSLocation gpsLocation = new(packet.Payload[index..]);

								index += 12;

								logText.WriteLine("--- Record: GPS Location ---");
								logText.WriteLine(gpsLocation.ToString());

								if (this.verbose) {
									Console.WriteLine("--- Record: GPS Location ---");
									Console.WriteLine(gpsLocation.ToString());
								}

								break;
							case 9: // G-sensor collision alarm (unused)
								index++;

								break;
							case 11: // G-sensor tow alarm (unused)
								index++;

								break;
							case 18: // Battery voltage
								BatteryVoltage batteryVoltage = new(packet.Payload[index..]);

								index += 2;

								logText.WriteLine("--- Record: Battery Voltage ---");
								logText.WriteLine(batteryVoltage.ToString());

								if (this.verbose) {
									Console.WriteLine("--- Record: Battery Voltage ---");
									Console.WriteLine(batteryVoltage.ToString());
								}

								break;
							case 86: // LBS state (unused)
								index++;

								break;
							case 87: // CSQ
								CSQ csq = new(packet.Payload[index..]);

								index++;

								logText.WriteLine("--- Record: CSQ ---");
								logText.WriteLine(csq.ToString());

								if (this.verbose) {
									Console.WriteLine("--- Record: CSQ ---");
									Console.WriteLine(csq.ToString());
								}

								break;
							case 97: // Unknown
								// This type is unknown, but going through sample data it seems to
								// be 1 byte long
								index++;

								logText.WriteLine("--- Error ---");
								logText.WriteLine($"Unknown record type: {recordType}");

								if (this.verbose) {
									Console.WriteLine("--- Error ---");
									Console.WriteLine($"Unknown record type: {recordType}");
								}

								break;
							default: // Unknown record type
								logText.WriteLine("--- Error ---");
								logText.WriteLine($"Unknown record type: {recordType}");

								if (this.verbose) {
									Console.WriteLine("--- Error ---");
									Console.WriteLine($"Unknown record type: {recordType}");
								}

								break;
						}
					}

					break;
				default: // Unknown message type
					logText.WriteLine("--- Error ---");
					logText.WriteLine($"Unknown message type: {packet.MessageType}");

					if (this.verbose) {
						Console.WriteLine("--- Error ---");
						Console.WriteLine($"Unknown message type: {packet.MessageType}");
					}

					break;
			}

			// Write the raw data to the raw log file
			logRaw.WriteLine(BitConverter.ToString(packet.Raw()));

			// Close the log files
			logRaw.Close();
			logText.Close();
		}
	}
}
