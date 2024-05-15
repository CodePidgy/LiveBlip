using System.Net;
using System.Net.Sockets;

namespace TrackerService;

/// <summary>
/// Represents the controller for the application.
/// </summary>
public class Controller {
	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Represents the controller for the application.
	/// </summary>
	/// <param name="verbose">
	/// Whether to print connection information.
	/// </param>
	public Controller(bool verbose) {
		// Port to listen for connections
		int port = 22368;

		// Create a new TCP listener on the specified port
		TcpListener listener = new(IPAddress.Any, port);
		listener.Start();

		// Loop to accept new clients
		while (true) {
			// Accept a new client when they attempt to connect
			TcpClient client = listener.AcceptTcpClient();

			// Create a new client object to handle the client's connection
			_ = new Client(client, verbose);
		}
	}
}
