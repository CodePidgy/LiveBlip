namespace Decode;

public class BatteryVoltage {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte[] _voltage;

	// constructors ----------------------------------------------------------------------------- //
	public BatteryVoltage(byte[] data) {
		this._voltage = data[..2];
	}

	// properties ------------------------------------------------------------------------------- //
	public double Voltage => (this._voltage[0] << 8 | this._voltage[1]) / 10.0;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return $"Voltage: {this.Voltage}V";
	}
}
