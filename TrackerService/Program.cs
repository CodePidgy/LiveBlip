using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TrackerService;

/// <summary>
/// Represents the entry point of the application.
/// </summary>
public class Program {
	public static void Main(string[] args) {
		bool test = false;
		int port = 22368;

		Setup();

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
	/// <summary>
	/// Sets up the necessary directories for the application.
	/// </summary>
	private static void Setup() {
		if (!Directory.Exists("logs")) {
			Directory.CreateDirectory("logs");
		}
	}

	/// <summary>
	/// This method is used to perform test operations.
	/// It reads test data from the test file, connects to the server, and sends the data to the
	/// server.
	/// </summary>
	private static void Test() {
		// Get test bytes from text file
		string[] tests = System.IO.File.ReadAllLines("test_data.txt");
		// Connect to localhost
		TcpClient client = new("localhost", 22368);
		// Get the stream to send data to the server
		NetworkStream stream = client.GetStream();

		foreach (string test in tests) {
			stream.Write(test.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray());

			Thread.Sleep(50);
		}

		stream.Close();
	}
}
