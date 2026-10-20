using UnityEngine;
using UnityEngine.UI;

public class PhotoDisplayer : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;

    void Start()
    {
        if (PhotoManager.sprites[0] != null)
            image1.sprite = PhotoManager.sprites[0];

        if (PhotoManager.sprites[1] != null)
            image2.sprite = PhotoManager.sprites[1];

        if (PhotoManager.sprites[2] != null)
            image3.sprite = PhotoManager.sprites[2];

        if (PhotoManager.sprites[3] != null)
            image4.sprite = PhotoManager.sprites[3];
    }
}