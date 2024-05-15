using System.IO;

namespace TrackerService;

/// <summary>
/// Represents the entry point of the application.
/// </summary>
public class Program {
	public static void Main(string[] args) {
		// Will print the connection info if true
		bool verbose = false;

		// Will use the test_data.txt if true
		bool test = false;

		// The number of times to run the test
		int tests = 1000;

		// Parse the command line arguments
		foreach (string arg in args) {
			switch (arg[..2]) {
				case "-v":
					verbose = true;

					break;
				case "-t":
					test = true;

					break;
			}
		}

		Setup();

		if (test) {
			_ = new Test(tests);
		} else {
			_ = new Controller(verbose);
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
}
