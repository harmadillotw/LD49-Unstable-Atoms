using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject LevelController;
    //public Vector2 speed = new Vector2(50, 50);
    public float speed = 25f;
    public GameObject electronPrefab;
    public Rigidbody2D rb;
    public Atom playerAtom;

    public AudioClip gainElectronClip;
    public AudioClip loseElectronClip;
    //public AudioClip atomDeathClip;
    public AudioClip electronFireClip;
    //public AudioClip gammaFireClip;
    //public AudioClip transferSuccessClip;
    //public AudioClip transferFailClip;
    public AudioSource audioSource;


    private CircleCollider2D cc2d;
    private int count = 0;
    private int pFaceX = 0;
    private int pFaceY = 0;
    private float stabilityTime = 0f;
    private int stability = 0;
    private int maxStabilityTime = 5;

    private float flashInterval = 0.3f;
    private float flashCout = 0f;
    private bool flashing = false;
    private bool mainImage = true;
    private bool flashGreen = false;

    private LayerMask atomMask;
    private float atomCollisionTime = 0f;
    void Start()
    {
        atomMask = LayerMask.GetMask("Atom");
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        playerAtom = SavedSettings.playerAtom;
    }

    // Update is called once per frame
    void Update()
    {

        if (cc2d.IsTouchingLayers(atomMask))
        {
            atomCollisionTime += Time.deltaTime;
            if (atomCollisionTime > 2f)
            {
                //lose election
                if (playerAtom.getElectrons() > 0)
                {
                    playerAtom.removeElectron();
                    updateStability();
                }
                atomCollisionTime = 0f;
            }
        }
        if (stability != 0)
        {
            stabilityTime += Time.deltaTime;
        }

        


        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(inputX * speed, 0));
        rb.AddForce(new Vector2(0, inputY * speed));
        //Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        //movement *= Time.deltaTime;
        if ((Input.GetKey("a")) && (Input.GetKey("w")))
        {
            pFaceX = -1;
            pFaceY = 1;
        }
        else if ((Input.GetKey("a")) && (Input.GetKey("s")))
        {
            pFaceX = -1;
            pFaceY = -1;
        }
        else if ((Input.GetKey("d")) && (Input.GetKey("w")))
        {
            pFaceX = 1;
            pFaceY = 1;
        }
        else if ((Input.GetKey("d")) && (Input.GetKey("s")))
        {
            pFaceX = 1;
            pFaceY = -1;
        }
        else if (Input.GetKey("a"))
        {
            pFaceX = -1;
            pFaceY = 0;
        }
        else if (Input.GetKey("d"))
        {
            pFaceX = 1;
            pFaceY = 0;
        }
        else if (Input.GetKey("w"))
        {
            pFaceX = 0;
            pFaceY = 1;
        }
        else if (Input.GetKey("s"))
        {
            pFaceX = 0;
            pFaceY = -1;
        }
        //player.transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireElectron(inputX, inputY);
        }
        //Debug.Log(" Player Electrons: " + playerAtom.getElectrons());
        checkStability();
        if (flashing)
        {
            flashCout += Time.deltaTime;
            if (flashCout > flashInterval)
            {
                switchSprite(flashGreen);
                flashCout = 0f;
            }
        }
    }
    void fireElectron(float inputX, float inputY)
    {
        if (playerAtom.getElectrons() > 0)
        {
            if (pFaceY == 0 && pFaceX == 0)
            {
                // no shooting as no facing.
            }
            else
            {

                playAudio(electronFireClip);
                float xdir = 0f;
                float ydir = 0f;
                if (rb.velocity.x > 0)
                {
                    xdir = 1f;
                }
                else if (rb.velocity.x < 0)
                {
                    xdir = -1f;

                }
                if (rb.velocity.y > 0)
                {
                    ydir = 1f;
                }
                else if (rb.velocity.y < 0)
                {

                    ydir = -1f;
                }
                count++;
                //Debug.Log(" playerVel: " + rb.velocity.x + "," + rb.velocity.y);
                //Debug.Log(" fireElectron: " + pFaceX + "," + pFaceY);
                Vector3 startPos = new Vector3(transform.position.x + pFaceX, transform.position.y + pFaceY, transform.position.z);
                GameObject instance = Instantiate(electronPrefab, startPos, transform.rotation);
                instance.GetComponentInChildren<Electron>().setState(2);
                instance.GetComponentInChildren<Electron>().setSpeed(pFaceX, pFaceY);
                //instance.GetComponent<Electron>().setSpeed(rb.velocity.x, rb.velocity.y);
                if (playerAtom.getElectrons() > 0)
                {
                    playerAtom.removeElectron();
                    updateStability();
                }
            }
        }
        else
        {
            // TODO: failed to shoot noise
        }
    }

    public void collectElectron()
    {
        playAudio(gainElectronClip);
        playerAtom.addElectron();
        updateStability();

    }

    public void loseElectron()
    {
        playAudio(loseElectronClip);
        playerAtom.removeElectron();
        updateStability();

    }
    private void checkStability()
    {
        if (stability != 0)
        {
            if (stabilityTime > maxStabilityTime)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }

    }
    private void updateStability()
    {
        if (playerAtom.getElectrons() == 0)
        {
            if (stability != 3)
            {
                stability = 3;
                maxStabilityTime = 5;
                flashInterval = 0.05f;
                flashing = true;
                flashGreen = false;
            }
        } else if (playerAtom.electrons == playerAtom.maxElectrons)
        {
            if (stability != 0)
            {
                maxStabilityTime = 15;
                stability = 0;
                stabilityTime = 0;
                flashing = false;
                flashInterval = 0.5f;
                setMainSprite();
            }
        } else if (playerAtom.electrons < playerAtom.maxElectrons)
        {
            flashGreen = false;
            if ((playerAtom.electrons / playerAtom.maxElectrons * 100) < 50)
            {
                if (stability != 2)
                {
                    if (stability > 2)
                    {
                        stabilityTime = 0;
                    }
                    stability = 2;
                    maxStabilityTime = 10;
                    flashInterval = 0.2f;
                    flashing = true;
                }
            }
            else
            {
                if (stability != 1)
                {
                    if (stability > 1)
                    {
                        stabilityTime = 0;
                    }
                    stability = 1;
                    maxStabilityTime = 15;
                    flashInterval = 0.5f;
                    flashing = true;
                }
            }
        }
        else
        {
            flashGreen = true;
            if ((playerAtom.electrons / playerAtom.maxElectrons * 100) > 200)
            {
                if (stability != 3)
                {
                    stability = 3;
                    maxStabilityTime = 5;
                    flashInterval = 0.05f;
                    flashing = true;
                }
            } else if ((playerAtom.electrons / playerAtom.maxElectrons * 100) > 150)
            {
                if (stability != 2)
                {
                    if (stability > 2)
                    {
                        stabilityTime = 0;
                    }
                    stability = 2;
                    maxStabilityTime = 10;
                    flashInterval = 0.2f;
                    flashing = true;
                }
            }
            else
            {
                if (stability != 1)
                {
                    if (stability > 1)
                    {
                        stabilityTime = 0;
                    }
                    stability = 3;
                    maxStabilityTime = 15;
                    flashInterval = 0.5f;
                    flashing = true;
                }
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.gameObject.tag == "Atom"))
        {
            
            if (Input.GetKey("e"))
            {
                saveSettings(collider.gameObject);
                SceneManager.LoadScene("TakeOverScene");
            }
            
        }
        if ((collider.gameObject.tag == "Gamma"))
        {

            //Debug.Log("Gamma Trigger Collision");
            loseElectron();
            Destroy(collider.gameObject);


        }

        if ((collider.gameObject.tag == "Exit"))
        {
            //Debug.Log("Exit Trigger Collision");
            if (Input.GetKey("e"))
            {
                SceneManager.LoadScene("LevelMenu");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if ((collider.gameObject.tag == "Atom"))
        {

            if (Input.GetKey("e"))
            {
                saveSettings(collider.gameObject);
                SceneManager.LoadScene("TakeOverScene");
            }

        }
        if ((collider.gameObject.tag == "Gamma"))
        {

            //Debug.Log("Gamma Collision");

        }
        if ((collider.gameObject.tag == "Exit"))
        {

            //Debug.Log("Exit Collision");

        }
    }

    private void saveSettings(GameObject colGO)
    {
        LevelController lc = LevelController.GetComponent<LevelController>();
        List<AtomValue> AVList = new List<AtomValue>();
        foreach (GameObject go in lc.getAtomValues())
        {
            //Component goAS = go.GetComponentInChildren("AtomSprite");
            AtomValue av = new AtomValue(go.transform.position);
            av.atom = go.GetComponentInChildren<AtomController>().atom;
            if (go == colGO)
            {
                av.transferAtom = true;
                SavedSettings.enemyAton = av.atom;
            }
            AVList.Add(av);
        }
        SavedSettings.atomValues[SavedSettings.level - 1] = AVList;

        List<ElectronValue> EVList = new List<ElectronValue>();
        foreach (GameObject go in lc.getElectronValues())
        {
            ElectronValue ev = new ElectronValue(go.transform.position);
            EVList.Add(ev);
        }
        SavedSettings.electronValues[SavedSettings.level - 1] = EVList;

        SavedSettings.playerPosition = transform.position;
        SavedSettings.playerAtom = playerAtom;

        //temp enemy atom
        
    }

    private void switchSprite(bool green)
    {
        mainImage = !mainImage;
        if (mainImage)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerAtom.atom_sprite;
        }
        else
        {
            if (green)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerAtom.atom_sprite_green;
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerAtom.atom_sprite_red;
            }
        }
    }

    private void setMainSprite()
    {
        mainImage = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = playerAtom.atom_sprite;
    }

    private void playAudio(AudioClip clip)
    {
        int volumeSet = PlayerPrefs.GetInt("FXvolumeSet");
        float vol = 1f;
        if (volumeSet > 0)
        {
            int volume = PlayerPrefs.GetInt("FXVolume");
            vol = (float)volume / 100f;
        }

        audioSource.PlayOneShot(clip, vol);
    }
}