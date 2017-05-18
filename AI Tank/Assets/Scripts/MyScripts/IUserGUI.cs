using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUserGUI : MonoBehaviour {
	IUserAction action;
	// Use this for initialization
	void Start () {
		action = GameDirector.getInstance().currentSceneController as IUserAction;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!action.isGameOver()) {
			if (Input.GetKey(KeyCode.W)) {
				action.moveForward();
			}
			
			if (Input.GetKey(KeyCode.S)) {
				action.moveBackWard();
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				action.shoot();	
			}

			float offsetX = Input.GetAxis ("Horizontal");//获取水平轴上的增量，目的在于控制玩家坦克的转向
			action.turn(offsetX);
		}
	}
}
