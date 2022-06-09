using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject loading;
    public GameObject menu;
    public TMP_InputField y_name;
    public TMP_InputField r_name;

    private void Start()
    {
        loading.SetActive(true);
        menu.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loading.SetActive(false);
        menu.SetActive(true);
        y_name.text = PlayerPrefs.GetString("player");
    }

    public void JoinRoom()
    {
        r_name.text = r_name.text.Trim();
        y_name.text = y_name.text.Trim();
        if (string.IsNullOrWhiteSpace(r_name.text))
        {
            MessageGame.Message("Ten phong khong duoc de trong");
            return;
        }
        if (y_name.text.Length >= 17 || y_name.text.Length <= 3)
        {
            MessageGame.Message("Ten khong hop le, phai co tu 3 den 17 ki tu");
            return;
        }
        SaveNamePlayer();
        PhotonNetwork.JoinRoom(r_name.text);
        loading.SetActive(true);
        menu.SetActive(false);
    }
    public void JoinRandomRoom()
    {
        y_name.text = y_name.text.Trim();
        if (y_name.text.Length >= 17 || y_name.text.Length <= 3)
        {
            MessageGame.Message("Ten khong hop le, phai co tu 3 den 17 ki tu");
            return;
        }
        SaveNamePlayer();
        PhotonNetwork.JoinRandomRoom();
        loading.SetActive(true);
        menu.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Arena");
        MessageGame.Message("Joined to room: " + PhotonNetwork.CurrentRoom);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MessageGame.Message(message);
        loading.SetActive(false);
        menu.SetActive(true);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        MessageGame.Message(message);
        loading.SetActive(false);
        menu.SetActive(true);
    }

    public void CreateRoom()
    {
        r_name.text = r_name.text.Trim();
        y_name.text = y_name.text.Trim();
        if (string.IsNullOrWhiteSpace(r_name.text))
        {
            MessageGame.Message("Ten phong khong hop le");
            return;
        }
        if (y_name.text.Length >= 17 || y_name.text.Length <= 3)
        {
            MessageGame.Message("Ten khong hop le, phai co tu 3 den 17 ki tu");
            return;
        }
        SaveNamePlayer();
        PhotonNetwork.CreateRoom(r_name.text, new RoomOptions { MaxPlayers = 16 });
        loading.SetActive(true);
        menu.SetActive(false);
    }
    public override void OnCreatedRoom()
    {
        MessageGame.Message("Created Room" + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MessageGame.Message(message);
        loading.SetActive(false);
        menu.SetActive(true);
    }

    public void SaveNamePlayer()
    {
        PhotonNetwork.NickName = y_name.text;
        PlayerPrefs.SetString("player", y_name.text);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}