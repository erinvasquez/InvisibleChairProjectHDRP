using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Selection menu
/// How does multiplayer change this menu?
/// </summary>
public class GameSelectionMenu : SimpleMenu<GameSelectionMenu> {
    
    public MenuManager menuManager;
    public Button game_01Button;
    public Button game_02Button;
    public Button game_03Button;



    protected override void Awake() {
        // SUPER IMPORTANT line
        base.Awake();

        

    }


    /// <summary>
    /// 
    /// </summary>
    private void Start() {

        menuManager = transform.parent.GetComponent<MenuManager>();
        game_01Button = GameObject.Find("GAME_01 Button").GetComponent<Button>();
        game_02Button = GameObject.Find("GAME_02 Button").GetComponent<Button>();
        game_03Button = GameObject.Find("GAME_03 Button").GetComponent<Button>();

    }

    public void OnGame01Pressed() {

        SceneManager.LoadScene("Game_01");

    }

    public void OnGame02Pressed() {

        SceneManager.LoadScene("Game_02");

    }

    public void OnGame03Pressed() {

        SceneManager.LoadScene("Game_03");

    }


    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {

        // Currently, this only closes the top menu
        // I want to be able to close the entire settings menu, not just the submenu
        // Either close two menus or find another workaround

        MenuManager.Instance.CloseMenu();
    }



}
