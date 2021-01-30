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
    public Button comboRewardButton;

    public int comboRewardChance; //in %
    

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
        comboRewardButton.onClick.AddListener(UpgradeCombo);
        UpdateStats();

    }

    void UpdateStats()
    {
        statPointAvailableText.text = statsFile.loadedSave.statPoints.ToString();
        currHealthText.text = statsFile.loadedSave.maxHealth.ToString();
        attackPowerText.text = statsFile.loadedSave.attackDamage.ToString();
        movementSpeedText.text = statsFile.loadedSave.movementSpeed.ToString();
        if(Random.Range(0, 100) < comboRewardChance && statsFile.loadedSave.currentMaxCombo < 3)
        {
            comboRewardButton.gameObject.SetActive(true);
        } else
        {
            comboRewardButton.gameObject.SetActive(false);
        }
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

    public void UpgradeCombo()
    {
        statsFile.loadedSave.currentMaxCombo += 1;
        statsFile.loadedSave.statPoints -= 1;
        UpdateStats();
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
