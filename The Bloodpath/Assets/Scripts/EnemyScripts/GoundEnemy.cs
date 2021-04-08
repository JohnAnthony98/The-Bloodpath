using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundEnemy : MonoBehaviour
{
    public GameObject enemyObject;
    private bool findingRightBound;
    private bool findingLeftBound;
    private Vector3 rightBound;
    private Vector3 leftBound;
    private float travelTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (findingRightBound)
        {

        }
        else if (findingLeftBound)
        {

        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(this);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerTesting>().ResetMoves();
        }
    }

    private void OnDestroy()
    {
        Destroy(enemyObject);
    }
}
