using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    private void Update() {
        if(Input.GetButtonDown("Start")) {
            ScoreController.Reset();
            NextLevel.currentLevel = 1;
            Application.LoadLevel(0);
        }
    }
}
