using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    // we use SerializeField to create private variables visible in the inspector
    [SerializeField]
    private Button serverBtn;

    [SerializeField]
    private Button clientBtn;

    [SerializeField]
    private Button hostBtn;

    // TMP input field
    [SerializeField]
    private TMP_InputField iPInputField;

    [SerializeField]
    private TMP_InputField portInputField;

    [SerializeField]
    private Button JoinBtn;

    private void Awake()
    {
        // we add listeners to the buttons
        serverBtn.onClick.AddListener(() =>
        {
            // Singleton design pattern
            NetworkManager.Singleton.StartServer();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        JoinBtn.onClick.AddListener(() =>
        {
            string ip = iPInputField.text;
            string port = portInputField.text;
            Debug.Log($"Joining IP: {ip}, Port: {port}");
            // NetworkManager.Singleton.ConnectedHostname.
        });
    }
}
