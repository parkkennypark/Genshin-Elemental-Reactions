using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Enemy : MonoBehaviour
{
    public Element.Type baseType;

    private Element element;

    public GameObject elementIndicatorPrefab;
    public GameObject reactionIndicatorPrefab;
    public GameObject damageIndicatorPrefab;

    private ElementIndicator elementIndicator;

    void Start()
    {
        element = GetComponent<Element>();
        element.changed_type.AddListener(OnElementChanged);

        elementIndicator = Instantiate(elementIndicatorPrefab).GetComponent<ElementIndicator>();
        elementIndicator.Setup(transform, Vector3.up * 2);
        elementIndicator.UpdateElement(element.GetType());

        element.SetType(baseType);
    }

    void OnElementChanged()
    {
        elementIndicator.UpdateElement(element.GetType());
    }

    void Update()
    {

    }

    public void TakeDamage(Element.Type incomingElement, int damage, bool dontEffectElement)
    {
        ElementalReactions.ReactionEntry reaction = ElementalReactions.GetReaction(element.GetType(), incomingElement);
        if (reaction != null)
        {
            damage = Mathf.RoundToInt(damage * reaction.damageMult);

            ReactionIndicator reactionIndicator = Instantiate(reactionIndicatorPrefab).GetComponent<ReactionIndicator>();
            reactionIndicator.Setup(transform, Vector3.up * 1);
            reactionIndicator.SetReaction(reaction);

        }

        if (baseType == Element.Type.None || dontEffectElement)
        {
            element.SetType(incomingElement);
        }

        if (incomingElement == Element.Type.Pyro && !dontEffectElement)
        {
            StartCoroutine(TakeContinuousDamage(incomingElement, damage / 10, 0.2f, 15));
        }

        DamageIndicator damageIndicator = Instantiate(damageIndicatorPrefab).GetComponent<DamageIndicator>();
        damageIndicator.Setup(transform, Vector3.up * 1 + Random.insideUnitSphere);
        damageIndicator.SetText(incomingElement, damage, false);

        GetComponent<Animation>().Play();
    }

    IEnumerator TakeContinuousDamage(Element.Type element, int damage, float frequency, int numTimes)
    {
        for (int i = 0; i < numTimes; i++)
        {
            yield return new WaitForSeconds(frequency);
            TakeDamage(element, damage, true);
        }
    }
}