using UnityEngine;

public class MapLevelController : MonoBehaviour
{
    public GameObject[] levelButtons = new GameObject[4];
    public GameObject EndGameButton;
    void Start()
    {
        int lvsBeaten = LevelManager.levelsBeaten;

        if (lvsBeaten >= 4) {
            lvsBeaten--;
            EndGameButton.SetActive(true);
        } 

        for (int i = 0; i <= lvsBeaten; i++) {
            levelButtons[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
