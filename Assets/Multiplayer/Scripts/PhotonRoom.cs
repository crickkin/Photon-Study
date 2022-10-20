using UnityEngine;
using UnityEngine.SceneManagement;  
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom Instance { get; private set; }

    public int multiplayScene = 1;
    public int currentScene;

    private PhotonView _photonView;

    void Awake()
    {
        SingletonInitialize();
    }

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("joined a room");
        if (!PhotonNetwork.IsMasterClient)
            return;

        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Debug.Log("Loading level...");
        PhotonNetwork.LoadLevel(multiplayScene);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;

        if (currentScene == multiplayScene)
        {
            {
                CreatePlayer();
            }
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), 
            transform.position, 
            Quaternion.identity, 
            0);
    }

    private void SingletonInitialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
