using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// NetcodeBehaviour is a class that inherits from MonoBehaviour
public class PlayerNetwork : NetworkBehaviour
{
   private Material _cubeMaterial;
   private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(
          1,
          NetworkVariableReadPermission.Everyone,
          NetworkVariableWritePermission.Owner
      );

   private NetworkVariable<float> syncedRed = new NetworkVariable<float>(0f);
   private NetworkVariable<float> syncedGreen = new NetworkVariable<float>(0f);
   private NetworkVariable<float> syncedBlue = new NetworkVariable<float>(0f);

   public override void OnNetworkSpawn()
   {
      base.OnNetworkSpawn();
      _cubeMaterial = GetComponentInChildren<MeshRenderer>().material;
      SetRandomColor();
   }

   void SetRandomColor()
   {
      // Zufällige Farbe erzeugen
      syncedRed.Value = Random.Range(0f, 1f);
      syncedGreen.Value = Random.Range(0f, 1f);
      syncedBlue.Value = Random.Range(0f, 1f);
   }

   private void Update()
   {
      // modefier NetworkVariable
      if (Input.GetKey(KeyCode.T))
         _randomNumber.Value = Random.Range(0, 100);

      // simple movement for owner
      Vector3 moveDir = new Vector3(0, 0, 0);
      if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveDir.z = 1f; // f = float
      if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveDir.z = -1f;
      if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDir.x = -1f;
      if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDir.x = 1f;
      if (Input.GetKey(KeyCode.Space))
      {
         // respawn cube from Cubespawner
         GameObject cube = GameObject.Find("CubeSpawner");
      }

      float moveSpeed = 3f;
      transform.position += moveDir * moveSpeed * Time.deltaTime;
      _cubeMaterial.SetColor("_Color", new Color(syncedRed.Value, syncedGreen.Value, syncedBlue.Value));
   }

   private Color GetRandomColor()
   {
      return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
   }
}