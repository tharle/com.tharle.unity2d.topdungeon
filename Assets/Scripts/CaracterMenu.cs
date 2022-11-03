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
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = "NAN";

        // XP BAR
        xpText.text = "NAN";
        xpBar.localScale = new Vector3(.5f, xpBar.localScale.y, xpBar.localScale.z);

    }
}
