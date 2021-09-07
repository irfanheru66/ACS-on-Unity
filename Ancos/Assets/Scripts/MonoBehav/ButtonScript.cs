using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{


    // Update is called once per frame
    public void Fasttwo()
    {
        TimeManagerScript.timeScale = 2;
    }

    public void normal()
    {
        TimeManagerScript.timeScale = 1;
    }

    public void pause()
    {
        TimeManagerScript.timeScale = 0;
    }

    public void restart()
    {
        SceneManager.LoadScene("PEMSIS");
    }
}
