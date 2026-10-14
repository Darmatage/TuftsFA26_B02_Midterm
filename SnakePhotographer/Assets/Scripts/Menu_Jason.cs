using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Menu_Jason : MonoBehaviour
{

	public static bool MenuOpen = false;
    public GameObject MenuUI;
	public bool isPauseMenu = false;

	public AnimationCurve curveMove = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	public float elapsedMove = 0f;
	public float startOffset = 400f;
	public Vector3 onScreenPos;
	public Vector3 offScreenPos;
	public Vector3 thePosition;
	
	public bool moveDown = false;
	public bool moveUp = false;

   void Start(){
		MenuUI.SetActive(true);
		MenuOpen = false;

		onScreenPos = MenuUI.transform.position; //save the on-screen destination
		offScreenPos = MenuUI.transform.position;
		offScreenPos.y += startOffset;
		thePosition = offScreenPos;
		MenuUI.transform.position = thePosition; //move to the off-screen start position
    }

    void Update(){
        if (Input.GetKeyDown("p")){
			if (MenuOpen){ CloseMenu(); }
			else{ OpenMenu(); }
		}
    }

//NOTE: This function is for the open menu button
	public void OpenMenu(){
		if (!MenuOpen){
			MenuOpen = true;
			//Unlock cursor
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			//activate tween down:
			moveDown = true;
			moveUp = false;
		}
		else { 
			CloseMenu(); 
			}
	}

	public void CloseMenu(){
		if (isPauseMenu){Time.timeScale = 1f;}
		MenuOpen = false;
		//Lock cursor, if needed:
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
		
		//activate tween up:
		moveDown = false;
		moveUp = true;
	}

	void PauseTime(){
		Time.timeScale = 0f;
	}

	void FixedUpdate () {
		// Tween Move Down Onto Screen:
		if (moveDown){
			if (thePosition.y > onScreenPos.y){
				thePosition.y -= curveMove.Evaluate(elapsedMove) * startOffset;
				MenuUI.transform.position = thePosition;
				elapsedMove += (Time.deltaTime / 5f);
			}
			else {
				MenuUI.transform.position = onScreenPos;
				if (isPauseMenu){PauseTime();}
				}
		}

		// Tween Move UP Off Screen:
		if (moveUp){
			if(thePosition.y < offScreenPos.y){
				thePosition.y += curveMove.Evaluate(elapsedMove) * startOffset;
				MenuUI.transform.position = thePosition;
				elapsedMove -= (Time.deltaTime / 5f);
			}
			else {
				MenuUI.transform.position = offScreenPos;
				}
		}
	}

}
