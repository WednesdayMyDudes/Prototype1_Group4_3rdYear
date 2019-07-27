using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    // Start is called before the first frame update

    
    public List<GameObject> listOfConnectedPoppableBubbles = new List<GameObject>();

    bool inLevelBubble = false;

    public enum bubbleState { STANDBY, MOVING, STOPPED }; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D target) {



    }
}
