using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRespawn : MonoBehaviour
{
    Damageable playerHealth;

    void Start()
    {
        // Kiểm tra xem màn chơi 1 đã hoàn thành chưa
        if (PlayerPrefs.GetInt("LevelCompleted") == 1)
        {
            // Lấy trạng thái của nhân vật từ màn chơi trước
            int savedHealth = PlayerPrefs.GetInt("Health");
            // Áp dụng trạng thái của nhân vật cho nhân vật trong màn chơi 2
            playerHealth.Health= savedHealth;
        }
    }
}
