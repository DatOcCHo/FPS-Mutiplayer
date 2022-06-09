using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreboardManager : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            GetComponent<Canvas>().enabled = true;
        }

        else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}
