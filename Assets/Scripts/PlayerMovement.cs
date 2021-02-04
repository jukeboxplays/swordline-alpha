using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

//------------------------





//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL
//USO DESCONTINUADO 25-JUL





//------------------------

public class PlayerMovement : MonoBehaviour
{
    //CharacterController charControl;
    private Vector3 pos_D;

    private Vector3 pos;
    private float vel;

    private Vector3 rigPos;
    private Quaternion rigRot;

    private Transform gravity;

    public GameObject ActionManager;
    private VRInputs vrIn;

    public GameObject GravityCube;
    private CapsuleCollider capsCollider;
    private GameObject p_Head;

    private float distanceToGround;

    //public GameObject testCube;

    private Rigidbody rb;

    void Start()
    {
        vrIn = ActionManager.GetComponent<VRInputs>();
        vel = 2.0f;
        //collider = this.gameObject.GetComponent<CapsuleCollider>();
        //p_Head = vrIn.t_Head.GetComponent<GameObject>();

        //p_Head = GameObject.FindGameObjectWithTag("MainCamera");
        //collider = p_Head.GetComponent<CapsuleCollider>();

        capsCollider = GameObject.FindGameObjectWithTag("Collider").GetComponent<CapsuleCollider>();
        gravity = GravityCube.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //RotationHandler();
        HeightHandler();
        TranslateControl();
        GravityHandler();
    }

    void RotationHandler()
    {
        //ROTAÇÃO*
        rigPos = vrIn.t_Rig.position;
        rigRot = vrIn.t_Rig.rotation;

        transform.eulerAngles = new Vector3(0, vrIn.t_Head.eulerAngles.y, 0);

        vrIn.t_Rig.position = rigPos;
        vrIn.t_Rig.rotation = rigRot;
        //*Trava rotacao de GameObject filho, porém rotaciona a si mesmo

        capsCollider.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

    }

    void HeightHandler()
    {
        //collider.height = vrIn.t_Head.transform.position.y; //LEGAL PARA FAZER MOVIMENTAÇÃO FÍSICA HIPER-AUMENTADA
        //collider.center = new Vector3(-vrIn.t_Head.transform.position.x, vrIn.t_Head.transform.position.y, -vrIn.t_Head.transform.position.z);

        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(vrIn.t_Head.position, -Vector3.up, Color.green);
        if (Physics.Raycast(vrIn.t_Head.position, -Vector3.up, out hit))
        {
            distanceToGround = hit.distance;

            capsCollider.height = distanceToGround;
            capsCollider.center = new Vector3(0.0f, -distanceToGround / 2.0f, 0.0f);
            //collider.height = distanceToGround;
        }
    }

    void TranslateControl()
    {
        //MOVIMENTACAO RETILINEA
        pos = (vrIn.touchpadValue * vel) * Time.deltaTime;
        pos_D = (
            new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"))
            * vel) * Time.deltaTime;

        //Debug.Log()


        this.gameObject.transform.Translate(pos.x, 0, pos.y);
        this.gameObject.transform.Translate(pos_D.x, 0, pos_D.y);

        //Debug.Log("Touchpad: " + pos);
        ////Debug.Log(p_Head.transform.position.x);
    }

    void GravityHandler()
    {
        gravity.position = new Vector3(capsCollider.transform.position.x, gravity.position.y, capsCollider.transform.position.z);
        this.transform.position = new Vector3(this.transform.position.x, gravity.position.y, this.transform.position.z);

        /*
			!!!! TO-DO !!!!
			-Layer do cubo e Layer do player precisam de ajustes
		*/
    }

}
