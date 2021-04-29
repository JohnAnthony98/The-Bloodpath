using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float dash_force;
    public float sg_force;
    public float walkSpeed;
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

    private List<GameObject> killedEnemies;

    public int health;
    private int maxHealth;
    private float impactForce;
    private float stagger;



    // Start is called before the first frame update
    void Start()
    {
        checkpoint = transform.position;
        dash_force = 12f;
        sg_force = 6f;
        walkSpeed = 0.5f;
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

        killedEnemies = new List<GameObject>();
        maxHealth = 2;
        health = maxHealth;
        impactForce = 3f;
        stagger = 0.1f;
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
                this.GetComponent<Collider>().isTrigger = false;
                if (onGround == true)
                {
                    ResetMoves();
                }
            }
        }
    }

    private void Respawn()
    {
        health = maxHealth;
        LoadCheckpoint();
    }

    private void Facing()
    {
        if (Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.5)
        {
            BodyFacing.x = -1;
        }
        else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.5)
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
        if (Input.GetButton("dash") && dashesLeft > 0)
        {
            this.GetComponent<Collider>().isTrigger = true;
            /*Debug.Log("Dash");
            moveable = false;
            move_time = Time.time;*/
            if (Input.GetKey("w") || Input.GetAxis("Vertical") < -0.5)
            {
                //jumping
                if (Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.5)
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * -1, dash_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.5)
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
            else if (Input.GetKey("s") || Input.GetAxis("Vertical") > 0.5)
            {
                //jumping
                if (Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.5)
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(dash_force * -1, dash_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(dashPreFab) as GameObject;
                }
                else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.5)
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
        if (Input.GetButton("shotgun") && blastsLeft > 0)
        {
            this.GetComponent<Collider>().isTrigger = true;
            /*Debug.Log("Dash");
            moveable = false;
            move_time = Time.time;*/
            if (Input.GetKey("w") || Input.GetAxis("Vertical") < -0.5)
            {
                //jumping
                if (Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.5)
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * 1, sg_force * -1, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, 1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.5)
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
            else if (Input.GetKey("s") || Input.GetAxis("Vertical") > 0.5)
            {
                //jumping
                if (Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.5)
                {
                    rbody.velocity = new Vector3(0, 0, 0);
                    rbody.useGravity = false;
                    rbody.AddForce(sg_force * 1, sg_force, 0, ForceMode.Impulse);
                    moveable = false;
                    move_time = Time.time;
                    BodyFacing = new Vector2(-1, -1);
                    attack = Instantiate(shotgunPreFab) as GameObject;
                }
                else if (Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.5)
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

        if ((Input.GetAxis("Horizontal") < -0.5 || Input.GetKey("a")) && moveable)
        {
            Vector3 vel = rbody.velocity;
            vel.x = 0;
            rbody.velocity = vel;
            rbody.AddForce(-1 * walkSpeed, 0, 0, ForceMode.Impulse);
        }
        if ((Input.GetAxis("Horizontal") > 0.5 || Input.GetKey("d")) && moveable)
        {
            Vector3 vel = rbody.velocity;
            vel.x = 0;
            rbody.velocity = vel;
            rbody.AddForce(walkSpeed, 0, 0, ForceMode.Impulse);
        }
    }

    private void SetCheckpoint()
    {
        foreach (GameObject go in killedEnemies)
        {
            Destroy(go);
        }
        killedEnemies.Clear();
    }

    private void LoadCheckpoint()
    {
        transform.position = checkpoint;
        foreach (GameObject go in killedEnemies)
        {
            go.SetActive(true);
        }
        killedEnemies.Clear();
    }

    public void EnemyDestroyed(GameObject go)
    {
        killedEnemies.Add(go);
        ResetMoves();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            ResetMoves();
            onGround = true;
        }
        if ((collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Spike") && moveable)
        {
            health--;
            if(health <= 0)
            {
                Respawn();
                ResetMoves();
                return;
            }
            //move player away from enemy 
            if(collision.transform.position.x > this.transform.position.x)
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(impactForce * -1, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time + (move_cooldown - stagger);
                BodyFacing.y = 0;
            }
            else
            {
                rbody.velocity = new Vector3(0, 0, 0);
                rbody.AddForce(impactForce, 0, 0, ForceMode.Impulse);
                moveable = false;
                move_time = Time.time + (move_cooldown - stagger);
                BodyFacing.y = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player crosses a checkpoint, update the checkpoint variable, so the player respawns there upon death
        if (other.gameObject.CompareTag("checkpoint"))
        {
            checkpoint = other.gameObject.transform.position;
            SetCheckpoint();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameController.gameController.PlayerWins();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ground")
        {
            ResetMoves();
            onGround = true;
            this.GetComponent<Collider>().isTrigger = false;
        }

        if (other.gameObject.tag == "Wall")
        {
            this.GetComponent<Collider>().isTrigger = false;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
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
