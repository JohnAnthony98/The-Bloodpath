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
    private float speed;

    private GameObject moveCheckerPreFab;
    private GameObject moveChecker;


    // Start is called before the first frame update
    void Start()
    {
        foundGround = false;
        findingRightBound = false;
        findingLeftBound = false;
        moveCheckerPreFab = Resources.Load("PreFabs/Enemies/GroundEnemyMoveChecker") as GameObject;
        speed = 0.05f;
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
            newPos.x += speed;
            transform.position = newPos;
        }
        else if (findingLeftBound)
        {
            //move left
            Vector3 newPos = transform.position;
            newPos.x -= speed;
            transform.position = newPos;
            startTime = Time.time;
        }
    }

    public void SetBound()
    {
        if (findingRightBound)
        {
            findingRightBound = false;
            findingLeftBound = true;
        }
        else if (findingLeftBound)
        {
            findingLeftBound = false;
            findingRightBound = true;
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetBound();
            moveChecker.GetComponent<GroundEnemyMoveChecker>().SwapBound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            this.gameObject.SetActive(false);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().EnemyDestroyed(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(moveChecker);
        Destroy(this.gameObject);
    }
}
