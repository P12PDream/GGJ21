using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideoutManager : MonoBehaviour
{
    // Start is called before the first frame update
    SaveFile statsFile;

    public Text statPointAvailableText;
    public Text currHealthText;
    public Text attackPowerText;
    public Text movementSpeedText;

    

    public GameObject upgradePanel;
    public Button healthButton;
    public Button movementButton;
    public Button attackPowerButton;

    void Start()
    {
        statsFile = GetComponent<SaveFile>();

        statPointAvailableText.text = statsFile.loadedSave.statPoints.ToString();
        currHealthText.text = statsFile.loadedSave.maxHealth.ToString();
        attackPowerText.text = statsFile.loadedSave.attackDamage.ToString();
        movementSpeedText.text = statsFile.loadedSave.movementSpeed.ToString();

        healthButton.onClick.AddListener(UpgradeHealth);
        movementButton.onClick.AddListener(UpgradeMovementSpeed);
        attackPowerButton.onClick.AddListener(UpgradeAttackPower);
        UpdateStats();

    }

    void UpdateStats()
    {
        statPointAvailableText.text = statsFile.loadedSave.statPoints.ToString();
        currHealthText.text = statsFile.loadedSave.maxHealth.ToString();
        attackPowerText.text = statsFile.loadedSave.attackDamage.ToString();
        movementSpeedText.text = statsFile.loadedSave.movementSpeed.ToString();
        if(statsFile.loadedSave.statPoints == 0)
        {
            upgradePanel.SetActive(false);
        } else
        {
            upgradePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeAttackPower()
    {
        statsFile.loadedSave.attackDamage += 1;
        statsFile.loadedSave.statPoints -= 1;
        UpdateStats();
    }


    public void UpgradeMovementSpeed()
    {
        statsFile.loadedSave.movementSpeed += 1;
        statsFile.loadedSave.statPoints -= 1;
        UpdateStats();
    }

    public void UpgradeHealth()
    {
        statsFile.loadedSave.maxHealth += 20;
        statsFile.loadedSave.statPoints -= 1;
        UpdateStats();
    }
}
