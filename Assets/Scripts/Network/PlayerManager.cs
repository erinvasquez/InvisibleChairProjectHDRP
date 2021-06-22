using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player values in a game
/// </summary>
public class PlayerManager : MonoBehaviour {

    /// <summary>
    /// Number ID for current player?
    /// </summary>
    public int id;

    /// <summary>
    /// Preffered username the player goes by
    /// </summary>
    public string username;

    public MeshRenderer model;
    public GameObject modelGameObject;

    public void Initialize(int _id, string _username) {
        id = _id;
        username = _username;
        //health = maxHealth;
    }

}
