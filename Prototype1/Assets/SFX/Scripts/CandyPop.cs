using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPop : MonoBehaviour
{
    // Start is called before the first frame update
    //Will be attached to any object that has to play sound
    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("BG Sound");    //Play bounce sound
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            FindObjectOfType<AudioManager>().Play("Bounce");    //Play bounce sound
        }

        if (Input.GetKey(KeyCode.E))
        {
            FindObjectOfType<AudioManager>().Play("Explode");    //Play bounce sound
        }

        if (Input.GetKey(KeyCode.S))
        {
            FindObjectOfType<AudioManager>().Play("Shoot");    //Play bounce sound
        }

        if (Input.GetKey(KeyCode.M))
        {
            FindObjectOfType<AudioManager>().Play("Match");    //Play bounce sound
            StartCoroutine(Delay());
            

        }

        if (Input.GetKey(KeyCode.N))
        {
            FindObjectOfType<AudioManager>().Play("No Match");  //Play bounce sound
        }
    }

   
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<AudioManager>().Play("Unwrap");    //Play bounce sound
    }
}
