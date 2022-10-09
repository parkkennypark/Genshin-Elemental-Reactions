using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIndicator : WorldToScreenUI
{
    public Sprite[] sprites;

    public void UpdateElement(Element.Type element)
    {
        GetComponentInChildren<Image>().sprite = sprites[Element.ElementToIndex(element)];
    }
}
