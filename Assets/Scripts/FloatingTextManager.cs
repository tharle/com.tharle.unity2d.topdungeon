using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>(); // pool of txt floatings

    private void Update()
    {
        floatingTexts.ForEach(floatingText => floatingText.UpdateFloatingText());
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = message;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        
        floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position); // transfer WORLD space to SCREEN space so we can use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = floatingTexts.Find(t => !t.active);

        if(floatingText == null)
        {
            floatingText = new FloatingText();
            floatingText.gameObject = Instantiate(textPrefab);
            floatingText.gameObject.transform.SetParent(textContainer.transform);
            floatingText.text = floatingText.gameObject.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(floatingText);
        }

        return floatingText;
    }
}
