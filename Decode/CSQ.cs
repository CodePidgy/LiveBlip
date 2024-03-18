﻿namespace Decode;

public class CSQ {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _csq;

	// constructors ----------------------------------------------------------------------------- //
	public CSQ(byte[] data) {
		this._csq = data[0];
	}

	// properties ------------------------------------------------------------------------------- //
	public int Strength => (62 / 31) * this._csq - 113;

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
