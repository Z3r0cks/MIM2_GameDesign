using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// NetcodeBehaviour is a class that inherits from MonoBehaviour
public class PlayerNetwork : NetworkBehaviour
{
   [SerializeField] private NetworkObject _cubePrefab;
   private bool _cubeSpawned = false;
   private float _timeSinceLastCube = 0f;
   private float _timeToReset = 1f;

   private Material _cubeMaterial;
   private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(
          1,
          NetworkVariableReadPermission.Everyone,
          NetworkVariableWritePermission.Owner
      );

   private NetworkVariable<Color> _syncedColor = new NetworkVariable<Color>(
      new Color(0, 0, 0),
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
   );

   public override void OnNetworkSpawn()
   {
      base.OnNetworkSpawn();
      _cubeMaterial = GetComponentInChildren<MeshRenderer>().material;
      if (IsOwner)
      {
         SetRandomColor();
      }

      _syncedColor.OnValueChanged += (x, y) =>
      {
         UpdateMaterialColor(y);
      };
   }

   void UpdateMaterialColor(Color newColor)
   {
      _cubeMaterial.SetColor("_Color", newColor);
   }

   void SetRandomColor()
   {
      // Zufällige Farbe erzeugen
      _syncedColor.Value = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
   }

   private void Update()
   {
      // modifier NetworkVariable
      if (Input.GetKey(KeyCode.T))
         _randomNumber.Value = Random.Range(0, 100);

      // simple movement for owner
      Vector3 moveDir = new Vector3(0, 0, 0);
      if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveDir.z = 1f; // f = float
      if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveDir.z = -1f;
      if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDir.x = -1f;
      if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDir.x = 1f;
      // if (Input.GetKey(KeyCode.Space) && !_cubeSpawned)
      // {
      //    SpawnCube();
      // }
      if (_cubeSpawned)
      {
         _timeSinceLastCube += Time.deltaTime;
         if (_timeSinceLastCube >= _timeToReset)
         {
            _cubeSpawned = false;
            _timeSinceLastCube = 0f;
         }
      }

      float moveSpeed = 3f;
      transform.position += moveDir * moveSpeed * Time.deltaTime;
      _cubeMaterial.SetColor("_Color", new Color(_syncedColor.Value.r, _syncedColor.Value.g, _syncedColor.Value.b));
   }

   // public void SpawnCube()
   // {
   //    // spawn new cube
   //    if (IsOwner)
   //    {
   //       Vector3 spawnPos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.0f, 5.0f), Random.Range(-5.0f, 5.0f));
   //       Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
   //       NetworkObject spawnedCube = NetworkObject.Instantiate(_cubePrefab, spawnPos, spawnRotation);
   //       _cubeSpawned = true;
   //    }
   // }
}
