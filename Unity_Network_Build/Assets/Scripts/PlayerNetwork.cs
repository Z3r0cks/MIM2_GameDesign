using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// NetcodeBehaviour is a class that inherits from MonoBehaviour
public class PlayerNetwork : NetworkBehaviour
{
   private Material _cubeMaterial;
   // private NetworkVariable<Color> _syncedColor;
   private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(
          1,
          NetworkVariableReadPermission.Everyone,
          NetworkVariableWritePermission.Owner
      );

   private void Start()
   {
      // var _syncedColor = new NetworkVariable<Color>(
      //       new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
      //       NetworkVariableReadPermission.Everyone,
      //       NetworkVariableWritePermission.Owner
      //    );

      _cubeMaterial = GetComponentInChildren<MeshRenderer>().material;
      SetMaterialColor();
   }
   private void Update()
   {
      // Debug.Log(_randomNumber);
      if (!IsOwner)
         return;

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
      // _cubeMaterial.SetColor("_Color", _syncedColor.Value);
   }

   private Color GetRandomColor()
   {
      return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
   }


   private void SetMaterialColor()
   {
      // _cubeMaterial = GetComponent<MeshRenderer>().material;
      Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

      // Materialfarbe ändern
      _cubeMaterial.SetColor("_Color", randomColor);
   }
}



// // NetcodeBehaviour is a class that inherits from MonoBehaviour
// public class PlayerNetwork : NetworkBehaviour
// {
//    private Material _cubeMaterial;
//    private NetworkVariable<Color> _syncedColor;
//    private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(
//        1,
//        NetworkVariableReadPermission.Everyone,
//        NetworkVariableWritePermission.Owner
//    );

//    private void Start()
//    {
//       _cubeMaterial = GetComponentInChildren<MeshRenderer>().material;
//       base.OnNetworkDespawn();
//       _syncedColor = new NetworkVariable<Color>(
//          new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
//          NetworkVariableReadPermission.Everyone,
//          NetworkVariableWritePermission.Owner
//       );
//       SetMaterialColor();
//    }

//    private void Update()
//    {
//       if (!IsOwner)
//          return;

//       // modefier NetworkVariable
//       if (Input.GetKey(KeyCode.T))
//          _randomNumber.Value = Random.Range(0, 100);

//       // simple movement for owner
//       Vector3 moveDir = new Vector3(0, 0, 0);
//       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveDir.z = 1f; // f = float
//       if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveDir.z = -1f;
//       if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDir.x = -1f;
//       if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDir.x = 1f;
//       if (Input.GetKey(KeyCode.Space))

//       {
//          // respawn cube from Cubespawner
//          GameObject cube = GameObject.Find("CubeSpawner");
//       }

//       float moveSpeed = 3f;
//       transform.position += moveDir * moveSpeed * Time.deltaTime;
//       _cubeMaterial.SetColor("_Color", _syncedColor.Value);
//    }

//    private Color GetRandomColor()
//    {
//       return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
//    }

//    private void SetMaterialColor()
//    {
//       // _cubeMaterial = GetComponent<MeshRenderer>().material;
//       Color randomColor = GetComponentInParent<Color>();

//       // Materialfarbe ändern
//       _cubeMaterial.SetColor("_Color", randomColor);
//    }
// }
