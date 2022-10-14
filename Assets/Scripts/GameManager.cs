using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // Weapon...

    // Logic
    public int pesos;
    public int experience;

    // Save game
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    // Load Game
    public void LoadState()
    {
        Debug.Log("LoadState");
    }
}
