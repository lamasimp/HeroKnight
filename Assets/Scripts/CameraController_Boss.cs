using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Boss : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -31.54f, 4.86f), 
            Mathf.Clamp(player.position.y, -2.4f, 3.6f), transform.position.z);
    }
}
