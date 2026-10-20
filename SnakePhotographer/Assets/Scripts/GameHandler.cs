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

      [HideInInspector] public bool snakeIsHit = false;

      //saving game progress
      //temporarily disabled anything related to finishGameButton
      //public GameObject finishGameButton;

      public string[] levelSceneNames = { "Level1", "Level2", "Level3", "Level4" };
      public float timeRemaining = 60f;
      public TMP_Text timerText;
      public string loseSceneName = "LoseScene";
      public bool snakePhotoTaken = false;
      public bool runLevelTimer = true;

      private bool updatedLevel = false;

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
                              StartCoroutine(photoScript.takePhoto()); 
                              
                              //snakeIsHit will contain a bool that says if the snake 
                              //is captured in the photo or not
                        }
                  }

                  if (photoScript != null)
                        snakeIsHit = photoScript.snakeIsHit; 
                        
                  if (snakeIsHit && !updatedLevel) {
                        Debug.Log("Player Wins!");
                        //the player wins! 
                        LevelManager.levelsBeaten++;
                        updatedLevel = true;
                  }
            }

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

      public void StartGame() {
            // 1. First, load the actual gameplay level normally
            UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
      }

      // Return to MainMenu
      public void MainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
      }

      public void PhotoBook() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("PhotobookScene");
      }

      public void Map() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapScene");
      }

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
            //time needs to be set to 1 before setting pauseMenu Active
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

      }


      //checks whether all levels are completed and activates a button to finish the game
      public void CheckAllLevelsCompleted()
      {
            if (LevelManager.levelsBeaten == 4) {
                  //finishGameButton.SetActive(allCompleted);
            }
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