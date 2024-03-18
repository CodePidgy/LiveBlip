namespace Decode;

public class GPSLocation {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte[] _latitude;
	private readonly byte[] _longitude;
	private readonly byte[] _speed;
	private readonly byte[] _direction;

	// constructors ----------------------------------------------------------------------------- //
	public GPSLocation(byte[] data) {
		int index = 0;

		this._latitude = data[index..(index + 4)];

		index += 4;

		this._longitude = data[index..(index + 4)];

		index += 4;

		this._speed = data[index..(index + 2)];

		index += 2;

		this._direction = data[index..(index + 2)];
	}

	// properties ------------------------------------------------------------------------------- //
	public double Direction => (double) (this._direction[0] << 8 | this._direction[1]) / 100;

	public double Latitude => (double) (
		this._latitude[0] << 24 |
		this._latitude[1] << 16 |
		this._latitude[2] << 8 |
		this._latitude[3]
	) / 10000000;

	public double Longitude => (double) (
		this._longitude[0] << 24 |
		this._longitude[1] << 16 |
		this._longitude[2] << 8 |
		this._longitude[3]
	) / 10000000;

	public double Speed => (double) (this._speed[0] << 8 | this._speed[1]) / 10;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return (
			$"Lattitude: {this.Latitude}\n" +
			$"Longitude: {this.Longitude}\n" +
			$"Speed: {this.Speed}km/h\n" +
			$"Direction: {this.Direction}°"
		);
	}
}
