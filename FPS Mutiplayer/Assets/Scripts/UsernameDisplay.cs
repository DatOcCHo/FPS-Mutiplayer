using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.text = pv.Owner.NickName;
    }
}
