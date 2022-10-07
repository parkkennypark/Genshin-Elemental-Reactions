using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float fireForce;
    public float gravityForce;
    public Transform explosionPrefab;
    public Transform[] particlePrefabs;


    private Rigidbody rb;
    private Element element;

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

    public void Fire()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * fireForce);
    }

    void OnTriggerEnter(Collider other)
    {
        Transform explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Transform explosion2 = Instantiate(particlePrefabs[element.GetTypeEnum()], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
