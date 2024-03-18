namespace Decode;

public class LoginRequest {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _majorVersion;
	private readonly byte _minorVersion;
	private readonly byte _imeiLength;
	private readonly string _imei;
	private readonly byte _modelLength;
	private readonly string _model;
	private readonly byte _firmwareVersionLength;
	private readonly string _firmwareVersion;
	private readonly byte _passwordLength;
	private readonly string _password;

	// constructors ----------------------------------------------------------------------------- //
	public LoginRequest(byte[] data) {
		int index = 0;

		this._majorVersion = data[index++];
		this._minorVersion = data[index++];
		this._imeiLength = data[index++];
		this._imei = System.Text.Encoding.ASCII.GetString(data, index, this._imeiLength);

		index += this._imeiLength;

		this._modelLength = data[index++];
		this._model = System.Text.Encoding.ASCII.GetString(data, index, this._modelLength);

		index += this._modelLength;

		this._firmwareVersionLength = data[index++];
		this._firmwareVersion = System.Text.Encoding.ASCII.GetString(data, index, this._firmwareVersionLength);

		index += this._firmwareVersionLength;

		this._passwordLength = data[index++];
		this._password = System.Text.Encoding.ASCII.GetString(data, index, this._passwordLength);
	}

	// properties ------------------------------------------------------------------------------- //
	public string FirmwareVersion => this._firmwareVersion;

	public int FirmwareVersionLength => this._firmwareVersionLength;

	public string IMEI => this._imei;

	public int IMEILength => this._imeiLength;

	public int MajorVersion => this._majorVersion;

	public int MinorVersion => this._minorVersion;

	public string Model => this._model;

	public string Password => this._password;

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
