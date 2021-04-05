﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float dash_force;
    public float sg_force;
    public float deathBarrier = -20;
    private bool moveable;
    private float move_time;
    private float move_cooldown;
    private Rigidbody rbody;
    private Vector2 BodyFacing;
    private Vector3 checkpoint;

    private GameObject dashPreFab;
    private GameObject shotgunPreFab;


    // Start is called before the first frame update
    void Start()
    {
        checkpoint = transform.position;
        dash_force = 7.195f;
        sg_force = 5f;
        moveable = true;
        move_cooldown = 0.5f;
        rbody = GetComponent<Rigidbody>();
        BodyFacing = new Vector2(1, 0);

        dashPreFab = Resources.Load("PreFabs/DashAttack") as GameObject;
        shotgunPreFab = Resources.Load("PreFabs/ShotgunBlast") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player has fallen below the death barrier, then reset them at thier last checkpoint
        if (transform.position.y < deathBarrier)
        {
            Respawn();
        }

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

    private void OnTriggerEnter(Collider other)
    {
        //if the player crosses a checkpoint, update the checkpoint variable, so the player respawns there upon death
        if (other.gameObject.CompareTag("checkpoint"))
        {
            checkpoint = other.gameObject.transform.position;
        }
    }

    private void Respawn()
    {
        transform.position = checkpoint;
    }

    private void Facing()
    {
        if (Input.GetKey("a"))
        {
            BodyFacing.x = -1;
        }
        else if (Input.GetKey("d"))
        {
            BodyFacing.x = 1;
        }
    }

    private void Move()
    {
        GameObject attack;
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
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * 1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(0, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
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
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(dash_force * 1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(0, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
            }
            else
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(dash_force * BodyFacing.x, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
                BodyFacing.y = 0;
                attack = Instantiate(dashPreFab) as GameObject;
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
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * -1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(0, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
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
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(sg_force * -1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.AddForce(0, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(0, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
            }
            else
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(sg_force * BodyFacing.x * -1, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
                BodyFacing.y = 0;
                attack = Instantiate(shotgunPreFab) as GameObject;
            }
        }
    }

    public Vector2 GetFacing()
    {
        return BodyFacing;
    }
}
