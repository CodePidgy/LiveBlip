using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Namespace;

namespace TrackerService;

public class Program {
	public static void Main(string[] args) {
		bool test = false;

		int port = 22368;

		TcpListener listener = new(IPAddress.Any, port);

		listener.Start();

		Console.WriteLine($"Listening for TCP connections on port {port}...");

		if (test) {
			new Thread(Test).Start();
		}

		while (true) {
			// Accept a new client when they attempt to connect
			TcpClient client = listener.AcceptTcpClient();

			// Create a new client object to handle the client's connection
			_ = new Client(client);
		}
	}

	// methods ---------------------------------------------------------------------------------- //
	private static void Test() {
		// Get test bytes from text file
		string[] tests = System.IO.File.ReadAllLines("test_data.txt");
		// Connect to localhost
		TcpClient client = new("localhost", 22368);
		// Get the stream to send data to the server
		NetworkStream stream = client.GetStream();

		for (int i = 0; i < 10; i++) {
			stream.Write(tests[i].Split('-').Select(x => Convert.ToByte(x, 16)).ToArray());

			Thread.Sleep(1000);
		}

		stream.Close();
	}
}
