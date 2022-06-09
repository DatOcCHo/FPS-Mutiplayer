using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView pV;
    GameObject controller;
    private void Awake()
    {
        pV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (pV.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation,0, new object[] { pV.ViewID});
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        Invoke("CreateController", 5f);
    }
}
