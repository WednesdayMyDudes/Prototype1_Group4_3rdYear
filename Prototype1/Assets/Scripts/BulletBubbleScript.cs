using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubbleScript : MonoBehaviour
{
    public GameObject[] colorBubbles; // bubbles array

    public BubbleScript.BUBBLE_TYPE typeOfBubbles; // bubble types enums 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Setting the next type of bubble before shooting 
    public void SetType(BubbleScript.BUBBLE_TYPE typeOfBubbles)
    {
        
        foreach (var go in colorBubbles)
        {
            go.SetActive(false);
        }

        this.typeOfBubbles = typeOfBubbles;

        colorBubbles[(int)typeOfBubbles].SetActive(true); // Setting active the bubble that has been selected
    }
}
