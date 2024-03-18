namespace Decode;

public class GSensorCollisionAlarm {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _collision;

	// constructors ----------------------------------------------------------------------------- //
	public GSensorCollisionAlarm(byte[] data) {
		this._collision = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	public bool Status => this._collision == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Collision detected" : "No collision detected";

		return $"G-Sensor Collision Alarm: {status}";
	}
}
