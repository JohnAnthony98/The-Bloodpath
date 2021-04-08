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
    void Start()
    {
        attackTime = 0.25f;
        startTime = Time.time;

        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerDir = player.GetComponent<PlayerTesting>().GetFacing();

        //figure out placement and direction of attack based on player direction
        if(playerDir.y == 1)//Attack upwards
        {
            if(playerDir.x == 1)//diagonal up and to the right
            {
                posOffset = new Vector3(0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else if (playerDir.x == -1)//diagonal up and to the left
            {
                posOffset = new Vector3(-0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else //stright up
            {
                posOffset = new Vector3(0, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else if (playerDir.y == -1)//Attack Downwards
        {
            if (playerDir.x == 1)//diagonal down and to the right
            {
                posOffset = new Vector3(0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else if (playerDir.x == -1)//diagonal down and to the left
            {
                posOffset = new Vector3(-0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else //stright down
            {
                posOffset = new Vector3(0, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else //attack to the sides
        {
            if (playerDir.x == 1)//diagonal down and to the right
            {
                posOffset = new Vector3(0.5f, 0f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (playerDir.x == -1)//diagonal down and to the left
            {
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
