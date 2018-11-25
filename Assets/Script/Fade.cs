using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public PlayerControl alice;
    public float FadeRate;
    private Image image;
    void Start() {
        this.image = this.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if(alice.getHP() <= 0) {
            image.color = new Color (0.0f, 0.0f, 0.0f, Mathf.Lerp (image.color.a, 1.0f, Time.deltaTime * FadeRate));
        }
    }
}
