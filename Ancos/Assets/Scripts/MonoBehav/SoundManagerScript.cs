using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManagerScript : MonoBehaviour
{
    public static AudioSource AS;
    public static AudioClip bellSound;

    private void Start()
    {
        bellSound = Resources.Load<AudioClip>("RZFWLXE-bell-hop-bell");
        AS = GetComponent<AudioSource>();
    }
    public static void ringBell()
    {
        
        AS.PlayOneShot(bellSound);
        
    }
}
