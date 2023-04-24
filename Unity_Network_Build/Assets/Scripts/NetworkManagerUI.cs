using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
   // we use SerializeField to create private variables visible in the inspector
   [SerializeField] private Button serverBtn, clientBtn, hostBtn, JoinClientBtn, StartServerBtn, StartHost;
   [SerializeField] private TMP_InputField iPInputField, portInputField;

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

      StartServerBtn.onClick.AddListener(() =>
      {
         SetTransportData();
         NetworkManager.Singleton.StartServer();
      });

      JoinClientBtn.onClick.AddListener(() =>
        {
           SetTransportData();
           NetworkManager.Singleton.StartClient();
        });

      StartHost.onClick.AddListener(() =>
        {
           SetTransportData();
           NetworkManager.Singleton.StartHost();
        });
   }

   private void SetTransportData()
   {
      NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = iPInputField.text;
      NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = ushort.Parse(portInputField.text);
   }
}
