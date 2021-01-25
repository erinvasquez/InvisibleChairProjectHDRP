/// <summary>
/// A fake menu that is used by our MenuManager for
/// gameplay and pausing
/// </summary>
public class GameMenu : SimpleMenu<GameMenu> {

    public override void OnBackPressed() {
        PauseMenu.Show();
    }

}
