using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bossAudio;
    AudioSource bgm;
    bool defaultMusic;
    bool bossRoom;
    bool toggled;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        defaultMusic = true;
        bossRoom = false;
        toggled = true;
    }

    void Update() {
        if(toggled) {
            if(bossRoom) {
                bgm.Stop();
                bgm.clip = bossAudio;
                bgm.Play();
                toggled = false;
                bossRoom = false;
            } else if(defaultMusic) {
                bgm.Play();
                toggled = false;
            }
        }    
    }

    public void EnterBossRoom() {
        toggled = true;
        bossRoom = true;
        defaultMusic = false;
    }
    
}
