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
    //public float health;
    //public float maxHealth = 100f;
    //public int itemCount = 0;
    public MeshRenderer model;

    public void Initialize(int _id, string _username) {
        id = _id;
        username = _username;
        //health = maxHealth;
    }

    /*
    public void SetHealth(float _health) {
        health = _health;

        if (health <= 0f) {
            Die();
        }
    }

    public void Die() {
        model.enabled = false;
    }

    public void Respawn() {
        model.enabled = true;
        SetHealth(maxHealth);
    }
    
    */

}
