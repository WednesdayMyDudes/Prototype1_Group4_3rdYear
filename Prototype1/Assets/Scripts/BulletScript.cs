using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject[] colorsGO;

    public BallScript.BALL_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetType(BallScript.BALL_TYPE type)
    {

        foreach (var go in colorsGO)
        {
            go.SetActive(false);
        }

        this.type = type;

        Debug.Log((int)type);

        colorsGO[(int)type].SetActive(true);
    }

}

