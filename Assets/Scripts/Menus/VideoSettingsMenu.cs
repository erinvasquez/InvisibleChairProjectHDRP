/// <summary>
/// Video settings part of settings menu
/// </summary>
public class VideoSettingsMenu : SimpleMenu<VideoSettingsMenu> {

    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {
        MenuManager.Instance.CloseMenu();
    }

}
