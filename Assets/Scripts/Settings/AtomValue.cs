using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomValue
{
    public Vector3 position;
    public Atom atom;
    public bool transferAtom;
    public bool destroyed;

    public AtomValue(Vector3 pos)
    {
        position = pos;
    }
}
