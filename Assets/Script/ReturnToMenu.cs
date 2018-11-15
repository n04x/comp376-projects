using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    private void Update() {
        if(Input.GetButtonDown("Start")) {
            Application.LoadLevel(0);
        }
    }
}
