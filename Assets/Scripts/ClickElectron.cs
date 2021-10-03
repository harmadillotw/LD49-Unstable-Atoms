using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickElectron : MonoBehaviour
{
    public GameObject takeOverController;
    public GameObject elecAnim;
    private List<Vector3> destinations = new List<Vector3>();
    private int position = 0;
    private Vector3 desPos;
    // Start is called before the first frame update
    void Start()
    {
        takeOverController = GameObject.Find("TakeOverController");
        Vector3 des;
        for (int i = 0; i < 4; i++)
        {
            des = transform.position + Utils.GetRamdomDirection() * Random.Range(3f, 3f);
            destinations.Add(des);
        }
        desPos = destinations[0];
        position++;
    }

    // Update is called once per frame
    void Update()
    {
        move(desPos);
        if (Vector3.Distance(transform.position, desPos) < 0.2f)
        {
            int newpos = position % 4;
            desPos = destinations[newpos];
            position++;
        }
    }

    private void move(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject neuroAnim = Instantiate(elecAnim, transform.position, transform.rotation);
            TakeOverController toc = takeOverController.GetComponent<TakeOverController>();
            toc.claimElectron();
            Destroy(gameObject);
        }
    }
}

