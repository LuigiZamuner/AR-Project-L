using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



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
    public void HandlePet()
     {
        SceneManager.LoadScene("PetPokemon");
    }
    public void handleHome()
    {
    SceneManager.UnloadScene("PetPokemon");
    SceneManager.LoadScene("ArScene");
    }
}
