using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DetectSnake : MonoBehaviour
{

    Ray ray;
    public float maxDetectDistance = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button pressed down!");
            if (Physics.Raycast(ray, out RaycastHit hit, maxDetectDistance))
            {
                Debug.Log(hit.collider.gameObject.name + "was hit!");
                if (hit.collider.gameObject.tag == "Snake") 
                {
                    Debug.Log("You hit the snake!");
                }
            }
        }
    }
}
