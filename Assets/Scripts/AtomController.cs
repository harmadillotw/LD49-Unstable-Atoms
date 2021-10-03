using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Atom atom;
    public GameObject gammaPrefab;

    private GameObject player;
    public float speed = 10f;
    public float chaseDistance = 3f;
    public float shootDistance = 10f;


    //public AudioClip atomDeathClip;
    public AudioClip gammaFireClip;
    //public AudioClip transferFailClip;
    public AudioSource audioSource;

    private float shootTimer = 0f; 
    private float shootPeriod = 2f;

    private float maxX = 25f;
    private float maxY = 25f;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float timer = 0.0f;

    private List<Vector3> destinations = new List<Vector3>();
    private int position = 0;

    private int state = 0;

    private float flashInterval = 0.3f;
    private float flashCout = 0f;
    private bool flashing = false;
    private bool mainImage = true;

    void Start()
    {
        shootTimer = Random.Range(0f, 1f);
        Vector3 des;
        for (int i = 0; i < 4; i++)
        {
            des = new Vector3(0,0,0) + Utils.GetRamdomDirection() * Random.Range(23f, 23f);
            destinations.Add(des);
        }

        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        roamingPosition = destinations[0];
        player = GameObject.Find("PlayerSprite");
    }

    //private Vector3 GetRamdomDirection()
    //{
    //    return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    //}
    //private Vector3 GetRoamingPosition(Vector3 fromPos)
    //{
    //    return fromPos + GetRamdomDirection() * Random.Range(10f,30f);
    //}
    // Update is called once per frame
    void Update()
    {
        float distance = 0f;
        switch (state)
        {
            // initialise
            case 0:
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    state = 1;
                    //Debug.Log("AtomController Enter State 1");
                }
                break;
            // roaming
            case 1:
                shootTimer += Time.deltaTime;
                distance = Vector2.Distance(player.transform.position, transform.position);
                if (distance < chaseDistance)
                {
                    state = 2;
                    //Debug.Log("AtomController Enter State 2");
                }
                else 
                {
                    if ((distance < shootDistance) && (shootTimer > shootPeriod))
                    {
                        //transform.up = player.transform.position;
                        Vector3 difference = player.transform.position - transform.position;
                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                        float shootDistance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();

                        shootGammaRay(direction, rotationZ);
                        shootTimer = 0f;
                    }
                        //Debug.Log(" AtomController: distance to player " + distance);

                    float reachedPos = 0.5f;
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
                    break;
            //chasing
            case 2:
                
                distance = Vector2.Distance(player.transform.position, transform.position);
                if (distance > chaseDistance)
                {
                    state = 1;
                    Debug.Log("AtomController Enter State 1");
                }
                else
                {
                    move(player.transform.position);
                }
                break;

        }

        if (flashing)
        {
            flashCout += Time.deltaTime;
            if ( flashCout > flashInterval)
            {
                switchSprite();
                flashCout = 0f;
            }
        }

    }

    private void switchSprite()
    {
        mainImage = !mainImage;
        if (mainImage)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = atom.atom_sprite;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = atom.atom_sprite_red;
        }
    }

    private void setMainSprite()
    {
        mainImage = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = atom.atom_sprite;
    }
    private bool withinBounds(Vector3 destination)
    {
        if (destination.x > maxX)
        {
            return false;
        }
        if (destination.x < -maxX)
        {
            return false;
        }
        if (destination.y > maxY)
        {
            return false;
        }
        if (destination.y < -maxY)
        {
            return false;
        }
        return true;
    }
    private void shootGammaRay(Vector2 direction, float rotationZ)
    {
        playAudio(gammaFireClip);
        GameObject b = Instantiate(gammaPrefab) as GameObject;
        b.transform.position = transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * 10f;

    //    // TODO: shoot noise
    //    float xdir = 0f;
    //float ydir = 0f;

    //Debug.Log(" playerVel: " + rb.velocity.x + "," + rb.velocity.y);

    //Vector3 startPos = new Vector3(transform.position.x , transform.position.y , transform.position.z);
    //GameObject instance = Instantiate(gammaPrefab, startPos, transform.rotation);
    //instance.GetComponentInChildren<Rigidbody2D>().AddForce(transform.forward * 3f);




    }
    private void move(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flashing = true;
            flashCout = 0f;
            switchSprite();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flashing = false;
            flashCout = 0f;
            setMainSprite();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flashing = true;
            flashCout = 0f;
            switchSprite();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flashing = false;
            flashCout = 0f;
            setMainSprite();
        }
    }
    public void setAtom(Atom at)
    {
        this.atom = at;
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
