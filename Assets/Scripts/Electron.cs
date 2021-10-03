using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody2D rb;
    public LevelController lc;

    public AudioClip atomDeathClip;
    //public AudioClip transferFailClip;
    public AudioSource audioSource;

    public GameObject atomAnim;

    private float maxX = 25f;
    private float maxY = 25f;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private int state = 0;  // 0= initial , 1 = roaming, 2=fired.
    private float timer = 0.0f;

    private List<Vector3> destinations = new List<Vector3>();
    private int position = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 des;
        for (int i = 0; i < 4; i++)
        {
            des = new Vector3(0, 0, 0) + Utils.GetRamdomDirection() * Random.Range(23f, 23f);
            destinations.Add(des);
        }

        rb = GetComponent<Rigidbody2D>();
        lc = GameObject.FindObjectOfType<LevelController>();
        startingPosition = destinations[0];
        roamingPosition = destinations[0];
        position++;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            timer += Time.deltaTime;
            if (timer > 4f)
            {
                state = 1;
                //Debug.Log("AtomController Enter State 1");
            }
        } else if (state == 1)
        { 
            float reachedPos = 1f;

            if (Vector3.Distance(transform.position, roamingPosition) < reachedPos)
            {
                int newpos = position % 4;
                roamingPosition = destinations[newpos];
                position++;

            }
            else
            {
                move(roamingPosition);
            }
        }
    }

    public void setSpeed(float xSpeed, float ySpeed)
    {
        rb.AddForce(new Vector2(xSpeed * speed, ySpeed * speed), ForceMode2D.Impulse);
    }
    public void setState(int state)
    {
        this.state = state;
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == 1)
        {
            float newXForce = rb.velocity.x * -2;
            float newYForce = rb.velocity.y * -2;
            rb.AddForce(new Vector2(newXForce, newYForce));
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController p = collision.gameObject.GetComponent<PlayerController>();
                p.collectElectron();
                //Debug.Log(" Electron OnTriggerEnter2D collide Player");
                Destroy(gameObject);
            }
            else if ((collision.gameObject.tag == "Atom") && state == 2)
            {
                //Debug.Log(" Electron OnTriggerEnter2D collide Atom");
                AtomController ac = collision.gameObject.transform.parent.GetComponent<AtomController>();
                ac.atom.removeElectron();
                if (ac.atom.isDead())
                {
                    GameObject neuroAnim = Instantiate(atomAnim, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    SavedSettings.score += (ac.atom.maxElectrons * 10);
                    lc.deleteAtomValue(collision.gameObject.transform.parent.gameObject);
                    Destroy(collision.gameObject);
                }

                Destroy(gameObject);
            }
            else
            {

                float newXForce = rb.velocity.x * -2;
                float newYForce = rb.velocity.y * -2;
                //Debug.Log(" Electron OnTriggerEnter2D " + newXForce + " , " + newYForce);
                rb.AddForce(new Vector2(newXForce, newYForce), ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            PlayerController p = collider.gameObject.GetComponent<PlayerController>();
            p.collectElectron();
            //Debug.Log(" Electron OnTriggerEnter2D collide Player");
            Destroy(gameObject);
        }
        else if ((collider.gameObject.tag == "Atom") && state == 2)
        {
            //Debug.Log(" Electron OnTriggerEnter2D collide Atom");
            AtomController ac = collider.gameObject.transform.parent.GetComponent<AtomController>();
            ac.atom.removeElectron();
            if (ac.atom.isDead())
            {
                playAudio(atomDeathClip);
                SavedSettings.score += (ac.atom.maxElectrons * 10);
                lc.deleteAtomValue(collider.gameObject.transform.parent.gameObject);
                Destroy(collider.gameObject);
            }
            
            Destroy(gameObject);
        }
        else 
        { 

            float newXForce = rb.velocity.x * -2;
            float newYForce = rb.velocity.y * -2;
            //Debug.Log(" Electron OnTriggerEnter2D " + newXForce + " , " + newYForce);
            rb.AddForce(new Vector2(newXForce, newYForce), ForceMode2D.Impulse);
        }


    }
    private void move(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
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
