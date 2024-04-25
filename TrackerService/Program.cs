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
		// Will use the test_data.txt if true
		bool test = false;

		// Port to listen for connections
		int port = 22368;

		Setup();

		// Create a new TCP listener on the specified port
		TcpListener listener = new(IPAddress.Any, port);
		listener.Start();

		Console.WriteLine($"Listening for TCP connections on port {port}...");

		// Start the test thread if test is true, creating a "fake" device connection
		if (test) {
			new Thread(Test).Start();
		}

		// Loop to accept new clients
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
		// Create the logs directory if it doesn't exist
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
		string[] tests = File.ReadAllLines("test_data.txt");

		// Connect to localhost
		TcpClient client = new("localhost", 22368);

		// Get the stream to send data to the server
		NetworkStream stream = client.GetStream();

		// Send each test data to the server
		foreach (string test in tests) {
			// Split the test data by the dash and convert each part to a byte and send it to the
			// server
			stream.Write(test.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray());

			// Wait for a second before sending the next data to avoid flooding the server
			Thread.Sleep(1000);
		}

		stream.Close();
	}
}
