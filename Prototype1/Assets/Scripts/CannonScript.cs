using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{

    public GameObject[] bubbles; // Game object bubbles
    public GameObject dotTrailPrefab; // Dot trail prefab
    public BulletBubbleScript bubble; // Bullet bubble

    private bool mouseDown = false; // On click
    private List<Vector2> dotsTrail; // List of dots trail
    private List<GameObject> dotsPool; // Pool for dots - 
    private int maxDots = 30; // Maximum number of dots

    private float dotGap = 0.30f; // The gap between dots
    private float bubbleProgress = 0.0f; // How far the bullet will progress - destroy at where the trail ends
    private float bulletIncrement = 0.0f; // Bubble increment

    private int type = 0;


    // Use this for initialization
    void Start()
    {

        dotsTrail = new List<Vector2>(); // 

        dotsPool = new List<GameObject>(); // Pool of dots


        // Setting up dots' alpha so they become transparent as they progress
        var i = 0;

        var alpha = 1.0f / maxDots; // 

        var startAlpha = 1.0f;


        while (i < maxDots)
        {
            var dot = Instantiate(dotTrailPrefab) as GameObject; // Instantiating the dot prefab

            var sp = dot.GetComponent<SpriteRenderer>(); // Retrieving the 'SpriteRenderer' Component

            var c = sp.color; // Spriwtes colour


            c.a = startAlpha - alpha; // Reducing the sprite's alpha

            startAlpha -= alpha; // //

            sp.color = c; // //


            dot.SetActive(false); // bool for setting active the dots when clicking the button

            dotsPool.Add(dot); // Pooling method for the dots

            i++;
        }

        // Select initial type
        SetNextType();
    }

    // Void function for randomising and setting the next type of ball
    void SetNextType()
    {
        // Access avery bubble in the array and set it to false
        foreach (var go in bubbles)
        {
            go.SetActive(false); // 
        }

        type = Random.Range(0, 8); 

        bubbles[type].SetActive(true); // Set the bubbles to true if selected

    }

    void HandleTouchDown(Vector2 touch)
    {

    }

    // Handling the button Up
    void HandleTouchUp(Vector2 touch)
    {
        // if dots trail is null - return its result
        if (dotsTrail == null || dotsTrail.Count < 2)
            return;

        // De-activate the dots trail when the button is released
        foreach (var d in dotsPool)
            d.SetActive(false);


        // 
        bubbleProgress = 0.0f;

        // Getting the type of balls that are stored within the enum Bubble_Type in the BubbleScript
        bubble.SetType((BubbleScript.BUBBLE_TYPE)type);
       
        // 
        bubble.gameObject.SetActive(true);

        // Transforming the bubble along the dots trail path
        bubble.transform.position = transform.position;


        // Path function
        InitPath();

        // Setting the bubble[Reload a random bubble after releasing the button]
        SetNextType();
    }

    // function for when a player is holding the button down and moving the mouse around
    void HandleTouchMove(Vector2 touch)
    {
        // return the bubble's state result [ignore the parent's state]
        if (bubble.gameObject.activeSelf)
            return;

        // if dots trail is null - return its result
        if (dotsTrail == null)
        {
            return;
        }

        // remove all the dotsTrail that have been destroyed from the vector
        dotsTrail.Clear();

        // foreach loop for setting active state to false every dots in the pool
        foreach (var dots in dotsPool)
            dots.SetActive(false);


        Vector2 point = Camera.main.ScreenToWorldPoint(touch);

        var direction = new Vector2(point.x - transform.position.x, point.y - transform.position.y);


        // Shooting a raycast in the direction of the mouse
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        // 
        if (hit.collider != null)
        {
            // transform the dots trail along the raycast
            dotsTrail.Add(transform.position);

            // if the raycast collides with the side walls, get direct
            if (hit.collider.tag == "SideWalls")
            {
                DoRayCast(hit, direction);
            }
            else
            {
                dotsTrail.Add(hit.point);

                DrawPaths();
            }
           
        }
    }

    void DoRayCast(RaycastHit2D previousHit, Vector2 directionIn)
    {
        // previous hit - shoot another raycast from where the ray previously hit the wall
        dotsTrail.Add(previousHit.point);


        // normalizing the raycast
        var normal = Mathf.Atan2(previousHit.normal.y, previousHit.normal.x);

        // getting a new direction for the new raycast
        var newDirection = normal + (normal - Mathf.Atan2(directionIn.y, directionIn.x));

        // reflecting the raycast[]
        var reflection = new Vector2(-Mathf.Cos(newDirection), -Mathf.Sin(newDirection));

        // setting the collision point as a new cast point
        var newCastPoint = previousHit.point + (2 * reflection);


        // cast a new ray and reflect it if it collides with anything
        var hit2 = Physics2D.Raycast(newCastPoint, reflection);
        if (hit2.collider != null)
        {
            // 
            if (hit2.collider.tag == "SideWalls")
            {
                //shoot another raycast then reflect it
                DoRayCast(hit2, reflection);
            }
            else
            {
                // add to the list
                dotsTrail.Add(hit2.point);

                // draw the dots trail path
                DrawPaths();
            }
        }
        else
        {
            DrawPaths();
        }
    }


    // Update is called once per frame
    void Update()
    {
        // setting the bubble game object's state
        if (bubble.gameObject.activeSelf)
        {

            bubbleProgress += bulletIncrement;

            if (bubbleProgress > 1)
            {

                // remove a dot trail at zero[0]
                dotsTrail.RemoveAt(0);

                // set false if the dot count is less than 2
                if (dotsTrail.Count < 2)
                {
                    bubble.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    InitPath();
                }
            }

            var px = dotsTrail[0].x + bubbleProgress * (dotsTrail[1].x - dotsTrail[0].x);
            var py = dotsTrail[0].y + bubbleProgress * (dotsTrail[1].y - dotsTrail[0].y);

            bubble.transform.position = new Vector2(px, py);

            return;
        }

        // return the trail's result
        if (dotsTrail == null)
            return;

        // Inputs
        if (Input.touches.Length > 0)
        {

            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchDown(touch.position);
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                HandleTouchUp(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                HandleTouchUp(touch.position);
            }
            HandleTouchMove(touch.position);
            return;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            HandleTouchDown(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            HandleTouchUp(Input.mousePosition);
        }
        else if (mouseDown)
        {
            HandleTouchMove(Input.mousePosition);
        }
    }

    // function for drawing the trail path[dotted line]
    void DrawPaths()
    {

        if (dotsTrail.Count > 1)
        {
            // set false for every dot in the pool array
            foreach (var d in dotsPool)
                d.SetActive(false);

            int index = 0;

            // 
            for (var i = 1; i < dotsTrail.Count; i++)
            {
                DrawSubPath(i - 1, i, ref index);
            }
        }
    }

    // function drawing the subpath
    void DrawSubPath(int start, int end, ref int index)
    {
        var pathLength = Vector2.Distance(dotsTrail[start], dotsTrail[end]); // trail's leght


        int numDots = Mathf.RoundToInt((float)pathLength / dotGap); // getting the space between the dots

        float dotProgress = 1.0f / numDots; // 

        var p = 0.0f;

        //
        while (p < 1)
        {
            var px = dotsTrail[start].x + p * (dotsTrail[end].x - dotsTrail[start].x);
            var py = dotsTrail[start].y + p * (dotsTrail[end].y - dotsTrail[start].y);

            if (index < maxDots)
            {
                var d = dotsPool[index];

                d.transform.position = new Vector2(px, py);

                d.SetActive(true);

                index++;
            }

            p += dotProgress;
        }
    }


    void InitPath()
    {
        var start = dotsTrail[0]; //

        var end = dotsTrail[1]; //

        var length = Vector2.Distance(start, end); // trail length

        var iterations = length / 0.30f; // Adjust how fast you want the bubble to move

        bubbleProgress = 0.0f; // 

        bulletIncrement = 1.0f / iterations; // 
    }

    // I left out other sections because I assume you know, Ryan.

}
