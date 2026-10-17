using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      private GameObject PhotoCamera;
      public GameObject pauseMenu;
      private bool isPaused = false;

      private PhotoTaker photoScript;

      private bool photoCameraEnabled = false;

      private bool snakeIsHit = false;
/*    
      
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public TMP_Text healthText;

      public static int gotTokens = 0;
      public TMP_Text tokensText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;
      public static string lastLevelDied;  //allows replaying the Level where you died
*/
      void Start()
      {

            player = GameObject.FindWithTag("Player");
            if (player != null) {
                  PhotoCamera = player.transform.Find("Camera").gameObject;
                  photoScript = PhotoCamera.GetComponent<PhotoTaker>();

                  if (photoScript == null) {
                        Debug.Log("failed to get photoscript!");
                  }

                  pauseMenu.SetActive(false);
            }


            //PhotoCamera.GetComponent<PhotoTaker>();


            
		/*
            
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            //}
            updateStatsDisplay();
			*/
      }

      void Update() 
      {
            
            if (Input.GetMouseButtonDown(1)) // right click
            {
                  Debug.Log("Right mouse button pressed down!");
                  if (photoCameraEnabled) {
                        Debug.Log("disabling camera!");
                        photoCameraEnabled = photoScript.disableCamera();
                  } else {
                        Debug.Log("enabling camera!");
                        photoCameraEnabled = photoScript.enableCamera();
                  }
                        
            }

            if (Input.GetMouseButtonDown(0) && photoCameraEnabled) // left click
            {
                  Debug.Log("Left mouse button pressed down!");
                  if (photoCameraEnabled) {
                        snakeIsHit = photoScript.takePhoto(); 
                        //snakeIsHit will contain a bool that says if the snake 
                        //is captured in the photo or not
                  }
            }

            if (snakeIsHit) {
                  //the player wins! 
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                  //QuitGame();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
            if (isPaused)
                  ResumeGame();
            else
                  PauseGame();
            }

      }
/*


      public void playerGetTokens(int newTokens){
            gotTokens += newTokens;
            updateStatsDisplay();
      }

      public void playerGetHit(int damage){
           if (isDefending == false){
                  playerHealth -= damage;
                  if (playerHealth >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        //play GetHit animation:
                        player.GetComponent<PlayerHurt>().playerHit();
                  }
            }

           if (playerHealth > StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
                  updateStatsDisplay();
            }

           if (playerHealth <= 0){
                  playerHealth = 0;
                  updateStatsDisplay();
                  playerDies();
            }
      }

      public void updateStatsDisplay(){
            healthText.text = "HEALTH: " + playerHealth;
            tokensText.text = "GOLD: " + gotTokens;
      }

      public void playerDies(){
            player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            lastLevelDied = sceneName;       //allows replaying the Level where you died
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            player.GetComponent<PlayerMove>().isAlive = false;
            player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndLose");
      }
*/
      public void StartGame() {
        // 1. First, load the actual gameplay level normally
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        
        // 2. ADD THIS LINE: This loads your UI scene ON TOP of Level1 smoothly
        UnityEngine.SceneManagement.SceneManager.LoadScene("UIScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

      // Return to MainMenu
      public void MainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
             // Reset all static variables here, for new games:
            //playerHealth = StartPlayerHealth;
      }

      public void PhotoBook() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("PhotobookScene");
             // Reset all static variables here, for new games:
            //playerHealth = StartPlayerHealth;
      }

      public void Map() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapScene");
             // Reset all static variables here, for new games:
            //playerHealth = StartPlayerHealth;
      }

/*
      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }
	  */

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }

      public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    public void RestartLevel()
      {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }

      
} 