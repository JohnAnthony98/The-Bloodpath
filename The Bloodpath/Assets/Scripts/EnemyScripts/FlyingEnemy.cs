using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private bool findingTopBound;
    private bool findingBottomBound;
    private float speed;

    private Vector3 orgin;
    private Vector3 topBound;
    private Vector3 bottomBound;
    private float range;


    // Start is called before the first frame update
    void Start()
    {
        findingTopBound = false;
        findingBottomBound = true;
        speed = 0.03f;
        range = 1f;

        orgin = transform.position;
        topBound = orgin;
        topBound.y += range;
        bottomBound = orgin;
        bottomBound.y -= range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (findingBottomBound)
        {
            //move down
            Vector3 newPos = transform.position;
            newPos.y -= speed;
            newPos.x = orgin.x;
            newPos.z = orgin.z;
            transform.position = newPos;

            if (transform.position.y <= bottomBound.y)
            {
                ChangeDirection();
                return;
            }
        }
        else if (findingTopBound)
        {
            //move up
            Vector3 newPos = transform.position;
            newPos.y += speed;
            newPos.x = orgin.x;
            newPos.z = orgin.z;
            transform.position = newPos;

            if (transform.position.y >= topBound.y)
            {
                ChangeDirection();
                return;
            }
        }
    }

    private void ChangeDirection()
    {
        if (findingTopBound)
        {
            findingTopBound = false;
            findingBottomBound = true;
        }
        else if (findingBottomBound)
        {
            findingBottomBound = false;
            findingTopBound = true;
        }
    }

    private void SetBound()
    {
        if(transform.position.y > bottomBound.y)
        {
            //move lower bound to current position, then move upper bound higher
            float higher = transform.position.y - bottomBound.y;
            topBound.y += higher;
            bottomBound.y = transform.position.y;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            SetBound();
            ChangeDirection();
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
        Destroy(this.gameObject);
    }
}
