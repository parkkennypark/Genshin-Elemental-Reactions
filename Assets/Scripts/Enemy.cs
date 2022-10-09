using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Enemy : MonoBehaviour
{
    private Element element;

    public GameObject elementIndicatorPrefab;

    private ElementIndicator elementIndicator;

    void Start()
    {
        element = GetComponent<Element>();
        element.changed_type.AddListener(OnElementChanged);

        elementIndicator = Instantiate(elementIndicatorPrefab).GetComponent<ElementIndicator>();
        elementIndicator.Setup(transform, Vector3.zero);
        elementIndicator.UpdateElement(element.GetType());
    }

    void OnElementChanged()
    {
        elementIndicator.UpdateElement(element.GetType());
    }

    void Update()
    {

    }
}