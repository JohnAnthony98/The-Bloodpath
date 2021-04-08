using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private bool findingRightBound;
    private bool findingLeftBound;
    private bool foundGround;
    private Vector3 rightBound;
    private Vector3 leftBound;
    private float travelTime;
    private float startTime;

    private GameObject moveCheckerPreFab;
    private GameObject moveChecker;


    // Start is called before the first frame update
    void Start()
    {
        foundGround = false;
        findingRightBound = false;
        findingLeftBound = false;
        moveCheckerPreFab = Resources.Load("PreFabs/Enemies/GroundEnemyMoveChecker") as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(foundGround == false)
        {
            return;
        }

        if (findingRightBound)
        {
            //move right
            Vector3 newPos = transform.position;
            newPos.x += 0.2f;
            transform.position = newPos;
        }
        else if (findingLeftBound)
        {
            //move left
            Vector3 newPos = transform.position;
            newPos.x -= 0.2f;
            transform.position = newPos;
            startTime = Time.time;
        }
        else
        {
            //move between two bounds
            float u = (Mathf.Sin(Time.time - startTime - (Mathf.PI/2)) + 1)  / 2;
            


            Vector3 newPos = (1 - u) * leftBound + u * rightBound;
            this.gameObject.transform.position = newPos;
        }
    }

    public void SetBound()
    {
        if (findingRightBound)
        {
            rightBound = transform.position;
            findingRightBound = false;
            findingLeftBound = true;
        }
        else if (findingLeftBound)
        {
            leftBound = transform.position;
            findingLeftBound = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if(foundGround == false)
            {
                foundGround = true;
                findingRightBound = true;
                moveChecker = Instantiate(moveCheckerPreFab);
                moveChecker.GetComponent<GroundEnemyMoveChecker>().SetEnemy(this.gameObject);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(this);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerTesting>().ResetMoves();
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
