namespace LiveBlip.Components;

public class Settings {
	// fields ----------------------------------------------------------------------------------- //
	private bool _darkMode = false;
	private bool _isUserLoggedIn = false;

	// properties ------------------------------------------------------------------------------- //
	public bool DarkMode {
		get => this._darkMode;
		set {
			this._darkMode = value;
		}
	}
	public bool isUserLoggedIn {
		get=>this._isUserLoggedIn;
		set {this._isUserLoggedIn = value;}

	}
}
