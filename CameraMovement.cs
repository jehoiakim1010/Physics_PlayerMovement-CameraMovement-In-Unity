using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to Camera
public class CameraMovement : MonoBehaviour
{
    Transform Player, Target;

    //Set THe Sensitivity in Inspector Window
    [SerializeField] private float MouseSensitivity;

    float MouseX;
    float MouseY;

    void Start()
    {
        //Create A New GameObject that name into Player_Target 
        //The Target GameObject must be at the head of the player
        //I Named The Target GameObject as Player_Target
        Target = GameObject.FindWithTag("Player").transform.find("Player_Target").transform;

        //Find The Player using Tag
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        //Call The Function CameraMove
        CameraMove();
    }

    public void CameraMove()
    {
        //If The Player Pressed The Right Mouse Button
        if (Input.GetMouseButton(1))
        {
            //The Main Camera field Of View will be decreseased
            Camera.main.fieldOfView -= zoomRate * Time.deltaTime;
        }
        else
        {
            //The Main Camera field Of View will be Increased
            Camera.main.fieldOfView += zoomRate * Time.deltaTime;
        }

        //I Set the limit of the fieldOfView to 20 to 60
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20, 60);

        //Set The MouseX into the Input Mouse X and Y
        MouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        MouseY -= Input.GetAxis("Mouse Y") * MouseSensitivity;

        //Limit the Mouse Y to prevent rotating the camera
        MouseY = Mathf.Clamp(MouseY, -80f, 40f);

        //Set this Camera To look at the Target
        transform.LookAt(Target);

        //Set The Rotation of the Camera with smooth using Slerp
        //The Quarternion.Euler let the player to control the camera
        Target.rotation = Quaternion.Slerp(Target.transform.rotation, Quaternion.Euler(MouseY, MouseX, 0.0f), MouseSensitivity * Time.deltaTime);

        //Set The Rotation of the Player with smooth using Slerp
        //The Quarternion.Euler let The player gameobject to Rotate when Depend on the Camera Forward
        Player.rotation = Quaternion.Slerp(Target.transform.rotation, Quaternion.Euler(0.0f, MouseY, 0.0f), MouseSensitivity * Time.deltaTime);
    }
}