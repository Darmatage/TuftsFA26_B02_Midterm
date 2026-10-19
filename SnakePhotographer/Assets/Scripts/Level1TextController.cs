using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1TextController : MonoBehaviour
{
    public TMP_Text startText;
    public TMP_Text distanceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("HideStartText", 7f);
        Invoke("HideDistanceText", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    private void HideDistanceText()
    {
        distanceText.gameObject.SetActive(false);
    }
}
