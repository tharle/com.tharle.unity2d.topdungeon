using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public Player player;

    // Logic
    public int pesos;
    public int experience;

    private void Awake()
    {
        if (GameManager.instance != null) // We need just one GameManager
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState; // All time will save the game when Load one Scene
        DontDestroyOnLoad(gameObject);
    }

    public void ShowText(string message, int fontSize, Color color,  Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the Weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) return false;

        // has the PESOS enough? 
        if (pesos < weaponPrices[weapon.weaponLevel]) return false;

        pesos -= weaponPrices[weapon.weaponLevel];
        weapon.UpgradeWeapon();
        return true;
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int level = 0;
        int xpPerLevel = 0;

        while(experience >= xpPerLevel && level <= xpTable.Count)
        {
            xpPerLevel += xpTable[level];
            level++;
        }

        return level;
    }

    // Total XP for get a level
    public int GetXpToLevel(int level)
    {
        int levelInteration = 0;
        int xp = 0;

        while(levelInteration < level) xp += xpTable[levelInteration++];

        return xp;

    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        int nextLevel = GetCurrentLevel();
        if (currLevel < nextLevel) 
        {
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        int currLevel = GetCurrentLevel();
        Debug.Log($"You level up to {currLevel}");
        ShowText($"You level up to {currLevel}", 35, Color.cyan, player.transform.position, Vector3.up * 40, 2f);
        player.OnLevelUp();


    }

    // Save game
    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string save = "";
        save +=  "0" + "|";
        save += pesos.ToString() + "|";
        save += experience.ToString() + "|";
        save += weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("SaveState", save);

        Debug.Log("Save with sucess!");
    }

    // Load Game
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //TODO change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.SetWeapon(int.Parse(data[3]));
        player.SetLevel(GetCurrentLevel());

        player.ToSpawnPoint();

        Debug.Log("The state was loaded!");
    }
}
