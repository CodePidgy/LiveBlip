using System;

namespace Decode.TP302F;

/// <summary>
/// Represents the battery voltage.
/// </summary>
public class BatteryVoltage {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte[] _voltage;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>BatteryVoltage</c> class with the specified
	/// data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the battery voltage data.
	/// </param>
	public BatteryVoltage(byte[] data) {
		this._voltage = data[..2];
	}

	// properties ------------------------------------------------------------------------------- //
	public double Percentage => Math.Round((this.Voltage - 10.2) / 2.2 * 100);

	/// <summary>
	/// Gets the voltage value in volts.
	/// </summary>
	public double Voltage => (this._voltage[0] << 8 | this._voltage[1]) / 10.0;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return $"Voltage: {this.Voltage}V ({this.Percentage}%)";
	}
}
