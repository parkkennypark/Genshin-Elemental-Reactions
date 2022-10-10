using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffectorArea : MonoBehaviour
{
    public Element element;

    public int damage;

    private float chargeRatio;
    private Vector3 dir;

    private void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    public void Setup(Element.Type elementType, float chargeRatio, Vector3 dir)
    {
        this.chargeRatio = chargeRatio;
        this.dir = dir;

        element.SetType(elementType);

        // this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Enemy>())
        {
            // Vector3 dir = other.transform.position - transform.position;
            other.GetComponentInParent<Enemy>().TakeDamage(element.GetType(), damage, false, dir, chargeRatio);
        }
    }
}
