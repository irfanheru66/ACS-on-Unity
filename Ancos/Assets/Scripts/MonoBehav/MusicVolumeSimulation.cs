using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeSimulation : MonoBehaviour
{
    void Start()
    {
        MusicSystem.Instance.gameObject.GetComponent<AudioSource>().volume = 0.1f;
    }

}
