using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float dash_force;
    public float sg_force;
    private bool moveable;
    private float move_time;
    private float move_cooldown;
    private Rigidbody rbody;
    private int BodyFacing;


    // Start is called before the first frame update
    void Start()
    {
        dash_force = 7.195f;
        sg_force = 5f;
        moveable = true;
        move_cooldown = 0.5f;
        rbody = GetComponent<Rigidbody>();
        BodyFacing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
        if (moveable)
        {
            Move();
        }
        else
        {
            if(Time.time - move_time >= move_cooldown)
            {
                moveable = true;
            }
        }
    }

    private void Facing()
    {
        if (Input.GetKey("a"))
        {
            BodyFacing = -1;
        }
        else if (Input.GetKey("d"))
        {
            BodyFacing = 1;
        }
    }

    private void Move()
    {
        if (Input.GetKey("k"))
        {
            /*Debug.Log("Dash");
            moveable = false;
            move_time = Time.time;*/
            if (Input.GetKey("w"))
            {
                //jumping
                if (Input.GetKey("a"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * -1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * 1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
            }
            else if (Input.GetKey("s"))
            {
                //jumping
                if (Input.GetKey("a"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * -1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * 1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
            }
            else
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(dash_force * BodyFacing, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
            }
        }
        if (Input.GetKey("l"))
        {
            /*Debug.Log("Dash");
            moveable = false;
            move_time = Time.time;*/
            if (Input.GetKey("w"))
            {
                //jumping
                if (Input.GetKey("a"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * 1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * -1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
            }
            else if (Input.GetKey("s"))
            {
                //jumping
                if (Input.GetKey("a"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * 1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * -1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                }
            }
            else
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(sg_force * BodyFacing * -1, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
            }
        }
    }
}
