using UnityEngine;
using UnityEngine.UI;

public class PhotoDisplayer : MonoBehaviour
{
    private Image image1;
    private Image image2;
    private Image image3;
    private Image image4;

    void Start()
    {
        image1 = GetComponent<RawImage>();

        // Check if a photo was actually taken
        if (PhotoManager.CapturedPhoto != null)
        {
            rawImage.texture = PhotoManager.CapturedPhoto;
        }
        else
        {
            Debug.LogWarning("No photo found in PhotoManager!");
        }
    }
}