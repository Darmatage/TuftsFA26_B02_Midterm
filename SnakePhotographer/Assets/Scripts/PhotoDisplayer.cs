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
        if (PhotoManager.sprite1 != null)
            image1.sprite = PhotoManager.sprite1;

        if (PhotoManager.sprite2 != null)
            image2.sprite = PhotoManager.sprite2;

        if (PhotoManager.sprite3 != null)
            image3.sprite = PhotoManager.sprite3;

        if (PhotoManager.sprite4 != null)
            image4.sprite = PhotoManager.sprite4;
    }
}