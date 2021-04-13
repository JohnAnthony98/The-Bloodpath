﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float dash_force;
    private float sg_force;
    private float deathBarrier = -20;
    private bool moveable;
    private float move_time;
    private float move_cooldown;
    private int maxDashes;
    private int dashesLeft;
    private int maxBlasts;
    private int blastsLeft;
    private bool onGround;
    private Rigidbody rbody;
    private Vector2 BodyFacing;
    private Vector3 checkpoint;

    private GameObject dashPreFab;
    private GameObject shotgunPreFab;


    // Start is called before the first frame update
    void Start()
    {
        checkpoint = transform.position;
        dash_force = 12f;
        sg_force = 6f;
        moveable = true;
        move_cooldown = 0.25f;
        rbody = GetComponent<Rigidbody>();
        BodyFacing = new Vector2(1, 0);
        onGround = true;

        maxDashes = 2;
        dashesLeft = maxDashes;
        maxBlasts = 1;
        blastsLeft = maxBlasts;

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
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.useGravity = true;
                if(onGround == true)
                {
                    ResetMoves();
                }
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
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameController.gameController.PlayerWins();
            Destroy(other.gameObject);
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
        if(GameController.gameController != null)
        {
            if (GameController.gameController.GamePaused())
            {
                return;
            }
        }
        GameObject attack;
        if (Input.GetKey("k") && dashesLeft > 0)
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
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * -1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * 1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
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
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * -1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * 1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
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
                rbody.useGravity = false;
                rbody.AddForce(dash_force * BodyFacing.x, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
                BodyFacing.y = 0;
                attack = Instantiate(dashPreFab) as GameObject;
            }
            dashesLeft--;
        }
        if (Input.GetKey("l") && blastsLeft > 0)
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
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * 1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * -1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
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
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * 1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d"))
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * -1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(1, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
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
                rbody.useGravity = false;
                rbody.AddForce(sg_force * BodyFacing.x * -1, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time;
                BodyFacing.y = 0;
                attack = Instantiate(shotgunPreFab) as GameObject;
            }
            blastsLeft--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            ResetMoves();
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = false;
        }
    }

    public Vector2 GetFacing()
    {
        return BodyFacing;
    }

    public void ResetMoves()
    {
        dashesLeft = maxDashes;
        blastsLeft = maxBlasts;
    }
}
