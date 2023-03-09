using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{
    private GameObject[] backgrounds;
    private GameObject[] grounds;

    private float lastBGX;
    private float lastGroundX;

    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");
    
        lastBGX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        foreach(GameObject bg in backgrounds)
        {
            lastBGX = (bg.transform.position.x > lastBGX) ? bg.transform.position.x : lastBGX;
        }

        foreach(GameObject gr in grounds)
        {
            lastGroundX = (gr.transform.position.x > lastGroundX) ? gr.transform.position.x : lastGroundX;
        }        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D) target).size.x;

            temp.x = lastBGX + width;

            target.transform.position = temp;

            lastBGX = temp.x;
        }
        else if (target.tag == "Ground")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D) target).size.x;

            temp.x = lastGroundX + width;

            target.transform.position = temp;

            lastGroundX = temp.x;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
