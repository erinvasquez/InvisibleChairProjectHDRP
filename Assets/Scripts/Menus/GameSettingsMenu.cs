/// <summary>
/// Game settings part of our settings menu
/// </summary>
public class GameSettingsMenu : SimpleMenu<GameSettingsMenu> {

    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {
        MenuManager.Instance.CloseMenu();
    }

}
