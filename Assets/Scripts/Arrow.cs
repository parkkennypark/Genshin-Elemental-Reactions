using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Arrow : MonoBehaviour
{
    public float fireForce;
    public float gravityForce;
    public Transform explosionPrefab;
    public Transform[] particlePrefabs;
    public Element element;

    private Rigidbody rb;
    private float chargeRatio;

    private bool collided;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        element = GetComponent<Element>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.forward = rb.velocity;

        rb.AddForce(Vector3.down * gravityForce * Time.deltaTime);
    }

    public void Fire(float chargeRatio)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fireForce);

        this.chargeRatio = chargeRatio;
    }

    void OnTriggerEnter(Collider other)
    {
        if (collided)
            return;

        collided = true;
        rb.isKinematic = true;


        int elementIndex = Element.ElementToIndex(element.GetType());
        Transform elementEffector = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        elementEffector.GetComponent<ElementEffectorArea>().Setup(element.GetType(), chargeRatio, transform.forward);

        Transform particles = Instantiate(particlePrefabs[elementIndex], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
