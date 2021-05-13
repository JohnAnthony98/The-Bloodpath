using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private Renderer cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.color = Color.black;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cubeRenderer.material.color = Color.green;
        }
    }
}
