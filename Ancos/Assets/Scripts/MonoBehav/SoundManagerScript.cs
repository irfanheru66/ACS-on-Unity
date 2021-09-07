using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManagerScript : MonoBehaviour
{
    public static AudioSource AS;
    public static AudioClip bellSound,congratsSound;

    private void Start()
    {
        bellSound = Resources.Load<AudioClip>("RZFWLXE-bell-hop-bell");
        congratsSound = Resources.Load<AudioClip>("15893197_congratulations-and-applause_by_wexdexflow_preview");
        AS = GetComponent<AudioSource>();
    }
    public static void ringBell()
    {
        
        AS.PlayOneShot(bellSound);
        
    }
    public static void ringCongrats()
    {
        AS.PlayOneShot(congratsSound);
    }
}
