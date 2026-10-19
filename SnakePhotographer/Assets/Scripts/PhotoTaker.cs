/* Controls the camera that take photos.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.IO;
public class PhotoTaker : MonoBehaviour
{
    Transform photoCamera_T;

    //the camera that takes the photos
    Camera photoCamera;

    //the main camera of the player
    public Transform playerCamera;
    Ray ray;
    [SerializeField] float maxDetectDistance = 100;
    public DistanceSliderController distanceMeter; //to reference another script
    public SliderControllerLevel3 wildcatDistanceMeter;
    public Text nextLevelText;
    


    //where the taken image goes
    [SerializeField] Image[] photos = new Image[4];
    int photoCount = 0;

    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //disables the camera 
        photoCamera_T = transform.Find("PhotoCamera");
        photoCamera = photoCamera_T.GetComponent<Camera>();

        disableCamera();


        //nextLevelText.enabled = false;
        
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


    //Be Carefule with this, some stuff that seems useless are actually 
    //required for the code to function.
    private IEnumerator makePhoto() 
    {
        yield return new WaitForEndOfFrame();

        photoCamera.Render();

        RenderTexture previousActive = RenderTexture.active;

        RenderTexture rt = photoCamera.targetTexture;
        RenderTexture.active = rt;
        
        Texture2D texture2D = new Texture2D(rt.width, rt.height, 
            TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previousActive;
        
        Sprite pictureSprite = Sprite.Create(texture2D, 
            new Rect(0,0, texture2D.width, texture2D.height), 
            new Vector2(0.5f,0.5f));

        photos[photoCount].sprite = pictureSprite;

        if (photoCount < 3) {
            photoCount++;
        } else if (photoCount == 3) {
            photoCount = 0;
        }
    }

    //returns a bool that tells if the snake is detected and the distance is 
    //correct
    //takes a photo, and puts that photo into a canvas image object
    public bool takePhoto() 
    {

        StartCoroutine(makePhoto());
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
            FindFirstObjectByType<GameHandler>().RegisterSnakePhoto();
            //need to load win message
            //nextLevelText.enabled = true;
        } else {
            //do whatever needs to be done when snake is not detected.
            Debug.Log("Snake not detected!");
        }   

        return snakeDetected;
    }

    //returns true if raycast hit snake, false if it hit anything else
    //please don't change this code
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
