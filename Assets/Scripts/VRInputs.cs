using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRInputs : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadAction; //pos. touchpad: TouchpadTouch
    public Vector2 touchpadValue;

    public SteamVR_Action_Boolean pinchAction; //botao Trigger: Grab Pinch
    public bool pinchValue;

    public Transform t_Rig;
    public Transform t_Head;

    // Start is called before the first frame update
    void Start()
    {
        t_Rig = SteamVR_Render.Top().origin;
        t_Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);
        pinchValue = pinchAction.GetState(SteamVR_Input_Sources.Any);
        //Debug.Log("Touchpad: " + touchpadValue);
    }

}
