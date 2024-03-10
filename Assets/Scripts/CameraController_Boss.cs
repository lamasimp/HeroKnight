using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Boss : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, 135.45f, 168f), 
            Mathf.Clamp(player.position.y, 1.39f, 5.85f), transform.position.z);
    }
}
