﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMoveChecker : MonoBehaviour
{
    private GameObject attatchedEnemy = null;
    private bool scaleSet;
    private bool findingRightBound;
    private bool findingLeftBound;
    // Start is called before the first frame update
    void Start()
    {
        findingRightBound = true;
        findingLeftBound = false;
        scaleSet = false;
        if(attatchedEnemy != null)
        {
            this.gameObject.transform.localScale = attatchedEnemy.transform.localScale;
            scaleSet = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attatchedEnemy == null)
        {
            return;
        }
        if (scaleSet == false)
        {
            this.gameObject.transform.localScale = attatchedEnemy.transform.localScale;
            scaleSet = true;
        }

        if (findingRightBound)
        {
            Vector3 newPos = attatchedEnemy.transform.position;
            newPos.x += transform.localScale.x;
            transform.position = newPos;
        }
        else if (findingLeftBound)
        {
            Vector3 newPos = attatchedEnemy.transform.position;
            newPos.x -= transform.localScale.x;
            transform.position = newPos;
        }
    }

    public void SetEnemy(GameObject enemy)
    {
        attatchedEnemy = enemy;
    }

    public void SwapBound()
    {
        if (findingRightBound)
        {
            findingRightBound = false;
            findingLeftBound = true;
        }
        else
        {
            findingRightBound = true;
            findingLeftBound = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            attatchedEnemy.GetComponent<GroundEnemy>().SetBound();
            SwapBound();
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
