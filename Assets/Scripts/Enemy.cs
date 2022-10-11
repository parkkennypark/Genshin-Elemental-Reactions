using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Enemy : MonoBehaviour
{
    public Element.Type baseType;

    private Element element;
    private Rigidbody rb;

    public GameObject elementIndicatorPrefab;
    public GameObject reactionIndicatorPrefab;
    public GameObject damageIndicatorPrefab;

    public GameObject overloadedExplosionPrefab;
    public GameObject superconductExplosionPrefab;

    private ElementIndicator elementIndicator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        element = GetComponent<Element>();
        element.changed_type.AddListener(OnElementChanged);

        elementIndicator = Instantiate(elementIndicatorPrefab).GetComponent<ElementIndicator>();
        elementIndicator.Setup(transform, Vector3.up * 1.4f);
        elementIndicator.UpdateElement(element.GetType());

        element.SetType(baseType);

        if (baseType != Element.Type.None)
            elementIndicator.SetName(baseType);

    }

    void OnElementChanged()
    {
        elementIndicator.UpdateElement(element.GetType());
    }

    void Update()
    {

    }

    public void TakeDamage(Element.Type incomingElement, int damage, bool secondaryDamage, Vector3 dir, float chargeRatio)
    {
        damage = Mathf.RoundToInt(damage * Random.Range(0.9f, 1.1f));

        ElementalReactions.Reaction reaction = ElementalReactions.GetReaction(element.GetType(), incomingElement);
        if (reaction != null && !secondaryDamage)
        {
            damage = Mathf.RoundToInt(damage * reaction.damageMult);

            ReactionIndicator reactionIndicator = Instantiate(reactionIndicatorPrefab).GetComponent<ReactionIndicator>();
            reactionIndicator.Setup(transform, Vector3.up * 1);
            reactionIndicator.SetReaction(reaction);

            print(reaction.reactionName);

            if (reaction.reactionName.Contains("Overloaded"))
                Instantiate(overloadedExplosionPrefab, transform.position - dir.normalized, Quaternion.identity);

            if (reaction.reactionName.Contains("Electro-Charged"))
                StartCoroutine(TakeContinuousDamage(incomingElement, damage / 10, 0.2f, 15));

            if (reaction.reactionName.Contains("Superconduct"))
            {
                Instantiate(superconductExplosionPrefab, transform.position - dir.normalized, Quaternion.identity);
            }

            if (baseType == Element.Type.None)
                element.SetType(Element.Type.None);
        }

        if (reaction == null && baseType == Element.Type.None && !secondaryDamage)
        {
            element.SetType(incomingElement);
        }

        DamageIndicator damageIndicator = Instantiate(damageIndicatorPrefab).GetComponent<DamageIndicator>();
        damageIndicator.Setup(transform, Vector3.up * 1 + Random.insideUnitSphere);
        damageIndicator.SetText(incomingElement, damage, false);

        GetComponent<Animation>().Play();

        rb.AddForce(dir * damage * chargeRatio);
        rb.AddForce(Vector3.up * damage * chargeRatio);
    }

    IEnumerator TakeContinuousDamage(Element.Type element, int damage, float frequency, int numTimes)
    {
        for (int i = 0; i < numTimes; i++)
        {
            yield return new WaitForSeconds(frequency);
            TakeDamage(element, damage, true, Vector3.zero, 0);
        }
    }
}