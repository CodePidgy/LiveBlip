using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Namespace;

/// <summary>
/// Class to handle a client's connection to the server
/// <param name="client"=>
/// The <c>TcpClient</c> object representing the client's connection
/// </param>
/// </summary>
public class Client {
	// fields ----------------------------------------------------------------------------------- //
	private TcpClient _client;
	private Thread _thread;

	// constructors ----------------------------------------------------------------------------- //
	public Client(TcpClient client) {
		Console.WriteLine($"+ Client connected: {client.Client.RemoteEndPoint}");

		this._client = client;
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

		while (true) {
			// Create a buffer to temporarily store the data
			byte[] buffer = new byte[1024];

			// Copy the data from the stream to the buffer from the first byte to the last
			// Get how many bytes are in the buffer from the first byte to the last
			int bytesCount = stream.Read(buffer, 0, buffer.Length);

			// If the bytes read is 0, the client disconnected, and we can stop reading
			if (bytesCount == 0) {
				this.HandleDisconnect();

				return;
			}

			Console.WriteLine($"=== {this._client.Client.RemoteEndPoint} ===");

			int messageType = buffer[0];
			int packetIdentifier = buffer[1];
			int payloadLength = buffer[2] | buffer[3];
			byte[] payload = buffer[4..(4 + payloadLength)];
			int crc = buffer[^2] | buffer[^1];

			// Direct payload based on message type
			switch (messageType) {
				case 1: // Login request
					this.HandleLoginRequest(payload);

					break;
				case 5: // Record
					this.HandleRecord(payload);

					break;
				default: // Unknown message type
					Console.WriteLine("--- Error ---");
					Console.WriteLine($"Unknown message type: {messageType}");

					break;
			}
		}
	}

	/// <summary>
	/// Method to handle a client's disconnection from the server.
	/// </summary>
	private void HandleDisconnect() {
		Console.WriteLine("--- Disconnect ---");
	}

	/// <summary>
	/// Method to handle a login request from the client.
	/// </summary>
	/// <param name="payload"></param>
	private void HandleLoginRequest(byte[] payload) {
		Console.WriteLine("--- Login Request ---");

		int index = 0;
		int majorVersion = payload[index++];
		int minorVersion = payload[index++];
		int imeiLength = payload[index++];
		string imei = Encoding.ASCII.GetString(payload, index, imeiLength);

		index += imeiLength;

		int modelLength = payload[index++];
		string model = Encoding.ASCII.GetString(payload, index, modelLength);

		index += modelLength;

		int firmwareVersionLength = payload[index++];
		string firmwareVersion = Encoding.ASCII.GetString(payload, index, firmwareVersionLength);

		index += firmwareVersionLength;

		int passwordLength = payload[index++];
		string password = Encoding.ASCII.GetString(payload, index, passwordLength);

		Console.WriteLine($"Major Version: {majorVersion}");
		Console.WriteLine($"Minor Version: {minorVersion}");
		Console.WriteLine($"IMIE Length: {imeiLength}");
		Console.WriteLine($"IMEI: {imei}");
		Console.WriteLine($"Model Length: {modelLength}");
		Console.WriteLine($"Model: {model}");
		Console.WriteLine($"Firmware Version Length: {firmwareVersionLength}");
		Console.WriteLine($"Firmware Version: {firmwareVersion}");
		Console.WriteLine($"Password Length: {passwordLength}");
		Console.WriteLine($"Password: {password}");
	}

	private void HandleRecord(byte[] payload) {
		Console.WriteLine("--- Record ---");

		int index = 0;
		int recordCount = payload[index++];

		Console.WriteLine($"Record Count: {recordCount}");

		DateTime timeStamp = DateTime.UnixEpoch.AddSeconds(
				payload[index++] << 24 |
				payload[index++] << 16 |
				payload[index++] << 8 |
				payload[index++]
			);

		Console.WriteLine($"Time Stamp: {timeStamp}");

		for (int _ = 0; _ < recordCount; _++) {
			int recordType = payload[index++];

			Console.WriteLine($"--- Record Type: {recordType}");

			switch (recordType) {
				case 1: // GPS location
					double lattitude = (
						(double) (
							payload[index++] << 24 |
							payload[index++] << 16 |
							payload[index++] << 8 |
							payload[index++]
						)
					) / 10000000;
					double longitude =  (
						(double) (
							payload[index++] << 24 |
							payload[index++] << 16 |
							payload[index++] << 8 |
							payload[index++]
						)
					) / 10000000;
					double speed = (
						(double) (payload[index++] << 8 | payload[index++])
					) / 10;
					double direction = (
						(double) (payload[index++] << 8 | payload[index++])
					) / 100;

					Console.WriteLine($"Lattitude: {lattitude}");
					Console.WriteLine($"Longitude: {longitude}");
					Console.WriteLine($"Speed: {speed}km/h");
					Console.WriteLine($"Direction: {direction}°");

					break;
				default:
					Console.WriteLine("--- Error ---");
					Console.WriteLine($"Unknown record type: {recordType}");
					Console.WriteLine($"Payload: {BitConverter.ToString(payload)}");

					return;
			}
		}
	}
}
