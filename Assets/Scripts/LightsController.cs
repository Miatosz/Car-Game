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

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnLights(mainLight);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            TurnLights(reverseLight);
        }
    }
    
    
    private void TurnLights(GameObject lights)
    {
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
