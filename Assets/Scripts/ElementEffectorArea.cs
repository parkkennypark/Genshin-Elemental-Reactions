using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffectorArea : MonoBehaviour
{
    public Element element;

    public int damage;

    private void Start()
    {
        Destroy(gameObject, 1);
    }

    public void Setup(Element.Type elementType)
    {
        element.SetType(elementType);
        // this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.GetComponentInParent<Element>())
        // {
        //     other.GetComponentInParent<Element>().SetType(element.GetType());
        // }

        if (other.GetComponentInParent<Enemy>())
        {
            other.GetComponentInParent<Enemy>().TakeDamage(element.GetType(), damage, false);
        }
    }
}
