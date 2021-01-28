﻿using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Manages our menu flow
/// Using lessons taken from: https://github.com/YousicianGit/UnityMenuSystem/blob/master/Assets/Scripts/MenuSystem/MenuManager.cs
/// https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/wiki/Controls/MenuSystem#markdown-header-external-links
/// </summary>
public class MenuManager : MonoBehaviour {

    public MainMenu mainMenuPrefab;
    public GameMenu gameMenuPrefab;
    public PauseMenu pauseMenuPrefab;
    public SettingsMenu settingsMenuPrefab;
    public GameSettingsMenu gameSettingsMenuPrefab;
    public AudioSettingsMenu audioSettingsMenuPrefab;
    public VideoSettingsMenu videoSettingsMenuPrefab;

    private Stack<Menu> MenuStack = new Stack<Menu>();

    public static MenuManager Instance { get; private set; }

    /// <summary>
    /// Keep this is our singleton and start us off on the MainMenu
    /// </summary>
    private void Awake() {

        Instance = this;

        MainMenu.Show();

    }

    /// <summary>
    /// Instantiates our Menus from their respective prefab
    /// </summary>
    /// <typeparam name="T">Menu class type</typeparam>
    public void CreateInstance<T>() where T : Menu {
        var prefab = GetPrefab<T>();

        Instantiate(prefab, transform);
    }

    /// <summary>
    /// When destroyed, make the instance null
    /// </summary>
    private void OnDestroy() {
        Instance = null;
    }

    /// <summary>
    /// Open a menu and add it to our stack
    /// </summary>
    /// <param name="instance">Our menu instance</param>
    public void OpenMenu(Menu instance) {

        // Deactivate top menu
        if (MenuStack.Count > 0) {

            if (instance.DisableMenusUnderneath) {

                foreach (var menu in MenuStack) {
                    menu.gameObject.SetActive(false);

                    if (menu.DisableMenusUnderneath)
                        break;
                }

            }

            var topCanvas = instance.GetComponent<Canvas>();
            var previousCanvas = MenuStack.Peek().GetComponent<Canvas>();
            topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;

        }

        MenuStack.Push(instance);

    }

    /// <summary>
    /// Closes the current menu and remove it from the stack
    /// </summary>
    public void CloseMenu() {
        var instance = MenuStack.Pop();
        Destroy(instance.gameObject);

        // Re-activate top mneu
        if (MenuStack.Count > 0) {
            MenuStack.Peek().gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Get Prefab dynamically, based on public fields set from Unity
    /// Private fields with SerializeField attribute is also an option
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T GetPrefab<T>() where T : Menu {
        var fields = this.GetType().GetFields(BindingFlags.Public |
            BindingFlags.Instance | BindingFlags.DeclaredOnly);

        // WE NEED TO AVOID FOREACH AS MUCH AS POSSIBLE
        foreach (var field in fields) {
            var prefab = field.GetValue(this) as T;

            if (prefab != null) {
                return prefab;
            }

        }

        if (typeof(T) == typeof(MainMenu))
            return mainMenuPrefab as T;

        if (typeof(T) == typeof(PauseMenu))
            return pauseMenuPrefab as T;

        if (typeof(T) == typeof(SettingsMenu))
            return settingsMenuPrefab as T;

        if (typeof(T) == typeof(GameSettingsMenu))
            return gameSettingsMenuPrefab as T;

        if (typeof(T) == typeof(AudioSettingsMenu))
            return audioSettingsMenuPrefab as T;

        if (typeof(T) == typeof(VideoSettingsMenu))
            return videoSettingsMenuPrefab as T;




        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }

    /// <summary>
    /// Close the selected menu
    /// </summary>
    /// <param name="menu">Our menu object</param>
    public void CloseMenu(Menu menu) {

        if (MenuStack.Count == 0) {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", menu.GetType());
            return;
        }

        if (MenuStack.Peek() != menu) {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", menu.GetType());
            return;
        }

        CloseTopMenu();
    }

    /// <summary>
    /// Closes menu at the top of our stack
    /// </summary>
    public void CloseTopMenu() {
        var instance = MenuStack.Pop();

        if (instance.DestroyWhenClosed)
            Destroy(instance.gameObject);
        else
            instance.gameObject.SetActive(false);

        // Reactivate top menu
        // If reactivated menu is an overlay, we need to activate the menu under it
        // AVOID USING FOREACH
        foreach (var menu in MenuStack) {
            menu.gameObject.SetActive(true);

            if (menu.DisableMenusUnderneath)
                break;

        }
    }

    /// <summary>
    /// When ESCAPE/PAUSE/BACK is pressed, do the appropriate action
    /// </summary>
    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) && MenuStack.Count > 0) {
            MenuStack.Peek().OnBackPressed();
        }

    }

}
