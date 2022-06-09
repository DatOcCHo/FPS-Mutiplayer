using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageGame : MonoBehaviour
{
    public static MessageGame Instance;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
        gameObject.SetActive(false);
    }

    public static void Message(string message)
    {
        Instance.text.text = message;
        Instance.gameObject.SetActive(true);
    }

    public static void TurnOff()
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("OffMessage", 2f);
    }

    private void OffMessage()
    {
        Instance.gameObject.SetActive(false);
    }
}