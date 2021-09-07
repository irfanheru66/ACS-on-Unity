using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
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
}
