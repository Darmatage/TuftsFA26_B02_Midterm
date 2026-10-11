using UnityEngine;
using UnityEngine.UI;

public class Level1TextController : MonoBehaviour
{
    public Text startText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("HideStartText", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }
}
