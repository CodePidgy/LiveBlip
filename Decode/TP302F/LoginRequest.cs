namespace Decode.TP302F;

/// <summary>
/// Represents a login request.
/// </summary>
public class LoginRequest {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _majorVersion;
	private readonly byte _minorVersion;
	private readonly byte _imeiLength;
	private readonly byte[] _imei;
	private readonly byte _modelLength;
	private readonly byte[] _model;
	private readonly byte _firmwareVersionLength;
	private readonly byte[] _firmwareVersion;
	private readonly byte _passwordLength;
	private readonly byte[] _password;

	// constructors ----------------------------------------------------------------------------- //
	/// <summary>
	/// Initializes a new instance of the <c>LoginRequest</c> class with the specified data.
	/// </summary>
	/// <param name="data">
	/// The byte array containing the login request data.
	/// </param>
	public LoginRequest(byte[] data) {
		int index = 0;

		this._majorVersion = data[index++];
		this._minorVersion = data[index++];
		this._imeiLength = data[index++];
		this._imei = data[index..(index + this._imeiLength)];

		index += this._imeiLength;

		this._modelLength = data[index++];
		this._model = data[index..(index + this._modelLength)];

		index += this._modelLength;

		this._firmwareVersionLength = data[index++];
		this._firmwareVersion = data[index..(index + this._firmwareVersionLength)];

		index += this._firmwareVersionLength;

		this._passwordLength = data[index++];
		this._password = data[index..(index + this._passwordLength)];
	}

	// properties ------------------------------------------------------------------------------- //
	/// <summary>
	/// Gets the firmware version.
	/// </summary>
	public string FirmwareVersion => System.Text.Encoding.ASCII.GetString(this._firmwareVersion);

	/// <summary>
	/// Gets the length of the firmware version.
	/// </summary>
	public int FirmwareVersionLength => this._firmwareVersionLength;

	/// <summary>
	/// Gets the IMEI.
	/// </summary>
	public string IMEI => System.Text.Encoding.ASCII.GetString(this._imei);

	/// <summary>
	/// Gets the length of the IMEI.
	/// </summary>
	public int IMEILength => this._imeiLength;

	/// <summary>
	/// Gets the major version.
	/// </summary>
	public int MajorVersion => this._majorVersion;

	/// <summary>
	/// Gets the minor version.
	/// </summary>
	public int MinorVersion => this._minorVersion;

	/// <summary>
	/// Gets the model.
	/// </summary>
	public string Model => System.Text.Encoding.ASCII.GetString(this._model);

	/// <summary>
	/// Gets the password.
	/// </summary>
	public string Password => System.Text.Encoding.ASCII.GetString(this._password);

	/// <summary>
	/// Gets the length of the password.
	/// </summary>
	public int PasswordLength => this._passwordLength;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return (
			$"Major Version: {this.MajorVersion}\n" +
			$"Minor Version: {this.MinorVersion}\n" +
			$"IMEI: {this.IMEI}\n" +
			$"Model: {this.Model}\n" +
			$"Firmware Version: {this.FirmwareVersion}\n" +
			$"Password: {this.Password}"
		);
	}
}
