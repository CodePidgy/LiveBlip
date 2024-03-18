namespace Decode;

/// <summary>
/// Represents a G-Sensor Tow Alarm.
/// </summary>
public class GSensorTowAlarm {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _tow;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>GSensorTowAlarm</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the G-sensor tow alarm status.
	/// </param>
	public GSensorTowAlarm(byte[] data) {
		this._tow = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	/// <summary>
	/// Gets the status of the G-Sensor tow alarm.
	/// </summary>
	public bool Status => this._tow == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Tow detected" : "No tow detected";

		return $"G-Sensor Tow Alarm: {status}";
	}
}
