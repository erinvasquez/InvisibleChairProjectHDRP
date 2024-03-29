﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Unity Network Client Game Manager,
/// 
/// Should this manage Game we're in? [GAME01, GAME02, etc]     YES
/// Should this game manage the players we're connected to?     MAYBE
/// Should this manage parties?                                 IF ABOVE IS TRUE
/// Should this manage chat? [Game, Party, Group, World]        MAYBE
/// 
/// Manages our player prefabs.                                 
/// 
/// Assuming this won't be destroyed between scenes,
/// this script...
/// 
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// Our GameManager instance.
    /// 
    /// Should we set this to DoNotDestroy?     YES FOR NOW
    /// 
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// A dictionary of players in our client's game, including the client
    /// </summary>
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    //public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    //public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    //public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();

    /// <summary>
    /// A local single player
    /// </summary>
    public GameObject localPlayerPrefab;
    /// <summary>
    /// Players OTHER than the local single player
    /// </summary>
    public GameObject playerPrefab;
    //public GameObject itemSpawnerPrefab;
    //public GameObject projectilePrefab;
    //public GameObject enemyPrefab;

    public int gameNumber = 0; // By default, 0 is the main menu, 1 is "GAME_01"
    // We can think of another way of doing this later, like with a type or its own script

    private void Awake() {
        if (instance == null) {
            instance = this;

            // Keep this game manager around, we'll need it
            DontDestroyOnLoad(gameObject);

        } else if (instance != this) {
            Debug.Log("GameManager instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>
    /// 
    /// Spawns either the client's player, or another player gameobject.
    /// Instantiate and initialize us ID and Username, adding to our dictionary of players
    /// </summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation) {
        GameObject _player;
        if (_id == Client.instance.myId) {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        } else {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    /*
    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem) {
        GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());
    }

    public void SpawnProjectile(int _id, Vector3 _position)
    {
        GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }
    
    public void SpawnEnemy(int _id, Vector3 _position)
    {
        GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
        _enemy.GetComponent<EnemyManager>().Initialize(_id);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
    }
    */

}
