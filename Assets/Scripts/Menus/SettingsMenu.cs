/// <summary>
/// Gives our SettingsMenu buttons their functionality
/// </summary>
public class SettingsMenu : SimpleMenu<SettingsMenu> {

    protected override void Awake() {
        base.Awake();
    }

    /// <summary>
    /// Start us off on the first menu, the Game settings menu
    /// </summary>
    private void Start() {
        GameSettingsMenu.Show();
    }

    public void OnGameSettingsPressed() {

        // Only hide the menu if there is an instance
        if (AudioSettingsMenu.Instance != null)
            AudioSettingsMenu.Hide();

        if (VideoSettingsMenu.Instance != null)
            VideoSettingsMenu.Hide();

        // Only RE-show the menu if it's already destroyed
        if (GameSettingsMenu.Instance == null)
            GameSettingsMenu.Show();
    }

    public void OnAudioSettingsPressed() {

        // Only hide the menu if there is an instance
        if (GameSettingsMenu.Instance != null)
            GameSettingsMenu.Hide();

        if (VideoSettingsMenu.Instance != null)
            VideoSettingsMenu.Hide();

        // Only RE-show the menu if it's already destroyed
        if (AudioSettingsMenu.Instance == null) {
            AudioSettingsMenu.Show();

        }



    }

    public void OnVideoSettingsPressed() {

        // Only hide the menu if there is an instance
        if (GameSettingsMenu.Instance != null)
            GameSettingsMenu.Hide();

        if (AudioSettingsMenu.Instance != null)
            AudioSettingsMenu.Hide();

        // Only RE-show the menu if it's already destroyed
        if (VideoSettingsMenu.Instance == null)
            VideoSettingsMenu.Show();
    }

    public override void OnBackPressed() {

        // Only hide the menu if there is an instance
        if (GameSettingsMenu.Instance != null)
            GameSettingsMenu.Hide();

        if (VideoSettingsMenu.Instance != null)
            VideoSettingsMenu.Hide();

        if (AudioSettingsMenu.Instance != null)
            AudioSettingsMenu.Hide();

        MenuManager.Instance.CloseMenu();

    }

}
