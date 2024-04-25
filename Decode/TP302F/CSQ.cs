namespace Decode.TP302F;

/// <summary>
/// Represents the CSQ (Cell Signal Quality) class that calculates the signal strength and status
/// based on the CSQ value.
/// </summary>
public class CSQ {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _csq;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>CSQ</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the CSQ data.
	/// </param>
	public CSQ(byte[] data) {
		this._csq = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	/// <summary>
	/// Gets the strength value calculated based on the CSQ value.
	/// </summary>
	public int Strength => (62 / 31) * this._csq - 113;

	/// <summary>
	/// Gets the status of the signal strength based on the CSQ value.
	/// </summary>
	public string Status {
		get {
			if (this._csq >= 0 && this._csq <= 9) {
				return "Marginal";
			} else if (this._csq >= 10 && this._csq <= 14) {
				return "OK";
			} else if (this._csq >= 15 && this._csq <= 19) {
				return "Good";
			} else if (this._csq >= 20 && this._csq <= 30) {
				return "Excellent";
			} else {
				return "No signal";
			}
		}
	}

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return $"CSQ: {this.Strength}dBm ({this.Status})";
	}
}
