using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReactions : MonoBehaviour
{
    public static string[,] reactions = new string[4, 4];

    void Start()
    {



        var dataset = Resources.Load<TextAsset>("Reactions");
        var dataLines = dataset.text.Split('\n'); // Split also works with simple arguments, no need to pass char[]

        for (int i = 1; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(',');
            for (int d = 1; d < data.Length; d++)
            {
                int element1 = i - 1;
                int element2 = d - 1;

                string line = data[d];
                string[] split = data[d].Split(' ');
                string reaction = split[0];
                float damageMult = 1;

                if (split.Length > 1)
                {
                    string dmgStr = split[1];
                    int endIndex = dmgStr.IndexOf(')');
                    string str = dmgStr.Substring(1, endIndex - 2);
                    damageMult = float.Parse(str);
                    print(str);
                }

                reactions[element1, element2] = reaction;
            }
        }
    }

}
