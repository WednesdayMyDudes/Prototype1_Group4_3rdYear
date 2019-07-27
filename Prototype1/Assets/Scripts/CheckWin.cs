using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callCheck() {

        StartCoroutine(checkWin());

    }

    public IEnumerator checkWin()
    {

        yield return new WaitForSeconds(0.5f);

        int donuts = 0;
        int cupcakes = 0;
        int candy = 0;
        int sum = 0;


        donuts = GameObject.FindGameObjectsWithTag("Donut").Length;
        cupcakes = GameObject.FindGameObjectsWithTag("CupCake").Length;
        candy = GameObject.FindGameObjectsWithTag("Candy").Length;

        sum = donuts + cupcakes + candy;

        if (sum == 1)
        {

            Debug.Log("win" + sum);

        }

        else { Debug.Log("playing"); }
    }
}
