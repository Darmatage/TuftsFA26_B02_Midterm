using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CameraController : MonoBehaviour
{
    public float mouseSens;
    public Transform playerTransform;

    float xRotation;
    float yRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //locks the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        transform.position = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);


        //update camera transform location
        Vector3 playerPosition = playerTransform.position;
        playerPosition.y += 0.9f;
        transform.position = playerPosition;
        
    }
}
