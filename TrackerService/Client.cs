using System;
using System.Net.Sockets;
using System.Threading;

namespace Namespace;

/// <summary>
/// Class to handle a client's connection to the server
/// <param name="device"=>
/// The <c>TcpClient</c> object representing the device's connection
/// </param>
/// </summary>
public class Client {
	// fields ----------------------------------------------------------------------------------- //
	private TcpClient _device;
	private Thread _thread;

	// constructors ----------------------------------------------------------------------------- //
	public Client(TcpClient device) {
		Console.WriteLine($"+ Client connected: {device.Client.RemoteEndPoint}");

		this._device = device;
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
		NetworkStream stream = this._device.GetStream();

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

			Console.WriteLine($"=== {this._device.Client.RemoteEndPoint} ===");

			int messageType = buffer[0];
			int packetIdentifier = buffer[1];
			int payloadLength = buffer[2] | buffer[3];
			byte[] payload = buffer[4..(4 + payloadLength)];
			int crc = buffer[^2] | buffer[^1];
		}
	}

	/// <summary>
	/// Method to handle a client's disconnection from the server.
	/// </summary>
	private void HandleDisconnect() {
		Console.WriteLine("--- Disconnect ---");
	}
}
