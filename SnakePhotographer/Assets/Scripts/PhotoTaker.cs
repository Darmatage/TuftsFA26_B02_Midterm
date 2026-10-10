/* Controls the camera that take photos.
*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PhotoTaker : MonoBehaviour
{
    Transform photoCamera_T;
    Camera photoCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //disables the camera 
        photoCamera_T = transform.Find("PhotoCamera");

        if (photoCamera_T != null)
        {
            photoCamera = photoCamera_T.GetComponent<Camera>();

            if (photoCamera != null)
            {
                photoCamera.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
