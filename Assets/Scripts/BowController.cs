using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class BowController : MonoBehaviour
{
    public enum State
    {
        None,
        Charging,
        Charged
    }

    public LayerMask rayMask;
    public float timeToFullCharge = 2;
    public Transform arrowPrefab;
    public Camera camera;
    public Transform chargeIndicator;
    public Transform tip;
    public GameObject fullyChargedParticlesPrefab;
    public GameObject chargedParticles;
    public float baseFOV = 60;
    public float aimingFOV = 50;
    public float charingFOVChange = 5;
    public float fullChargeFOVChange = 8;

    private State state;

    private float timeCharging;
    private float targetFOV;
    private Element element;

    void Start()
    {
        element = GetComponent<Element>();

        ChangeState(State.None);
    }

    void Update()
    {
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * 15f);
        if (state == State.None)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(State.Charging);
                timeCharging = 0;
            }
        }
        else if (state == State.Charging)
        {
            timeCharging += Time.deltaTime;

            chargeIndicator.localScale = Vector3.one * (1 - GetChargeRatio());
            targetFOV = aimingFOV - GetChargeRatio() * charingFOVChange;

            if (timeCharging >= timeToFullCharge)
            {
                ChangeState(State.Charged);
                Transform particles = Instantiate(fullyChargedParticlesPrefab, tip.position, tip.rotation, tip).transform;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                Fire(GetChargeRatio());
                ChangeState(State.None);
            }
        }
        else
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Fire(GetChargeRatio());
                ChangeState(State.None);
            }
        }
    }

    void ChangeState(State newState)
    {
        state = newState;

        chargeIndicator.gameObject.SetActive(state != State.None);

        if (state == State.None)
        {
            targetFOV = baseFOV;
            chargedParticles.SetActive(false);
        }
        else if (state == State.Charged)
        {
            targetFOV = aimingFOV - fullChargeFOVChange;
            chargedParticles.SetActive(true);

        }
    }

    void Fire(float chargeRatio)
    {
        Vector3 target = camera.transform.forward * 200;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 200, rayMask))
        {
            target = hit.point;
        }

        Vector3 dir = target - transform.position;

        Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation).GetComponent<Arrow>();
        arrow.transform.LookAt(target, Vector3.up);

        arrow.element.SetType(element.GetType());
        arrow.Fire(chargeRatio);
    }

    public State GetState()
    {
        return state;
    }

    float GetChargeRatio()
    {
        return timeCharging / timeToFullCharge;
    }
}
