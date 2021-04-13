using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    private float attackTime;
    private float startTime;
    public GameObject attack;
    private GameObject player;
    private Vector3 posOffset;


    private Vector3 startPos;
    private Vector3 endPos;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = 0.1f;
        startTime = Time.time;
        range = 1f;

        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerDir = player.GetComponent<PlayerController>().GetFacing();

        Vector3 newPos = player.transform.position;

        //figure out placement and direction of attack based on player direction
        if (playerDir.y == 1)//Attack upwards
        {
            if (playerDir.x == 1)//diagonal up and to the right
            {
                posOffset = new Vector3(0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
                endPos = new Vector3(range, range, 0);
            }
            else if (playerDir.x == -1)//diagonal up and to the left
            {
                posOffset = new Vector3(-0.5f, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
                endPos = new Vector3(-range, range, 0);
            }
            else //stright up
            {
                posOffset = new Vector3(0, 0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                endPos = new Vector3(0, range, 0);
            }
        }
        else if (playerDir.y == -1)//Attack Downwards
        {
            if (playerDir.x == 1)//diagonal down and to the right
            {
                posOffset = new Vector3(0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, -45);
                endPos = new Vector3(range, -range, 0);
            }
            else if (playerDir.x == -1)//diagonal down and to the left
            {
                posOffset = new Vector3(-0.5f, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 45);
                endPos = new Vector3(-range, -range, 0);
            }
            else //stright down
            {
                posOffset = new Vector3(0, -0.75f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                endPos = new Vector3(0, -range, 0);
            }
        }
        else //attack to the sides
        {
            if (playerDir.x == 1)//diagonal down and to the right
            {
                posOffset = new Vector3(0.5f, 0f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                endPos = new Vector3(range, 0, 0);
            }
            else if (playerDir.x == -1)//diagonal down and to the left
            {
                posOffset = new Vector3(-0.5f, 0f, 0);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                endPos = new Vector3(-range, 0, 0);
            }
        }

        startPos = newPos + posOffset;
        endPos += startPos;

        this.gameObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float u = (Time.time - startTime) / attackTime;
        if (u >= 1)
        {
            Destroy(this);
        }
        Vector3 newPos = (1 - u) * startPos + u * endPos;
        this.gameObject.transform.position = newPos;
    }

    protected void OnDestroy()
    {
        Destroy(attack);
    }
}
