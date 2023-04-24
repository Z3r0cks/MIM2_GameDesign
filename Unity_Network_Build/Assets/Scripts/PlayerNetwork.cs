using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// NetcodeBehaviour is a class that inherits from MonoBehaviour
public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> _randomNumber = new NetworkVariable<int>(
        1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveDir.z = 1f; // f = float
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveDir.x = 1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
