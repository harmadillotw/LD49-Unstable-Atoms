using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    //private int numberAtoms = 5;
    public List<Atom> basicAtoms = new List<Atom>();
    public GameObject electronPrefab;
    public GameObject atomPrefab;
    public GameObject player;
    public GameObject exit1;
    public GameObject exit2;
    public  List<GameObject> atomValues = new List<GameObject>();
    public  List<GameObject> electronValues = new List<GameObject>();

    public Sprite h_main;
    public Sprite h_red;
    public Sprite h_green;

    public Sprite he_main;
    public Sprite he_red;
    public Sprite he_green;

    public Sprite li_main;
    public Sprite li_red;
    public Sprite li_green;

    public Sprite be_main;
    public Sprite be_red;
    public Sprite be_green;

    public Sprite b_main;
    public Sprite b_red;
    public Sprite b_green;

    public Sprite c_main;
    public Sprite c_red;
    public Sprite c_green;

    public Sprite n_main;
    public Sprite n_red;
    public Sprite n_green;

    public Sprite o_main;
    public Sprite o_red;
    public Sprite o_green;

    public Sprite f_main;
    public Sprite f_red;
    public Sprite f_green;

    public Sprite ne_main;
    public Sprite ne_red;
    public Sprite ne_green;




    private Text scoreText;
    private Text levelText;
    private Text atomText;


    void Start()
    {
        if (SavedSettings.level == 1)
        {
            exit1.transform.position = new Vector3(-124, 0, 0);
            exit2.transform.position = new Vector3(24, 0, 0);
        } 
        else if (SavedSettings.level == 5)
        {
            exit1.transform.position = new Vector3(-24, 0, 0);
            exit2.transform.position = new Vector3(124, 0, 0);
        }
        else
        {
            exit1.transform.position = new Vector3(-24, 0, 0);
            exit2.transform.position = new Vector3(24, 0, 0);
        }
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        atomText = GameObject.Find("AtomText").GetComponent<Text>();
        levelText.text = "Level: " + SavedSettings.level;
        
        if ((SavedSettings.basicAtoms == null) || (SavedSettings.basicAtoms.Count < 10))
        {
            createBasicAtoms();
        }
        Vector3 startPos;
        atomText.text = SavedSettings.playerAtom.name;
        player.GetComponentInChildren<PlayerController>().playerAtom = new Atom(SavedSettings.playerAtom);
        player.GetComponentInChildren<SpriteRenderer>().sprite = SavedSettings.playerAtom.atom_sprite;
        if ((SavedSettings.atomValues[SavedSettings.level - 1].Count == 0) && (SavedSettings.electronValues[SavedSettings.level - 1].Count == 0))
        {
            for (int i = 0; i < 3; i++)
            {
                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                GameObject atom = Instantiate(atomPrefab, startPos, transform.rotation);
                atom.GetComponentInChildren<AtomController>().atom = new Atom(SavedSettings.basicAtoms[0]);
                atom.GetComponentInChildren<SpriteRenderer>().sprite = SavedSettings.basicAtoms[0].atom_sprite;
                atomValues.Add(atom);

                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                atom = Instantiate(atomPrefab, startPos, transform.rotation);
                atom.GetComponentInChildren<AtomController>().atom = new Atom(SavedSettings.basicAtoms[1]);
                atom.GetComponentInChildren<SpriteRenderer>().sprite = SavedSettings.basicAtoms[1].atom_sprite;
                atomValues.Add(atom);
            }
            for (int i = 0; i < 5; i++)
            {
                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                GameObject electron = Instantiate(electronPrefab, startPos, transform.rotation);
                electronValues.Add(electron);
            }
            player.GetComponentInChildren<SpriteRenderer>().sprite = SavedSettings.playerAtom.atom_sprite;
        }
        else
        {
            //atomValues = SavedSettings.savedObjects;
            foreach (AtomValue av in SavedSettings.atomValues[SavedSettings.level -1])
            {
                if ((SavedSettings.returnFromTransfer) && (SavedSettings.successOnTransfer) && (av.transferAtom))
                {
                    //do nothing 
                }
                else
                {
                    GameObject atom = Instantiate(atomPrefab, av.position, transform.rotation);
                    atom.GetComponentInChildren<AtomController>().atom = new Atom(av.atom);
                    atom.GetComponentInChildren<SpriteRenderer>().sprite = av.atom.atom_sprite;
                    atomValues.Add(atom);
                }
            }
            //electronValues = SavedSettings.electronValues;
            foreach (ElectronValue ev in SavedSettings.electronValues[SavedSettings.level - 1])
            {
                GameObject electron = Instantiate(electronPrefab, ev.position, transform.rotation);
                electronValues.Add(electron);
            }
        }

        if (SavedSettings.playerPosition != null)
        {
            if ((SavedSettings.returnFromTransfer) && (SavedSettings.successOnTransfer) )
            {
                player.GetComponentInChildren<SpriteRenderer>().sprite = SavedSettings.playerAtom.atom_sprite;
            }
            player.transform.position = SavedSettings.playerPosition;
        }    
        //spawn atoms
        //spawn electrons
        //spawn player
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score\n" + SavedSettings.score;
    }
    public List<GameObject> getAtomValues()
    {
        return atomValues;
    }
    public List<GameObject> getElectronValues()
    {
        return electronValues;
    }

    public void setAtomValues(List<GameObject> av)
    {
        this.atomValues = av;
    }

    public void deleteAtomValue(GameObject av)
    {
        if (this.atomValues.Contains(av))
        {
            this.atomValues.Remove(av);
        }
    }

    public void createBasicAtoms()
    {

        SavedSettings.atomValues[0] = new List<AtomValue>();
        SavedSettings.atomValues[1] = new List<AtomValue>();
        SavedSettings.atomValues[2] = new List<AtomValue>();
        SavedSettings.atomValues[3] = new List<AtomValue>();
        SavedSettings.atomValues[4] = new List<AtomValue>();

        SavedSettings.electronValues[0] = new List<ElectronValue>();
        SavedSettings.electronValues[1] = new List<ElectronValue>();
        SavedSettings.electronValues[2] = new List<ElectronValue>();
        SavedSettings.electronValues[3] = new List<ElectronValue>();
        SavedSettings.electronValues[4] = new List<ElectronValue>();

        


        Atom at = new Atom(1, 0, 1, "Hydrogen", h_main, h_red, h_green);
        SavedSettings.basicAtoms.Add(at);
        SavedSettings.playerAtom = at;
        at = new Atom(2, 2, 2, "Helium", he_main, he_red, he_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(3, 3, 3, "Lithium", li_main, li_red, li_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(5, 4, 4, "Beryllium", be_main, be_red, be_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(6, 5, 5, "Boron", b_main, b_red, b_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(6, 6, 6, "Carbon", c_main, c_red, c_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(7, 7, 7, "Nitrogen", n_main, n_red, n_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(8, 8, 8, "Oxygen", o_main, o_red, o_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(10, 9, 9, "Fluorine", f_main, f_red, f_green);
        SavedSettings.basicAtoms.Add(at);
        at = new Atom(10, 10, 10, "Neon", ne_main, ne_red, ne_green);
        SavedSettings.basicAtoms.Add(at);

        Vector3 startPos;
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                AtomValue av = new AtomValue(startPos);
                av.atom = new Atom(SavedSettings.basicAtoms[j]);

                SavedSettings.atomValues[j].Add(av);


                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                av = new AtomValue(startPos);
                if (j < 4)
                {
                    av.atom = new Atom(SavedSettings.basicAtoms[j + 1]);
                }
                else
                {
                    av.atom = new Atom(SavedSettings.basicAtoms[j]);
                }
                SavedSettings.atomValues[j].Add(av);


            }
            for (int i = 0; i < 5; i++)
            {
                startPos = new Vector3(Random.Range(-22f, 22f), Random.Range(-22f, 22f), 0);
                ElectronValue electron = new ElectronValue(startPos);
                SavedSettings.electronValues[j].Add(electron);
            }
        }

    }

}
