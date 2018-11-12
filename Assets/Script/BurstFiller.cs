using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFiller : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int meter;
    public Sprite[] fillers;
    public int index;
    private void Awake() {
        meter = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    // Might change it based on final consensus on how meter will work
    public void fillMeter(int[] card) {
        meter = card[index];
        float percentage = ( (float) meter / 21) * 100.0f;
        Debug.Log("fillMeter(): " + percentage);
        if(meter == 0) {
            spriteRenderer.sprite = null;
        } else if(percentage > 0.0f && percentage < 10.0f) {
            spriteRenderer.sprite = fillers[0];
        } else if(percentage >= 10.0f && percentage < 20.0f) {
            spriteRenderer.sprite = fillers[1];
        } else if(percentage >= 20.0f && percentage < 30.0f) {
            spriteRenderer.sprite = fillers[2];
        } else if(percentage >= 30.0f && percentage < 40.0f) {
            spriteRenderer.sprite = fillers[3];
        } else if(percentage >= 40.0f && percentage < 50.0f) {
            spriteRenderer.sprite = fillers[4];
        } else if(percentage >= 50.0f && percentage < 60.0f) {
            spriteRenderer.sprite = fillers[5];
        } else if(percentage >= 60.0f && percentage < 70.0f) {
            spriteRenderer.sprite = fillers[6];
        } else if(percentage >= 70.0f && percentage < 80.0f) {
            spriteRenderer.sprite = fillers[7];            
        } else if(percentage >= 80.0f && percentage < 90.0f) {
            spriteRenderer.sprite = fillers[8];
        } else if(percentage >= 90.0f) {
            spriteRenderer.sprite = fillers[9];
        }
    }
}
