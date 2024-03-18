namespace Decode;

/// <summary>
/// Represents the state of the LBS (Location-Based Service).
/// </summary>
public class LBSState {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _lbs;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>LBSState</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the LBS data.
	/// </param>
	public LBSState(byte[] data) {
		this._lbs = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	/// <summary>
	/// Gets the status of the LBS state.
	/// </summary>
	public bool Status => this._lbs == 1;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		string status = this.Status ? "Location found" : "Location lost";

		return $"LBS: {status}";
	}
}
