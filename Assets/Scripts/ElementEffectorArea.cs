using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffectorArea : MonoBehaviour
{
    public Element element;

    private void Start()
    {
        Destroy(gameObject, 1);
    }

    public void SetElement(Element.Type elementType)
    {
        // print(elementType);
        element.SetType(elementType);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Element>())
        {
            // print(other + " " + element.GetType());

            other.GetComponentInParent<Element>().SetType(element.GetType());
        }
    }
}
