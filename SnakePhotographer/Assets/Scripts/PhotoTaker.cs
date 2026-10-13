/* Controls the camera that take photos.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class PhotoTaker : MonoBehaviour
{
    Transform photoCamera_T;
    Camera photoCamera;
    public Transform playerCamera;
    Ray ray;
    public float maxDetectDistance = 100;
    public DistanceSliderController distanceMeter; //to reference another script
    public SliderControllerLevel3 wildcatDistanceMeter;
    public Text nextLevelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //disables the camera 
        photoCamera_T = transform.Find("PhotoCamera");
        photoCamera = photoCamera_T.GetComponent<Camera>();

        disableCamera();

        nextLevelText.enabled = false;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //playerCamera.forward * distance from camera
        transform.position = playerCamera.position + (playerCamera.forward * 0.5f);
        
        //match the rotation of where the player is looking
        transform.rotation = playerCamera.rotation; 

    }

    public bool enableCamera() 
    {
        photoCamera_T.gameObject.SetActive(true); 
        transform.Find("Screen").gameObject.SetActive(true);
        transform.Find("ScreenBlack").gameObject.SetActive(false);
        return true;
    }

    public bool disableCamera() 
    {
        photoCamera_T.gameObject.SetActive(false); 
        transform.Find("Screen").gameObject.SetActive(false);
        transform.Find("ScreenBlack").gameObject.SetActive(true);
        return false;
    }

    public bool takePhoto() 
    {
        //returns whether the slider is in the green
        if (distanceMeter == null || !distanceMeter.CanTakePicture)
        {
            Debug.Log("Cannot take photo! Get to the correct distance.");
            return false;
        }

        //only check if it's actually assigned
        if (wildcatDistanceMeter != null &&
        !wildcatDistanceMeter.CanTakePicture)
        {
            Debug.Log("Wildcat slider is not green!");
            return false;
        }

        //shows whether the snake is in the picture while the slider is green
        bool snakeDetected = detectSnake();

        if (snakeDetected) {
            //make the photo
            Debug.Log("Photo taken!");
            //need to load win message
            nextLevelText.enabled = true;
        } else {
            //do whatever needs to be done when snake is not detected.
            Debug.Log("Snake not detected!");
        }   

        return snakeDetected;
    }

    //returns true if raycast hit snake, false if it hit anything else
    private bool detectSnake() 
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDetectDistance))
        {
            Debug.Log(hit.collider.gameObject.name + "was hit!");
            if (hit.collider.gameObject.tag == "Snake") 
            {
                Debug.Log("You hit the snake!");
                return true;
            } 
            else 
            {
                return false;
            }
        }

        return false;
    }
}
