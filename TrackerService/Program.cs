using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TrackerService;

public class Program {
	public static void Main(string[] args) {
		int port = 22368;

		TcpListener listener = new(IPAddress.Any, port);

		listener.Start();

		Console.WriteLine($"Listening for TCP connections on port {port}...");

		while (true) {
			// Accept a new client when they attempt to connect
			TcpClient client = listener.AcceptTcpClient();

			Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

			// Create a new thread to handle the client
			Thread thread = new(() => HandleClient(client));

			thread.Start();
		}
	}

	private static void HandleClient(TcpClient client) {
		// Get the network stream (data) sent by the client
		NetworkStream stream = client.GetStream();

		while (true) {
			// Create a buffer to temporarily store the data
			byte[] buffer = new byte[1024];
			// Get how many bytes are in the buffer from the first byte to the last
			int bytesCount = stream.Read(buffer, 0, buffer.Length);

			// If the bytes read is 0, the client disconnected, and we can stop reading
			if (bytesCount == 0) {
				Console.WriteLine("Client disconnected");

				break;
			}

			// Convert the buffer to a string, from the first byte to the last
			string data = Encoding.UTF8.GetString(buffer, 0, bytesCount);

			Console.WriteLine($"({client.Client.RemoteEndPoint}) Received: {data}");
		}
	}
}
