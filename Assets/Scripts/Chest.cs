using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChestSprite;
    public int pesosAmount = 5;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            string txtPesosAmount = pesosAmount > 1 ? "pesos" : "peso";
            GameManager.instance.pesos += pesosAmount;
            Debug.Log($"Grant {pesosAmount} {txtPesosAmount}!");
        }
    }

}
