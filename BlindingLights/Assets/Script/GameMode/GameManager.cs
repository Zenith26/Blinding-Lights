using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // to call gamemanager in other script

    [SerializeField] GameObject PlayerPawn = null; // used for the player to spawned

    GameObject Player = null; // once player is spawned, will set to this player

    public GameObject GetPlayer() { return Player; } // get the player from the WaveSpawner

    [SerializeField] Transform PlayerStart = null;

    private void Awake()
    {
        // Singleton
        // if there is no instance, set this one as the instance
        if(Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // will not destroy this gameObject when loading new scene
        } else if(Instance != null){ // is there is actually an instance. Destroy that one
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        GameObject _Player;

        //player
        _Player = Instantiate(PlayerPawn, PlayerStart.position, PlayerStart.rotation);

        Player = _Player;
    }

    public void Death()
    {
        // THIS IS FOR PLAYER WHEN THEY DIE
    }
}
