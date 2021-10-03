using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    bool followPlayer = true;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown("q"))
        {
            followPlayer = !followPlayer;
        }
        if (followPlayer)
        {
            //Debug.Log("CameraController: " + offset + " ,   " + player.transform.position.x + "," + player.transform.position.y);
            transform.position = player.transform.position + offset;
            
        }
    }
}
