using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TrackerService;

/// <summary>
/// Represents a test class for spawning clients and sending test data.
/// </summary>
public class Test {
	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Represents a test class for spawning clients and sending test data.
	/// </summary>
	/// <param name="tests">
	/// The number of times to run the test.
	/// </param>
	public Test(int tests) {
		List<Task> tasks = [];

		// Read the test data from the file
		string[] testData = File.ReadAllLines("test_data.txt");

		for (int i = 0; i < tests; i++) {
			// Copy the test data to a new array to avoid modifying the original
			string[] data = new string[testData.Length];
			testData.CopyTo(data, 0);

			string[] imei = new string[15];

			// Get the number of digits in the current number
			int digits = i.ToString().ToCharArray().Length;

			// Loop through the imei array, starting from the end
			for (int j = 14; j >= 0; j--) {
				// The current index is less than the imei length minus the number of digits. We do
				// this check so that we can assign the rightmost digit to the rightmost index
				if (j > 14 - digits) {
					// Assigns the rightmost digit to the rightmost index
					imei[j] = $"3{i.ToString()[(digits - 1) - (14 - j)]}";
				} else {
					// Assigns 30, which is the equivalent of 0 in this case
					imei[j] = "30";
				}
			}

			// Replace all the imei placeholders in the test data with the generated IMEI
			for (int j = 0; j < data.Length; j++) {
				data[j] = data[j].Replace(
					"38-36-38-34-34-30-30-36-39-34-37-37-32-33-39",
					string.Join("-", imei)
				);
			}

			// Add the SpawnClient method as a runnable task to the list
			tasks.Add(Task.Run(() => SpawnClient(data)));
		}

		// Create a task that will complete when all the tasks in the list are completed
		Task task = Task.WhenAll(tasks);

		// Wait for the task to complete, meaning all the tasks in the list have completed
		task.Wait();

		if (task.IsFaulted) {
			Console.WriteLine("Test failed");
		} else {
			Console.WriteLine("Test completed");
		}
	}

	// methods ---------------------------------------------------------------------------------- //
	/// <summary>
	/// Spawns a client and send the test data to the server.
	/// </summary>
	/// <param name="data">
	/// The test data to send to the server.
	/// </param>
	private static Task SpawnClient(string[] data) {
		TcpClient client = new("213.219.36.37", 22368);
		NetworkStream stream = client.GetStream();

		foreach (string line in data) {
			stream.Write(line.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray());

			Task.Delay(250).Wait();
		}

		stream.Close();
		client.Close();

		return Task.CompletedTask;
	}
}
