using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Element : MonoBehaviour
{
    public UnityEvent changed_type = new UnityEvent();

    public enum Type
    {
        None, Cryo, Electro, Hydro, Pyro
    }

    private Type type = Type.None;

    public Type GetType()
    {
        return type;
    }

    public void SetType(Type type)
    {
        this.type = type;
        print(name + " type changed to " + type);
        changed_type.Invoke();
    }

    public int GetTypeEnum()
    {
        return ElementToIndex(type);
    }

    public static int SpreadsheetIndexToType(int index)
    {
        switch (index)
        {
            case 0:
                return 4;
            case 1:
                return 3;
            case 2:
                return 2;
            case 3:
                return 1;
        }

        return 0;
    }

    public static int ElementToIndex(Type type)
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

    public static int ElementToSpreadsheetIndex(Type type)
    {
        int typeIndex = ElementToIndex(type);
        switch (typeIndex)
        {
            case 4:
                return 0;
            case 3:
                return 1;
            case 2:
                return 2;
            case 1:
                return 3;
        }

        return 0;
    }

}
