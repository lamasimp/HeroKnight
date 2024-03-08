using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealthed;

    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealthed;

    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        //create text at chacrater hit
        UnityEngine.Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tMPText = Instantiate(damageTextPrefab,spawnPosition, UnityEngine.Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tMPText.text = damageReceived.ToString();
    }
    public void CharacterHealthed(GameObject character, int healthRestored)
    {
        UnityEngine.Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tMPText = Instantiate(healthTextPrefab, spawnPosition, UnityEngine.Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tMPText.text = healthRestored.ToString();
    }
    void Update()
    {
        // Kiểm tra nếu người chơi bấm nút Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Gọi phương thức để quay lại màn hình bắt đầu
            BackToMainMenu();
        }
    }
    public void BackToMainMenu()
    {
        // Tải scene MainMenu (màn hình bắt đầu game)
        SceneManager.LoadScene(0);
    }

}
