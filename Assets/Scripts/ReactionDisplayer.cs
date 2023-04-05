using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDisplayer : MonoBehaviour
{
    int index = 0;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(index).gameObject.SetActive(false);
            index++;
            if (index >= transform.childCount)
                index = 0;
            transform.GetChild(index).gameObject.SetActive(true);

        }
    }
}
