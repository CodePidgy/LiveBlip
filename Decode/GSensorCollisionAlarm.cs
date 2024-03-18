namespace Decode;

/// <summary>
/// Represents the G-Sensor Collision Alarm.
/// </summary>
public class GSensorCollisionAlarm {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _collision;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>GSensorCollisionAlarm</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the G-sensor collision alarm status.
	/// </param>
	public GSensorCollisionAlarm(byte[] data) {
		this._collision = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	/// <summary>
	/// Gets the status of the collision alarm.
	/// </summary>
	public bool Status => this._collision == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Collision detected" : "No collision detected";

		return $"G-Sensor Collision Alarm: {status}";
	}
}
