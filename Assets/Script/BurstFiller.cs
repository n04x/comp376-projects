using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstFiller : MonoBehaviour
{
    Image fillImage;
    public GameObject burstImage;
    int meter;
    public Sprite[] fillers;
    public int index;
    bool blackjack;
    private void Awake() {
        blackjack = false;
        meter = 0;
        fillImage = GetComponent<Image>();   
        fillImage.enabled = false;    
    }

    public void fillMeter(int[] card) {
        meter = card[index];
        float percentage = ( (float) meter / 21) * 100.0f;
        //Debug.Log("fillMeter(): " + percentage);
        if(blackjack) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[9];
        }else if(meter == 0) { 
            fillImage.enabled = false;
        } else if(percentage > 0.0f && percentage < 10.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[0];
        } else if(percentage >= 10.0f && percentage < 20.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[1];
        } else if(percentage >= 20.0f && percentage < 30.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[2];
        } else if(percentage >= 30.0f && percentage < 40.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[3];
        } else if(percentage >= 40.0f && percentage < 50.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[4];
        } else if(percentage >= 50.0f && percentage < 60.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[5];
        } else if(percentage >= 60.0f && percentage < 70.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[6];
        } else if(percentage >= 70.0f && percentage < 80.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[7];            
        } else if(percentage >= 80.0f && percentage < 90.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[8];
        } else if(percentage >= 90.0f) {
            fillImage.enabled = true;
            fillImage.sprite = fillers[9];
        } 
    }
    public void SetBlackJack(bool b) {
        blackjack = b;
        burstImage.SetActive(b);
    }
}
