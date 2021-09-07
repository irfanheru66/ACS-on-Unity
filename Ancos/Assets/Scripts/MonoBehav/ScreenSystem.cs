using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSystem : MonoBehaviour
{
    public static int _fixnumAnt;

    public void Simulation()
    {
        if (SliderSystem._numAnt < 2)
        {
            _fixnumAnt = 2;
        }
        else
        {
            _fixnumAnt = SliderSystem._numAnt;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void Home()
    {
        SceneManager.LoadScene(0); 
    }
}
