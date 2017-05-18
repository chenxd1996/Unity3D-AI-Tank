using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour {
	private float hp; //生命值

	public float getHp() {
		return hp;
	}

	public void setHp(float hp) {
		this.hp = hp; 
	}

}

