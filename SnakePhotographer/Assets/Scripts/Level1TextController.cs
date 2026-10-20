using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1TextController : MonoBehaviour
{
    public TMP_Text startText;
    public TMP_Text distanceText;

    public GameObject nextLevelText;

    public GameHandler gameHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startText != null)
            Invoke("HideStartText", 20f);
        if (distanceText != null)
            Invoke("HideDistanceText", 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.snakeIsHit) {
            nextLevelText.SetActive(true);
        }

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
