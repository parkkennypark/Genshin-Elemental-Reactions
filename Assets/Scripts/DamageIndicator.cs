using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : WorldToScreenUI
{
    public Color[] elementColors;

    public void SetText(Element.Type element, int damage, bool isCrit)
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = damage.ToString();
        text.color = elementColors[Element.ElementToIndex(element)];

        if (isCrit)
        {
            transform.localScale = Vector3.one * 1.5f;
        }
    }
}
