using System;
using System.Net;
using System.Net.Sockets;
using Namespace;

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

			// Create a new client object to handle the client's connection
			_ = new Client(client);
		}
	}
}
