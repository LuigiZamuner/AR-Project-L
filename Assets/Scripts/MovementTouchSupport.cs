using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementTouchSupport : MonoBehaviour
{
    public int buttonIndex = 0;

    //buttons sup
    public void HandlePokeballModeButton()
    {
        buttonIndex = 1;
    }
    public void HandleRotateModeButton()
    {
        buttonIndex = 2;
    }
    public void HandleQuit()
    {
        Application.Quit();
    }
}
