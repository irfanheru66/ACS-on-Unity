using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public bool a = false;
    // public Animator animator;
    // public TMP_Text popUpText;

    public void PopUp()
    {
        if (a == false)
        {
            a = true;
            popUpBox.SetActive(true);
        }
        else if (a == true)
        {
            a = false;
            popUpBox.SetActive(false);
        }
        
        // popUpText.text = text;
        // animator.SetTrigger("pop");
    }
}
