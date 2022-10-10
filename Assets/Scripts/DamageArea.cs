using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public Element element;
    public int damage;
    public float knockbackRatio = 2;

    private void Start()
    {
        Destroy(gameObject, 0.2f);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Enemy>())
        {
            Vector3 dir = other.transform.position - transform.position;
            other.GetComponentInParent<Enemy>().TakeDamage(element.GetType(), damage, false, dir, knockbackRatio);
        }
    }
}
