using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
    public static int timeScale = 1;
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
