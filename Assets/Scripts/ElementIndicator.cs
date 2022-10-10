using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIndicator : WorldToScreenUI
{
    public Sprite[] sprites;

    public Image image;

    public void UpdateElement(Element.Type element)
    {
        if (element == Element.Type.None)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            image.sprite = sprites[Element.ElementToIndex(element)];
        }
    }
}
