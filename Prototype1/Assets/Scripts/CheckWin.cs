using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{

    GameObject transitionHolder;
    TransitionManager manager;

    Scene currentScene;

    public GameObject winHolder;
    public GameObject loseHolder;

    // Start is called before the first frame update
    void Start()
    {
        transitionHolder = GameObject.Find("SceneManager");
        manager = transitionHolder.GetComponent<TransitionManager>();

        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callLoss() {

        loseHolder.SetActive(true);

        StartCoroutine(callRestart());
    }

    public void callCheck() {

        StartCoroutine(checkWin());

    }

    public IEnumerator checkWin()
    {

        yield return new WaitForSeconds(0.5f);

        int donuts = 0;
        int cupcakes = 0;
        int candy = 0;
        int sum = 0;


        donuts = GameObject.FindGameObjectsWithTag("Donut").Length;
        cupcakes = GameObject.FindGameObjectsWithTag("CupCake").Length;
        candy = GameObject.FindGameObjectsWithTag("Candy").Length;

        sum = donuts + cupcakes + candy;

        if (sum == 1)
        {

            // Debug.Log("win" + sum);

            winHolder.SetActive(true);

            if (currentScene.name.Equals("tutorial01_lvl1")) {

               
                StartCoroutine(callLevel2());
            }

            if (currentScene.name.Equals("tutorial02_lvl2")) {

                StartCoroutine(callMenu());
            }
        }

        else { Debug.Log("playing"); }
    }

    public IEnumerator callRestart() {

        yield return new WaitForSeconds(1f);

        manager.resetLevel();

    }

    public IEnumerator callLevel2() {
        //Play Victory sound
        FindObjectOfType<AudioManager>().Play("Win");
        yield return new WaitForSeconds(5f);        //Wait for 5 sec for the win sound for play

        manager.goToLevel2();

    }

    public IEnumerator callMenu() {

        yield return new WaitForSeconds(1f);

        manager.goToMenu();
    }

   
}
