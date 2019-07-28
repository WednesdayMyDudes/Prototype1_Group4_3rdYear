using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLoader : MonoBehaviour
{
    public List<GameObject> ammoTypes;

    public Queue<GameObject> ammoList;

    int selectNumber;

    GameObject currentBubble;
    GameObject newBubble;

    RyanBubble currentBullet;

    

    // Start is called before the first frame update
    public void setAmmo()
    {

        ammoList = new Queue<GameObject>();


        

          selectNumber = Random.Range(0, ammoTypes.Count);

          

          newBubble = Instantiate(ammoTypes[selectNumber], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

        currentBullet = newBubble.GetComponent<RyanBubble>();

        currentBullet.bubbleState = RyanBubble.BubbleState.Waiting;

           ammoList.Enqueue(newBubble);
        }
    

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Mouse0)) { test script for ammo loading and usage

            //useAmmo();

          //  addAmmo();

        //}
       

         


    }

    public void addAmmo() {
        selectNumber = Random.Range(0, ammoTypes.Count);

        newBubble = Instantiate(ammoTypes[selectNumber], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,gameObject.transform.position.z), Quaternion.identity);

        ammoList.Enqueue(newBubble);

       

    }

    public IEnumerator useAmmo() {

        yield return new WaitForSeconds(0.5f);

        currentBubble = (GameObject) ammoList.Dequeue();

       // updateAmmoPos();

       addAmmo();

       

    }

   

    }

