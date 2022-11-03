using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private void Awake()
    {
        if(GameManager.instance != null) // We need just one GameManager
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState; // All time will save the game when Load one Scene
        DontDestroyOnLoad(gameObject);
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager FloatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    public void ShowText(string message, int fontSize, Color color,  Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextManager.Show(message, fontSize, color, position, motion, duration);
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

        Debug.Log("The state was loaded!");
    }
}
