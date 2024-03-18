namespace Decode;

public class GSensorTowAlarm {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _tow;

	// constructors ----------------------------------------------------------------------------- //
	public GSensorTowAlarm(byte[] data) {
		this._tow = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	public bool Status => this._tow == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Tow detected" : "No tow detected";

		return $"G-Sensor Tow Alarm: {status}";
	}
}
