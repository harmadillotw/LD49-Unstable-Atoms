using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavedSettings
{
    public static int level = 1;
    public static int score = 0;
    public static Atom playerAtom = new Atom();
    public static Atom enemyAton = new Atom();
    public static List<AtomValue>[] atomValues = new List<AtomValue>[5];
    public static List<ElectronValue>[] electronValues = new List<ElectronValue>[5];
    public static Vector3 playerPosition;
    public static List<Atom> basicAtoms = new List<Atom>();
    public static bool returnFromTransfer = false;
    public static bool successOnTransfer = false;
    //public static 
}


