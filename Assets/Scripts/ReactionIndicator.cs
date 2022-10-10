using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReactionIndicator : WorldToScreenUI
{
    public void SetReaction(ElementalReactions.Reaction reaction)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = reaction.reactionName;
        GetComponent<Animator>().speed = 0.8f;
    }
}
