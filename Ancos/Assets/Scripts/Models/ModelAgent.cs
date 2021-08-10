using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelAgent
{
    //isi index agent sama ancos si agent
    public int index;
    public ANCOS Ancos;
    public int kotaNow;

    public ModelAgent(int i, ANCOS ancos, int target) {
        this.index = i;
        this.Ancos = ancos;
        this.kotaNow = target;
    }
}
