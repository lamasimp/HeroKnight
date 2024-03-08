using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    Damageable playerHealth;
    
    public void LoadNextLevel()
    {
        
        // Lấy index của scene hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Tải scene tiếp theo trong danh sách scene đã build
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadNextLevel();
    }
}
