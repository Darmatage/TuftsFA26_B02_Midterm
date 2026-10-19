using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      private GameObject PhotoCamera;
      public GameObject pauseMenu;
      private bool isPaused = false;

      private PhotoTaker photoScript;

      private bool photoCameraEnabled = false;

      private bool snakeIsHit = false;

      //saving game progress
      //temporarily disabled anything related to finishGameButton
      //public GameObject finishGameButton;

      public string[] levelSceneNames = { "Level1", "Level2", "Level3", "Level4" };
      public float timeRemaining = 60f;
      public TMP_Text timerText;
      public string loseSceneName = "LoseScene";
      public bool snakePhotoTaken = false;
      public bool runLevelTimer = true;
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

            SetCursorForScene();

            //check if all levels completed
            /*
            if (finishGameButton != null)
            {
                  CheckAllLevelsCompleted();
            }
            */

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
            if (isPaused == false) { 
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
            }

            /*
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                  QuitGame();
            }
            */

            if (Input.GetKeyDown(KeyCode.P))
            {
                  if (isPaused) {
                        ResumeGame();
                  } else {
                        PauseGame();
                  }
            }

            //changed this to button b to avoid conflict with pause
            if (Input.GetKeyDown(KeyCode.H))
            {
                  SceneManager.LoadScene("PhotobookScene");
            } else if (Input.GetKeyDown(KeyCode.M)) {
                  SceneManager.LoadScene("MapScene");
            } else if (Input.GetKeyDown(KeyCode.N))
            {
                  SceneManager.LoadScene("MainMenu");
            }

            UpdateTimer();

      }

      public void RegisterSnakePhoto()
      {
      snakePhotoTaken = true;
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
            
            Time.timeScale = 1f;
            isPaused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            pauseMenu.SetActive(false);
      }

      public void WinGame()
      {
            SceneManager.LoadScene("WinScene");
      }

      public void RestartLevel()
      {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }

      //marks current level as completed]
      //NEED TO ACTUALLY CONNECT TO STH THAT SIGNALS IT'S COMPLETED! PHOTO TAKEN?
      public void CompleteCurrentLevel()
      {
            string levelName = SceneManager.GetActiveScene().name;

            //save this level as completed
            PlayerPrefs.SetInt("Completed_" + levelName, 1);
            PlayerPrefs.Save();
      }


      //checks whether all levels are completed and activates a button to finish the game
      public void CheckAllLevelsCompleted()
      {
            bool allCompleted = true;

            foreach (string levelName in levelSceneNames)
            {
                  if (PlayerPrefs.GetInt("Completed_" + levelName, 0) == 0)
                  {
                  allCompleted = false;
                  break;
                  }
            }

            //finishGameButton.SetActive(allCompleted);
      }

      //adds and updates timer for each level when bool to have timer is on
      void UpdateTimer()
      {
            if (!runLevelTimer || snakePhotoTaken)
                  return;

            if (timeRemaining > 0) //if more than 0, continue counting
            {
                  timeRemaining -= Time.deltaTime;

                  if (timeRemaining < 0)
                        timeRemaining = 0;
            }

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            if (timerText != null)
            {
                  timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            if (timeRemaining <= 0)
            {
                  runLevelTimer = false;
                  Time.timeScale = 1f;
                  SceneManager.LoadScene(loseSceneName);
            }
      }

      //makes sure cursor only locks for gameplay levels
      void SetCursorForScene()
      {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (System.Array.Exists(levelSceneNames, level => level == sceneName))
            {
                  //Gameplay levels: lock and hide cursor
                  Cursor.lockState = CursorLockMode.Locked;
                  Cursor.visible = false;
            }
            else
            {
                  //Map, Lose, and other scenes: unlock and show cursor
                  Cursor.lockState = CursorLockMode.None;
                  Cursor.visible = true;
            }
      }

      
} 