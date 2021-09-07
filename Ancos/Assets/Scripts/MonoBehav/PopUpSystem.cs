using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpSetting;
    public GameObject popUpHome;
    public bool a = false;
    // public Animator animator;
    // public TMP_Text popUpText;

    public void PopUpSetting()
    {
        if (a == false)
        {
            a = true;
            popUpSetting.SetActive(true);
        }
        else if (a == true)
        {
            a = false;
            popUpSetting.SetActive(false);
        }
    }

    public void PopUpHome()
    {
        if (a == false)
        {
            a = true;
            popUpHome.SetActive(true);
        }
        else if (a == true)
        {
            a = false;
            popUpHome.SetActive(false);
        }
    }
}
