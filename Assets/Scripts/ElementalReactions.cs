using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReactions : MonoBehaviour
{
    public static Reaction[,] reactions = new Reaction[4, 4];

    public class Reaction
    {
        public string reactionName;
        public float damageMult;

        public Reaction(string reactionName, float damageMult)
        {
            this.reactionName = reactionName;
            this.damageMult = damageMult;
        }
    }

    void Start()
    {
        var dataset = Resources.Load<TextAsset>("Reactions");
        var dataLines = dataset.text.Split('\n'); // Split also works with simple arguments, no need to pass char[]

        for (int i = 1; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(',');
            for (int d = 1; d < data.Length; d++)
            {
                // int element1 = Element.SpreadsheetIndexToType(i - 1);
                // int element2 = Element.SpreadsheetIndexToType(d - 1);

                int index1 = i - 1;
                int index2 = d - 1;

                string line = data[d];
                string[] split = data[d].Split(' ');
                string reactionName = split[0];
                float damageMult = 1;

                if (reactionName == "")
                    continue;

                if (split.Length > 1)
                {
                    string dmgStr = split[1];
                    int endIndex = dmgStr.IndexOf(')');
                    string str = dmgStr.Substring(1, endIndex - 2);
                    damageMult = float.Parse(str);
                }

                Reaction reaction = new Reaction(reactionName, damageMult);
                reactions[index1, index2] = reaction;
            }
        }
    }

    public static Reaction GetReaction(Element.Type element1, Element.Type element2)
    {
        if (element1 == Element.Type.None || element2 == Element.Type.None)
            return null;

        int index1 = Element.ElementToSpreadsheetIndex(element1);
        int index2 = Element.ElementToSpreadsheetIndex(element2);
        Reaction reaction = reactions[index1, index2];

        return reaction;
    }

}
