using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public enum Type
    {
        None, Cryo, Electro, Hydro, Pyro
    }

    public Type type = Type.None;

    public int GetTypeEnum()
    {
        if (type == Type.Cryo)
            return 1;
        if (type == Type.Electro)
            return 2;
        if (type == Type.Hydro)
            return 3;
        if (type == Type.Pyro)
            return 4;

        return 0;
    }

}
