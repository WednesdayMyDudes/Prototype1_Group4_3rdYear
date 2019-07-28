using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {

            Application.Quit();

        }
    }

    public void quitGame()
    {

        Application.Quit();

    }

    public void goToMenu()
    {

        SceneManager.LoadScene("Menu");

    }

    public void goToLevel1()
    {

        SceneManager.LoadScene("tutorial01_lvl1");

    }

    public void goToLevel2()
    {

        SceneManager.LoadScene("tutorial02_lvl2");

    }

    public void resetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
}
