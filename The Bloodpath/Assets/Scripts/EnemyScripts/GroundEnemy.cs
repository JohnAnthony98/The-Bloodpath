using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public bool findingRightBound;
    public bool findingLeftBound;
    private bool foundGround;
    private Vector3 rightBound;
    private Vector3 leftBound;
    private float travelTime;
    private float startTime;
    private float speed;
    private SpriteRenderer sprite;
    private Color defaultColor;

    private GameObject moveCheckerPreFab;
    private GameObject moveChecker;

    private int health;
    private int maxHealth;

    private bool playerHit;
    private float timeHit;
    private float pauseTime;
    private Vector3 hitLocation;
    private Vector3 orginPos;


    // Start is called before the first frame update
    void Start()
    { 
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        defaultColor = sprite.color;

        foundGround = false;
        findingRightBound = false;
        findingLeftBound = false;
        moveCheckerPreFab = Resources.Load("PreFabs/Enemies/GroundEnemyMoveChecker") as GameObject;
        speed = 0.05f;

        playerHit = false;
        pauseTime = 1f;

        maxHealth = 2;
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerHit)
        {
            Move();
        }
        else
        {
            if (Time.time - timeHit > pauseTime)
            {
                playerHit = false;
            }
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.position = hitLocation;
        }
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
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        else if (findingLeftBound)
        {
            //move left
            Vector3 newPos = transform.position;
            newPos.x -= speed;
            transform.position = newPos;
            startTime = Time.time;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("enemy"))
        {
            SetBound();
            moveChecker.GetComponent<GroundEnemyMoveChecker>().SwapBound();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHit = true;
            timeHit = Time.time;
            hitLocation = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            if (foundGround == false)
            {
                orginPos = this.transform.position;
                foundGround = true;
                findingRightBound = true;
                moveChecker = Instantiate(moveCheckerPreFab);
                moveChecker.GetComponent<GroundEnemyMoveChecker>().SetEnemy(this.gameObject);
                return;
            }
        }
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            StartCoroutine(Flashing(0.5f));
            if(other.name == "DashAttack(Clone)")
            {
                health -= 1;
            }
            else
            {
                health -= 2;
            }
            if(health <= 0)
            {
                this.gameObject.SetActive(false);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerController>().EnemyDestroyed(this.gameObject);
                health = maxHealth;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetMoves();
            playerHit = true;
            timeHit = Time.time;
            hitLocation = transform.position;
        }
    }

    private IEnumerator Flashing(float flashTime)
    {
        sprite.color = Color.red;

        yield return new WaitForSeconds(flashTime);

        sprite.color = defaultColor;

        yield return new WaitForSeconds(flashTime);
    }

    private void OnDestroy()
    {
        Destroy(moveChecker);
        Destroy(this.gameObject);
    }

    public void ResetPos()
    {
        this.transform.position = orginPos;
        sprite.color = defaultColor;
    }
}
