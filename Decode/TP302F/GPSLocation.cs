using System;

namespace Decode.TP302F;

/// <summary>
/// Represents a GPS location with latitude, longitude, speed, and direction.
/// </summary>
public class GPSLocation {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte[] _latitude;
	private readonly byte[] _longitude;
	private readonly byte[] _speed;
	private readonly byte[] _direction;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>GPSLocation</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the GPS location data.
	/// </param>
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
	/// <summary>
	/// Gets the direction in degrees.
	/// </summary>
	public double Direction => (double) (this._direction[0] << 8 | this._direction[1]) / 100;

	/// <summary>
	/// Gets the latitude value in degrees.
	/// </summary>
	public double Latitude {
		get {
			// For some reason, the bytes sent don't follow typical two's compliment. Instead of
			// 1111 1111 1111 1111 being -1, 1000 0000 0000 0001 is -1. So we need to check the
			// first bit and then flip it back to 0
			if ((this._latitude[0] & (1 << 7)) != 0) {
				return (double) (
					(this._latitude[0] & 0b01111111) << 24 | // Flip the sign bit back to 0
					this._latitude[1] << 16 |
					this._latitude[2] << 8 |
					this._latitude[3]
				) / -10000000;
			} else {
				return (double) (
					this._latitude[0] << 24 |
					this._latitude[1] << 16 |
					this._latitude[2] << 8 |
					this._latitude[3]
				) / 10000000;
			}
		}
	}

	/// <summary>
	/// Gets the longitude value in degrees.
	/// </summary>
	public double Longitude {
		get {
			// For some reason, the bytes sent don't follow typical two's compliment. Instead of
			// 1111 1111 1111 1111 being -1, 1000 0000 0000 0001 is -1. So we need to check the
			// first bit and then flip it back to 0
			if ((this._longitude[0] & (1 << 7)) != 0) {
				return (double) (
					(this._longitude[0] & 0b01111111) << 24 | // Flip the sign bit back to 0
					this._longitude[1] << 16 |
					this._longitude[2] << 8 |
					this._longitude[3]
				) / -10000000;
			} else {
				return (double) (
					this._longitude[0] << 24 |
					this._longitude[1] << 16 |
					this._longitude[2] << 8 |
					this._longitude[3]
				) / 10000000;
			}
		}
	}

	/// <summary>
	/// Gets the speed in kilometers per hour.
	/// </summary>
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
