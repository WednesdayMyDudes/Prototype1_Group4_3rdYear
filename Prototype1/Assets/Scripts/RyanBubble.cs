using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RyanBubble : MonoBehaviour
{

    public enum BubbleState { Waiting, Movement, Stopped };
    public BubbleState bubbleState;

    public GameObject manager;

    CheckWin checkLoss;
 

    bool hasCollided;
   public bool preset;

    Rigidbody2D currentBody;

    [SerializeField]
    List<GameObject> sameTypeConnectedBubbles = new List<GameObject>();

    // Start is called before the first frame update

    private float minimunHorizontalValue = -7f;
    private float maximunHorizontalValue = 7.5f;
    private float minimunVerticalValue = 2.44f;
    private float maximunVerticalValue = 17f;
    private float horizontalOffset = 0f;
    private float verticalOffset = .88f;

    float checkRadius = 0.1f;

    [SerializeField]
    private List<float> verticalValues;

    void Awake()
    {

        bubbleState = BubbleState.Waiting;

        verticalValues = new List<float>(); // Vertical values contains every possible position that bubble can be stay.
        for (int i = 0; i < 17; i++)
        {
            verticalValues.Add(minimunVerticalValue + (verticalOffset * i));
        }
    }

    void Start()
    {

        sameTypeConnectedBubbles = new List<GameObject>();

        manager = GameObject.Find("BubbleManager");

        manager.GetComponent<BubbleManager>().inGameBubbleList.Add(this.gameObject);

        checkLoss = manager.GetComponent<CheckWin>();

        if (gameObject.transform.position.y < 6)
        {

            preset = false;
        }

        else { preset = true; }

        currentBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (preset == true) {

            preset = false;

            bubbleState = BubbleState.Stopped;

            


            Destroy(currentBody);
            transform.localRotation = Quaternion.identity;
            Vector3 thePosition = transform.localPosition;

            thePosition.x = Mathf.Round(thePosition.x);
            thePosition.y = Mathf.Floor(thePosition.y);

            Debug.Log("[OnCollisionEnter] Before Round:" + thePosition);
            if (thePosition.y % 2 >= 1)
            {
                if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                    thePosition.x = maximunHorizontalValue;
                else
                    thePosition.x += horizontalOffset;
            }
            else
            {
                if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                    thePosition.x = maximunHorizontalValue - horizontalOffset;
            }



            Debug.Log("[OnCollisionEnter]Position: " + Mathf.FloorToInt(thePosition.y) + " - " + 1 + " = " + (Mathf.FloorToInt(thePosition.y) - 1));
            thePosition.y = verticalValues[(Mathf.FloorToInt(thePosition.y) - 1)];
            Debug.Log("[OnCollisionEnter] After Round:" + thePosition);
            transform.localPosition = Vector3.one * 100;
            //The offset will be set in the returnCorrectPosition after check where bubble can be place.
            transform.localPosition = returnCorretPosition(thePosition);


        }


    }

    void OnCollisionEnter2D(Collision2D bubbleCollision)
    {

       
        if (bubbleCollision.gameObject.tag.Equals(gameObject.tag) )
        {
            sameTypeConnectedBubbles.Add(bubbleCollision.gameObject);

            RyanBubble bubbleCheck;

            bubbleCheck = bubbleCollision.gameObject.GetComponent<RyanBubble>();

            if (sameTypeConnectedBubbles.Count >= 2 && hasCollided == true)
            {
                CheckWin checkwin = manager.GetComponent<CheckWin>();

                checkwin.callCheck();

                foreach (GameObject bubble in sameTypeConnectedBubbles)
                {

                    Destroy(bubble);
                }

               
                

                Destroy(this.gameObject);
            }
        }
        if (bubbleCollision.gameObject.tag.Equals("Barrier") )
        {

            if (bubbleState == BubbleState.Movement)
            {


                bubbleState = BubbleState.Stopped;

                hasCollided = true;


                Destroy(currentBody);
                transform.localRotation = Quaternion.identity;
                Vector3 thePosition = transform.localPosition;

                thePosition.x = Mathf.Round(thePosition.x);
                thePosition.y = Mathf.Floor(thePosition.y);

                Debug.Log("[OnCollisionEnter] Before Round:" + thePosition);
                if (thePosition.y % 2 >= 1)
                {
                    if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                        thePosition.x = maximunHorizontalValue;
                    else
                        thePosition.x += horizontalOffset;
                }
                else
                {
                    if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                        thePosition.x = maximunHorizontalValue - horizontalOffset;
                }
               


                Debug.Log("[OnCollisionEnter]Position: " + Mathf.FloorToInt(thePosition.y) + " - " + 1 + " = " + (Mathf.FloorToInt(thePosition.y) - 1));
                thePosition.y = verticalValues[(Mathf.FloorToInt(thePosition.y) - 1)];
                Debug.Log("[OnCollisionEnter] After Round:" + thePosition);
                transform.localPosition = Vector3.one * 100;
                //The offset will be set in the returnCorrectPosition after check where bubble can be place.
                transform.localPosition = returnCorretPosition(thePosition);
            }




        }

        else if (bubbleCollision.gameObject.tag.Equals("Donut") || bubbleCollision.gameObject.tag.Equals("CupCake") || bubbleCollision.gameObject.tag.Equals("Candy")) {

            RyanBubble bubbleCheck;

            bubbleCheck = bubbleCollision.gameObject.GetComponent<RyanBubble>();

            if (bubbleCheck.bubbleState == BubbleState.Stopped) {

               

                if (bubbleState == BubbleState.Movement)
                {


                    bubbleState = BubbleState.Stopped;

                    hasCollided = true;


                    Destroy(currentBody);
                    transform.localRotation = Quaternion.identity;
                    Vector3 thePosition = transform.localPosition;

                    thePosition.x = Mathf.Round(thePosition.x);
                    thePosition.y = Mathf.Floor(thePosition.y);

                    Debug.Log("[OnCollisionEnter] Before Round:" + thePosition);
                    if (thePosition.y % 2 >= 1)
                    {
                        if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                            thePosition.x = maximunHorizontalValue;
                        else
                            thePosition.x += horizontalOffset;
                    }
                    else
                    {
                        if (thePosition.x + horizontalOffset >= maximunHorizontalValue)
                            thePosition.x = maximunHorizontalValue - horizontalOffset;
                    }
                     if (thePosition.y < 6)
                      {
                      Debug.Log("[OnCollisionEnter]End Game");
                        //SceneManager.LoadScene("GameOver");

                        checkLoss.callLoss();

                     
                   }


                    Debug.Log("[OnCollisionEnter]Position: " + Mathf.FloorToInt(thePosition.y) + " - " + 1 + " = " + (Mathf.FloorToInt(thePosition.y) - 1));
                    thePosition.y = verticalValues[(Mathf.FloorToInt(thePosition.y) - 1)];
                    Debug.Log("[OnCollisionEnter] After Round:" + thePosition);
                    transform.localPosition = Vector3.one * 100;
                    //The offset will be set in the returnCorrectPosition after check where bubble can be place.
                    transform.localPosition = returnCorretPosition(thePosition);
                }


            }

        }

        

        if (bubbleCollision.gameObject.tag.Equals("Side"))
        {
            Debug.Log("[OnCollisionEnter]Collision contacts: " + bubbleCollision.contacts[0].normal);
            currentBody.velocity = Vector3.Reflect(transform.localPosition, bubbleCollision.contacts[0].normal);
        }
    }



    Vector2 returnCorretPosition(Vector2 objectPosition)
    {
        Vector2 newPosition = Vector2.zero;
        if (!Physics.CheckSphere(objectPosition, checkRadius))
        {
            Debug.Log("<color=red>[returnCorretPosition]</color>: Current position is available.");
            newPosition = objectPosition;
        }
        else
        {
            objectPosition.y -= verticalOffset; objectPosition.x -= horizontalOffset;
            if (!Physics.CheckSphere(objectPosition, checkRadius))
            {
                Debug.Log("<color=red>[returnCorretPosition]</color>: Top left is available.");
                newPosition = objectPosition;
            }
            else
            {
                objectPosition.x += horizontalOffset * 2;
                if (!Physics.CheckSphere(objectPosition, checkRadius))
                {
                    Debug.Log("<color=red>[returnCorretPosition]</color>: Top right is available.");
                    newPosition = objectPosition;
                }
                else
                {
                    objectPosition.y += verticalOffset * 2;
                    if (!Physics.CheckSphere(objectPosition, checkRadius))
                    {
                        Debug.Log("<color=red>[returnCorretPosition]</color>: Bottom right is available.");
                        newPosition = objectPosition;
                    }
                    else
                    {
                        objectPosition.x -= horizontalOffset * 2;
                        if (!Physics.CheckSphere(objectPosition, checkRadius))
                        {
                            Debug.Log("<color=red>[returnCorretPosition]</color>: Bottom left is available.");
                            newPosition = objectPosition;
                        }
                        else
                        {
                            Debug.Log("<color=red>[returnCorretPosition]</color>: There's no position to fixed this object. (Quitting life!)");
                        }
                    }
                }
            }
        }
        return newPosition;
    }


}