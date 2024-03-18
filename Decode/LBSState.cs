namespace Decode;

public class LBSState {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _lbs;

	// constructors ----------------------------------------------------------------------------- //
	public LBSState(byte[] data) {
		this._lbs = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	public bool Status => this._lbs == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Location found" : "Location lost";

		return $"LBS: {status}";
	}
}
