using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadAction; //pos. touchpad: TouchpadTouch
    public float vel = 4.0f;
    CapsuleCollider capsCollider;
    Vector3 direction;
    float distanceToGround;
    public LayerMask layerIgnore;
   
    //Transform t_Rig;
    Transform t_Head;

    void Start()
    {
        //t_Rig = SteamVR_Render.Top().origin;
        t_Head = SteamVR_Render.Top().head;
        capsCollider = this.GetComponent<CapsuleCollider>();
    }

    void Update() {
        CharOffsetHandler();
        //HeightHandler();
        TranslateControl();
    }

    void HeightHandler()
    {
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(t_Head.position, Vector3.down, Color.red);

        //if (Physics.Raycast(t_Head.position, -Vector3.up, out hit))
        if (Physics.Raycast(t_Head.position, Vector3.down, out hit, layerIgnore))
        {

            //Debug.Log("Hit: " + hit.distance);
            distanceToGround = hit.distance;
            //distanceToGround = Vector3.Distance(t_Head.position, hit.transform.position) - 1.0f;

            Debug.Log("D1: " + distanceToGround);
            Debug.Log("D2: " + (Vector3.Distance(t_Head.position, hit.transform.position) - 1.0f));


            capsCollider.height = distanceToGround;
            capsCollider.center = new Vector3(0.0f, distanceToGround / 2.0f, 0.0f);
            //capsCollider.center = new Vector3(t_Head.localPosition.x, distanceToGround / 2.0f, t_Head.localPosition.z);
        }
    }
    void CharOffsetHandler()
    {
        capsCollider.center = new Vector3(t_Head.localPosition.x, capsCollider.center.y, t_Head.localPosition.z);
    }
    void TranslateControl()
    {
        //TOUCHPAD
        direction = t_Head.TransformDirection(new Vector3(touchpadAction.axis.x, 0.0f, touchpadAction.axis.y));        
        transform.Translate((direction * vel) * Time.deltaTime);
        //Debug.Log(touchpadAction.axis.x + "," + touchpadAction.axis.y);

        //KEYBOARD
        Vector3 directionKB = t_Head.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
        transform.Translate((directionKB * vel) * Time.deltaTime);
    }

}
