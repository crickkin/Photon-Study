using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    [HideInInspector] public GameObject myAvatar;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.Instance.spawnPoints.Length);

        if (photonView.IsMine)
        {
            var playerTransform = GameSetup.Instance.spawnPoints[spawnPicker];
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), playerTransform.position, playerTransform.rotation);
        }
    }

    void Update()
    {
        
    }
}
