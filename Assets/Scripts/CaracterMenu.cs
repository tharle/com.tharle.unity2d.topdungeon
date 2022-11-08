using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaracterMenu : MonoBehaviour
{
    // Text Fields
    public TextMeshProUGUI levelText, hitpointText, pesosText, upgradeCoastText, xpText;

    //Logic Fields
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite, WeaponSprite;
    public RectTransform xpBar;

    // Caracter Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            // If we went too far way
            if (currentCharacterSelection >= GameManager.instance.playerSprites.Count) currentCharacterSelection = 0;

        }
        else
        {
            currentCharacterSelection--;
            // If we went too far way
            if (currentCharacterSelection < 0) currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeWeaponClick()
    {
        if (GameManager.instance.TryUpgradeWeapon()) UpdateMenu();
    }

    // Update the character Information
    public void UpdateMenu()
    {
        //Weapon
        WeaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        if (GameManager.instance.weapon.weaponLevel >= GameManager.instance.weaponPrices.Count) upgradeCoastText.text = "MAX";
        else upgradeCoastText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        // Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + " / " + GameManager.instance.player.maxHitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        var level = GameManager.instance.GetCurrentLevel();
        levelText.text = level.ToString();

        // XP BAR
        float completionRatio;
        string xpDescription;
        if (level == GameManager.instance.xpTable.Count)
        {
            xpDescription = GameManager.instance.experience.ToString() + " total experience points";
            completionRatio = 1f;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(level - 1);
            int xpNextLevel = GameManager.instance.xpTable[level - 1];

            int currLevelXp = GameManager.instance.experience - prevLevelXp;
            completionRatio = (float)currLevelXp / (float)xpNextLevel;
            xpDescription = currLevelXp.ToString() + " / " + xpNextLevel.ToString();
        }
        xpBar.localScale = new Vector3(completionRatio, xpBar.localScale.y, xpBar.localScale.z);
        xpText.text = xpDescription;

    }
}
