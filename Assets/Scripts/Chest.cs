using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChestSprite;
    public int pesosAmount = 5;

    private Color coinColor = Color.yellow;
    private int fontSize = 25;
    private Vector3 motion = Vector3.up * 50; // that will be 50px per sec in UP direction
    private float duration = .8f; // in sec
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            string txtPesosAmount = pesosAmount > 1 ? "pesos" : "peso";
            GameManager.instance.pesos += pesosAmount;
            string txt = $"+{pesosAmount} {txtPesosAmount}!";
            GameManager.instance.ShowText(txt, fontSize, coinColor, transform.position, motion, duration);
            Debug.Log(txt);
        }
    }

}
