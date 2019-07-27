using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RyanCannon : MonoBehaviour
{

    float startBallSpeed = 0.1f;// assigns the initial speed value of the ball

    Vector3 difference;
    public int rotationOffset = 0;

    Vector2 mousPos;
    Vector2 BallPos;

    public GameObject ammoHolder;
    AmmoLoader ammoList;

    public GameObject currentBubble;
    GameObject firedBubble;

    RyanBubble BubbleStateHolder;

    // Start is called before the first frame update

    
    void Start(){

        ammoList = ammoHolder.GetComponent<AmmoLoader>();

        ammoList.setAmmo();

        currentBubble = ammoList.ammoList.Peek();
        
       
    }

    void FixedUpdate(){

        mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //gets position of mouse in the game world
        BallPos = gameObject.transform.position;// changes the balls position per fixed frame
        mousPos = mousPos - BallPos; // calculate the mouses position on the screen relative to the ball
        mousPos = mousPos.normalized; // prevents the magnitude of the balls force from being influenced by the distance of the ball to the mouse cursor, while keeping the same direection 


        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotationOffset);


        if (Input.GetKeyDown(KeyCode.Mouse0)){
                fireBubble();
            }

    }

        // Update is called once per frame
        void Update()
    {
       
    }

    public void fireBubble()
    {

        currentBubble = ammoList.ammoList.Peek();

        BubbleStateHolder = currentBubble.GetComponent<RyanBubble>();

        if (BubbleStateHolder.bubbleState == RyanBubble.BubbleState.Waiting) {
            
            BubbleStateHolder.bubbleState = RyanBubble.BubbleState.Movement;

            currentBubble.GetComponent<Rigidbody2D>().AddForce(mousPos * startBallSpeed);

            StartCoroutine(ammoList.useAmmo());
        }

      

      
    }


}
