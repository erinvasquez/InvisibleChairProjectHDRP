using UnityEngine;

/// <summary>
/// Lessons taken from: https://github.com/YousicianGit/UnityMenuSystem/blob/master/Assets/Scripts/MenuSystem/Menu.cs
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
    /// Open our menu by creating an instance, or using one that already exists
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
    /// Closes our menu if the instance exists
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


public abstract class Menu : MonoBehaviour {

    [Tooltip("Destroy the GameObject when menu is closed reduces memory usage)")]
    public bool DestroyWhenClosed = true;

    [Tooltip("Disable menus that are under this one in the stack")]
    public bool DisableMenusUnderneath = true;

    public abstract void OnBackPressed();

}
