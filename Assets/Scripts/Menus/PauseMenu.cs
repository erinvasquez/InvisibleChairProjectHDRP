/// <summary>
/// Attached to a "manager" GameObject; in charge of Pause menu
/// </summary>
public class PauseMenu : SimpleMenu<PauseMenu> {

    public void OnQuitPressed() {
        Hide();
        Destroy(this.gameObject);

        GameMenu.Hide(); // I'm guessing gamemenu is fake "menu" used for when you're playing the game
    }


}
