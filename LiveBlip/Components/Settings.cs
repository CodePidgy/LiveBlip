namespace LiveBlip.Components;

public class Settings {
	// fields ----------------------------------------------------------------------------------- //
	private bool _darkMode = false;

	// properties ------------------------------------------------------------------------------- //
	public bool DarkMode {
		get => this._darkMode;
		set {
			this._darkMode = value;
		}
	}
}
