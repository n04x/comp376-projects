using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputCheck : MonoBehaviour
{
    Text output;
    void Start()
    {
        output = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string axes = "";
        string button = "";
        string leftie = "";
        string rightie = "";
        //Right Stick 
        float right_horizontal = Input.GetAxis("RHorizontal");
        float right_vertical = Input.GetAxis("RVertical");
        if (right_horizontal != 0 || right_vertical != 0)
        {
            rightie = "RStick(" + right_horizontal + ", " + right_vertical + ")";
        }

        //Left Stick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            leftie = "LStick(" + horizontal + ", " + vertical + ")";
        }



        //Dpad
        float dpad_horizontal = Input.GetAxis("DpadHorizontal");
        float dpad_vertical = Input.GetAxis("DpadVertical");
        if (dpad_horizontal != 0 || dpad_vertical != 0)
        {
            axes = "Dpad(" + dpad_horizontal + ", " + dpad_vertical + ")";
        }

        //Trigger
        float trigger = Input.GetAxis("Triggers");
        if (trigger != 0)
        {
            if (trigger < 0)
                axes = "LT (" + trigger + ")";
            else
                axes = "RT (" + trigger + ")";
        }

        //Button
        if (Input.GetButton("LB"))
        {

            button = "LB";

        }
        if (Input.GetButton("RB"))
        {

            button = "RB";

        }
        if (Input.GetButton("X"))
        {

            button = "X";

        }
        if (Input.GetButton("Y"))
        {

            button = "Y";

        }
        if (Input.GetButton("A"))
        {

            button = "A";

        }
        if (Input.GetButton("B"))
        {

            button = "B";

        }
        if (Input.GetButton("Start"))
        {

            button = "Start";

        }
        if (Input.GetButton("Back"))
        {

            button = "Back";

        }

        output.text = "Left Stick:"+leftie+ "\nRight Stick: " +rightie + "\nOther Axes:" + axes +"\nButton: " + button;
    }


}
