using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReactionIndicator : WorldToScreenUI
{
    public void SetReaction(ElementalReactions.ReactionEntry reaction)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = reaction.reactionName;
    }
}
