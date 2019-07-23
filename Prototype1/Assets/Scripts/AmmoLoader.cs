using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLoader : MonoBehaviour
{
    public List<GameObject> ammoTypes;

    public GameObject tempBubble; //temporary until number of bubble types have been confirmed

    public Queue<GameObject> ammoList;

    

    GameObject Bubble;

    // Start is called before the first frame update
    void Start()
    {

        ammoList = new Queue<GameObject>();

        for (int i = 0; i <= 5; i++) {

           Bubble = Instantiate(tempBubble,new Vector3((float) -i,0,0),Quaternion.identity);

           ammoList.Enqueue(Bubble);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            useAmmo();

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            addAmmo();

        }
    }

    public void addAmmo() {

        Bubble = Instantiate(tempBubble, new Vector3(-(float) ammoList.Count, 0, 0), Quaternion.identity);

        ammoList.Enqueue(Bubble);

    }

    public void useAmmo() {

        Bubble = (GameObject)ammoList.Dequeue();

        Destroy(Bubble);

        updateAmmoPos();

    }

    public void updateAmmoPos() {

        GameObject[] sortingList = new GameObject[ammoList.Count];

        ammoList.CopyTo(sortingList,0);

        for (int i = 0; i < sortingList.Length; i++) {
            sortingList[i].transform.position = new Vector2(-(float)i,0);
        }

    }
}
