using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    Damageable playerHealth;
    public void LevelComplete()
    {
        // Lưu trạng thái của nhân vật và màn chơi
        PlayerPrefs.SetInt("Health", playerHealth.Health);
        PlayerPrefs.SetInt("LevelCompleted", 1);
        PlayerPrefs.Save(); // Lưu lại dữ liệu
    }
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
