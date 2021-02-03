/// <summary>
/// Menu class that implements paramaterless Show and Hide Methods
/// Lessons taken from https://github.com/YousicianGit/UnityMenuSystem/blob/master/Assets/Scripts/MenuSystem/SimpleMenu.cs
/// </summary>
public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T> {

    /// <summary>
    /// Opens the menu
    /// </summary>
    public static void Show() {
        Open();
    }

    /// <summary>
    /// Closes the menu
    /// </summary>
    public static void Hide() {
        Close();
    }

}
