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
            //DontDestroyOnLoad(gameObject); // will not destroy this gameObject when loading new scene
        } else if(Instance != null){ // is there is actually an instance. Destroy that one
            Destroy(gameObject);
        }

        PlayerSpawn(); // had to call awake, because AIBase can't get Player references
    }

    private void Start()
    {
        
    }

    void PlayerSpawn()
    {
        GameObject _Player;

        //player
        _Player = Instantiate(PlayerPawn, PlayerStart.position, PlayerStart.rotation);

        Player = _Player;
    }

    // THIS IS FOR PLAYER WHEN THEY DIE. Called from PlayerHealth script
    public void Death()
    {
        //Remove Camera from the player
        Transform cameraRemove = Player.transform.Find("Main Camera");
        cameraRemove.parent = null;

        // Get MainCamera references
        GameObject mainCamera = cameraRemove.gameObject;

        // Enable the camera components to show the scene
        Camera camera = mainCamera.GetComponent<Camera>();
        camera.enabled = true;

        // Disable the Orbit Cam script from the main camera so that our mouse got the control
        ThirdPersonOrbitCamBasic OrbitCam = mainCamera.GetComponent<ThirdPersonOrbitCamBasic>();
        OrbitCam.enabled = false;
    }
}
