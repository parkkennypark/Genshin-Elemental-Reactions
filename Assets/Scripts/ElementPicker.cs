using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementPicker : MonoBehaviour
{
    public Element bowElement;
    public Transform elementPickerUIParent;

    public RectTransform currentElementUI;


    void Start()
    {
        PickElement(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bowElement.type = Element.Type.None;
            PickElement(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bowElement.type = Element.Type.Pyro;
            PickElement(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bowElement.type = Element.Type.Hydro;
            PickElement(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            bowElement.type = Element.Type.Electro;
            PickElement(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            bowElement.type = Element.Type.Cryo;
            PickElement(4);
        }
    }

    void PickElement(int index)
    {
        RectTransform elementUI = elementPickerUIParent.GetChild(index).GetComponent<RectTransform>();
        if (elementUI == currentElementUI)
        {
            return;
        }

        if (currentElementUI)
        {
            StartCoroutine(SetElementUI(currentElementUI, false));
        }

        StartCoroutine(SetElementUI(elementUI, true));
        currentElementUI = elementUI;
    }

    IEnumerator SetElementUI(RectTransform elementUI, bool selected)
    {
        Vector2 target = selected ? Vector2.one * 150 : Vector2.one * 100;
        while (elementUI.sizeDelta != target)
        {
            elementUI.sizeDelta = Vector2.MoveTowards(elementUI.sizeDelta, target, Time.deltaTime * 1000f);
            yield return null;
        }
    }
}
