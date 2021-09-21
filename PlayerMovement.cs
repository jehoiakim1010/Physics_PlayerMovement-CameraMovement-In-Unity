using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to Player
public class PlayerMovement : MonoBehaviour
{
    RigidBody rb;
    //Set The Speed in the Inspector Window
    [SerializedField] float PlayerSpeed;
    Vector3 PlayerMovement;


    void Start()
    {
        //Get The RigidBody Component
        //The Player Object must have Rigidbody Component
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //This will let the player to move
        PlayerMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        //I call the Function PlayerMoveRB with the parameter of the PlayerMove to gain the Direction
        PlayerMoveRB(PlayerMove);
    }

    void PlayerMoveRB(Vector3 Direction)
    {
        //I Set The Direction to the Camera Rotation
        Direction = Camera.main.transform.rotation * Direction;

        //this will let the player move with the direction and speed
        rb.MovePosition(transform.position + (Direction * PlayerSpeed * Time.deltaTime));

        //Add Force Down to let the player prevent flying because this game object using Gravity
        rb.AddForce(-Vector3.up, ForceMode.Impulse);
    }
}