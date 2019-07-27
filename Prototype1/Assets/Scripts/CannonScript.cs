using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{

    public GameObject[] bubbles; // Game object bubbles
    public GameObject dotTrailPrefab; // Dot trail prefab
    public BulletBubbleScript bullet; // Bullet bubble

    private bool mouseDown = false; // On click
    private List<Vector2> dotsTrail; // List of dots trail
    private List<GameObject> dotsPool; //
    private int maxDots = 30; // Maximum number of dots

    private float dotGap = 0.20f; // The gap between dots
    private float bulletProgress = 0.0f; // How far the bullet will progress
    private float bulletIncrement = 0.0f; // Bullet increment

    private int typeOfBalls = 0;


    // Use this for initialization
    void Start()
    {

        dotsTrail = new List<Vector2>(); // 

        dotsPool = new List<GameObject>(); //


        // Seting up a while loop for the dot trail fading
        var i = 0;

        var alpha = 1.0f / maxDots;

        var startAlpha = 1.0f;

        while (i < maxDots)
        {
            var dot = Instantiate(dotTrailPrefab) as GameObject;

            var sp = dot.GetComponent<SpriteRenderer>();

            var c = sp.color;


            c.a = startAlpha - alpha;

            startAlpha -= alpha;

            sp.color = c;


            dot.SetActive(false);

            dotsPool.Add(dot);

            i++;
        }

        //select initial type
        SetNextType();
    }

    void SetNextType()
    {

        foreach (var go in bubbles)
        {
            go.SetActive(false);
        }

        typeOfBalls = Random.Range(0, 8);

        bubbles[typeOfBalls].SetActive(true);

    }

    void HandleTouchDown(Vector2 touch)
    {

    }

    void HandleTouchUp(Vector2 touch)
    {

        if (dotsTrail == null || dotsTrail.Count < 2)
            return;

        foreach (var d in dotsPool)
            d.SetActive(false);


        bulletProgress = 0.0f;

        bullet.SetType((BubbleScript.BUBBLE_TYPE)typeOfBalls);
       
        bullet.gameObject.SetActive(true);

        bullet.transform.position = transform.position;

        InitPath();


        SetNextType();
    }

    void HandleTouchMove(Vector2 touch)
    {


        if (bullet.gameObject.activeSelf)
            return;

        if (dotsTrail == null)
        {
            return;
        }

        dotsTrail.Clear();

        foreach (var d in dotsPool)
            d.SetActive(false);


        Vector2 point = Camera.main.ScreenToWorldPoint(touch);

        var direction = new Vector2(point.x - transform.position.x, point.y - transform.position.y);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null && hit.collider.tag != "WalNulls")
        {

            dotsTrail.Add(transform.position);

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

        dotsTrail.Add(previousHit.point);


        var normal = Mathf.Atan2(previousHit.normal.y, previousHit.normal.x);

        var newDirection = normal + (normal - Mathf.Atan2(directionIn.y, directionIn.x));

        var reflection = new Vector2(-Mathf.Cos(newDirection), -Mathf.Sin(newDirection));

        var newCastPoint = previousHit.point + (2 * reflection);

        //		directionIn.Normalize ();
        //		newCastPoint = new Vector2(previousHit.point.x + 2 * (-directionIn.x), previousHit.point.y + 2 * (directionIn.y));
        //		reflection = new Vector2 (-directionIn.x, directionIn.y);

        var hit2 = Physics2D.Raycast(newCastPoint, reflection);
        if (hit2.collider != null)
        {
            if (hit2.collider.tag == "SideWalls")
            {
                //shoot another raycast
                DoRayCast(hit2, reflection);
            }
            else
            {
                dotsTrail.Add(hit2.point);

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

        if (bullet.gameObject.activeSelf)
        {

            bulletProgress += bulletIncrement;

            if (bulletProgress > 1)
            {
                dotsTrail.RemoveAt(0);
                if (dotsTrail.Count < 2)
                {
                    bullet.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    InitPath();
                }
            }

            var px = dotsTrail[0].x + bulletProgress * (dotsTrail[1].x - dotsTrail[0].x);
            var py = dotsTrail[0].y + bulletProgress * (dotsTrail[1].y - dotsTrail[0].y);

            bullet.transform.position = new Vector2(px, py);

            return;
        }

        if (dotsTrail == null)
            return;

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
                HandleTouchMove(touch.position);
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

    void DrawPaths()
    {

        if (dotsTrail.Count > 1)
        {

            foreach (var d in dotsPool)
                d.SetActive(false);

            int index = 0;

            for (var i = 1; i < dotsTrail.Count; i++)
            {
                DrawSubPath(i - 1, i, ref index);
            }
        }
    }

    void DrawSubPath(int start, int end, ref int index)
    {
        var pathLength = Vector2.Distance(dotsTrail[start], dotsTrail[end]);


        int numDots = Mathf.RoundToInt((float)pathLength / dotGap);

        float dotProgress = 1.0f / numDots;

        var p = 0.0f;

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
        var start = dotsTrail[0];

        var end = dotsTrail[1];

        var length = Vector2.Distance(start, end);

        var iterations = length / 0.30f; // Adjust how fast you want the bubble to move

        bulletProgress = 0.0f;

        bulletIncrement = 1.0f / iterations;
    }

}
