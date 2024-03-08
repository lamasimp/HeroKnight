using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Mathf.Clamp(player.position.x, -78.7f, 92.8f), Mathf.Clamp(player.position.y, -30.34f, 2.5f),transform.position.z);
    }
}
