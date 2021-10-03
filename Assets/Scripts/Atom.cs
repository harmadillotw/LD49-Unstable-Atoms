using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom 
{
    public string name;
    public int maxProtons;
    public int maxNeutrons;
    public int maxElectrons;
    public int protons;
    public int neutrons;
    public int electrons;
    public Sprite atom_sprite;
    public Sprite atom_sprite_red;
    public Sprite atom_sprite_green;

    public Atom()
    {
        protons = 1;
        maxProtons = 1;
        neutrons = 0;
        maxNeutrons = 0;
        electrons = 1;
        maxElectrons = 1;
    }

    public Atom(int protons, int neutrons, int electrons)
    {
        this.protons = protons;
        this.maxProtons = protons;
        this.neutrons = neutrons;
        this.maxNeutrons = neutrons;
        this.electrons = electrons;
        this.maxElectrons = electrons;
    }

    public Atom(Atom at)
    {
        this.name = at.name;
        this.protons = at.protons;
        this.maxProtons = at.protons;
        this.neutrons = at.neutrons;
        this.maxNeutrons = at.neutrons;
        this.electrons = at.electrons;
        this.maxElectrons = at.electrons;
        this.atom_sprite = at.atom_sprite;
        this.atom_sprite_red = at.atom_sprite_red;
        this.atom_sprite_green = at.atom_sprite_green;
    }

    public Atom(int neutrons, int protons, int electrons, string name, Sprite sp_main, Sprite sp_red, Sprite sp_green)
    {
        this.protons = protons;
        this.maxProtons = protons;
        this.neutrons = neutrons;
        this.maxNeutrons = neutrons;
        this.electrons = electrons;
        this.maxElectrons = electrons;
        this.name = name;
        this.atom_sprite = sp_main;
        this.atom_sprite_red = sp_red;
        this.atom_sprite_green = sp_green;
    }

    public int getProtons()
    {
        return protons;
    }

    public int getNeutrons()
    {
        return neutrons;
    }

    public int getElectrons()
    {
        return electrons;
    }

    public void setElectrons(int elect)
    {
        electrons = elect;
    }

    public void removeElectron()
    {
        if (electrons > 0)
        {
            electrons--;
        }
    }

    public void addElectron()
    {
        electrons++;
    }

    public bool isDead()
    {
        if (electrons == 0)
        {
            return true;
        }
        return false;
    }

}
