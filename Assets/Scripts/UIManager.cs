using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

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
    
}
