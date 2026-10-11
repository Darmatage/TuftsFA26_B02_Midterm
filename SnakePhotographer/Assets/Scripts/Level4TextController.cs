using UnityEngine;
using UnityEngine.UI;
public class Level4TextController : MonoBehaviour
{
    public Text flashlightText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("HideFlashlightText", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HideFlashlightText()
    {
        flashlightText.gameObject.SetActive(false);
    }
}
