using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    private float attackTime;
    private float startTime;
    public GameObject attack;
    private GameObject player;
    private Vector3 posOffset;

    // Start is called before the first frame update
    void Awake()
    {
        attackTime = 0.25f;
        startTime = Time.time;

        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerDir = player.GetComponent<PlayerController>().GetFacing();

        //figure out placement and direction of attack based on player direction
        if(playerDir.y == 1)//Attack upwards
        {
            if(playerDir.x == 1)//diagonal up and to the right
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
                posOffset = new Vector3(0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else if (playerDir.x == -1)//diagonal up and to the left
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
                posOffset = new Vector3(-0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else //stright up
            {
                if(playerDir.z == 1)
                {
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipY = false;
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipY = true;
                }
                posOffset = new Vector3(0, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else if (playerDir.y == -1)//Attack Downwards
        {
            if (playerDir.x == 1)//diagonal down and to the right
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
                posOffset = new Vector3(0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else if (playerDir.x == -1)//diagonal down and to the left
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
                posOffset = new Vector3(-0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else //stright down
            {
                if (playerDir.z == 1)
                {
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipY = false;
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipY = true;
                }
                posOffset = new Vector3(0, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else //attack to the sides
        {
            if (playerDir.x == 1)//to the right
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
                posOffset = new Vector3(0.5f, 0f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (playerDir.x == -1)//to the left
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
                posOffset = new Vector3(-0.5f, 0f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }


        Vector3 newPos = player.transform.position;
        newPos += posOffset;
        this.gameObject.transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > attackTime)
        {
            Destroy(this);
        }
        Vector3 newPos = player.transform.position;
        newPos += posOffset;
        this.gameObject.transform.position = newPos;
    }

    protected void OnDestroy()
    {
        Destroy(attack);
    }
}
