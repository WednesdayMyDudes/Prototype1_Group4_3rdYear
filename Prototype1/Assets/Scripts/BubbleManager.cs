using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{

    public int level = 1;

    public List<GameObject> inGameBubbleList;

    public static BubbleManager Instance {

        get;
        private set;
    }

    bool win=false;

    void awake() {

        if (Instance == null) {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        inGameBubbleList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        inGameBubbleList.RemoveAll(item => item == null);

    }

   
}
