namespace LiveBlip.Components;

public class Settings {
	// fields ----------------------------------------------------------------------------------- //
	private bool _darkMode = false;
	private string _user = string.Empty;

	// properties ------------------------------------------------------------------------------- //
	public bool DarkMode {
		get => this._darkMode;
		set { this._darkMode = value; }
	}
	public string User {
		get => this._user;
		set { this._user = value; }
	}
}
