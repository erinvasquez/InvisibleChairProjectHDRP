﻿using UnityEngine;

/// <summary>
/// Lessons taken from: https://github.com/YousicianGit/UnityMenuSystem/blob/master/Assets/Scripts/MenuSystem/Menu.cs
/// 
/// Base Menu Abstract Class that defines some default methodds
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Menu<T> : Menu where T : Menu<T> {


    public static T Instance { get; private set; }

    /// <summary>
    /// Set this as our instance
    /// </summary>
    protected virtual void Awake() {
        Instance = (T)this;
    }

    protected virtual void OnDestroy() {
        Instance = null;
    }

    /// <summary>
    /// Called by our MenuManager in Awake()
    /// Creating a MenuManager instance if there isn't one already,
    /// and use it to Open this menu
    /// </summary>
    protected static void Open() {
        if (Instance == null) {
            MenuManager.Instance.CreateInstance<T>();
        } else {
            Instance.gameObject.SetActive(true);
        }

        MenuManager.Instance.OpenMenu(Instance);

    }

    /// <summary>
    /// Closes our menu, but only if our instance isn't null
    /// </summary>
    protected static void Close() {
        if (Instance == null) {
            Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
            return;
        }

        MenuManager.Instance.CloseMenu(Instance);
    }

    /// <summary>
    /// Close the menu if ESC/BACK is pressed
    /// </summary>
    public override void OnBackPressed() {
        Close();
    }

}

/// <summary>
/// Menu as a MonoBehaviour GameObject
/// </summary>
public abstract class Menu : MonoBehaviour {

    [Tooltip("Destroying the GameObject when the menu is closed reduces memory usage)")]
    public bool DestroyWhenClosed = true;

    [Tooltip("Disable menus that are under this one in the stack")]
    public bool DisableMenusUnderneath = true;

    public abstract void OnBackPressed();

}
