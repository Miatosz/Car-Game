using System;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public GameObject brakeLights;
    public GameObject dayTimeLight;
    public GameObject fogLight;
    public GameObject mainLight;
    public GameObject rearSideLight;
    public GameObject reverseLight;
    public GameObject turnSignalLeft;
    public GameObject turnSignalRight;

    public bool isBrakesLights;
    public bool indicator = false;


    private void Update()
    {
        InputHandler();
    }

    public void ReverseLight(bool turn)
    {
        if (turn)
        {
            reverseLight.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            reverseLight.GetComponent<MeshRenderer>().enabled = false;

        }
    }
    
    public void BrakeLight(bool brake)
    {
        
        if (brake)
        {
            brakeLights.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            brakeLights.GetComponent<MeshRenderer>().enabled = false;

        }
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnLights(mainLight);
            TurnLights(rearSideLight);
        }
        
        else if (Input.GetKeyDown(KeyCode.K))
        {
            indicator = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            TurnLights(turnSignalLeft);
        }
        
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            TurnLights(turnSignalRight);
        }
    }
    
    
    private void TurnLights(GameObject lights)
    {
        if (lights == turnSignalLeft || turnSignalRight)
        {
           
            
        }
        
        if (lights.GetComponent<MeshRenderer>().enabled == false)
        {
            lights.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            lights.GetComponent<MeshRenderer>().enabled = false;
        }

        
        
    }
}
