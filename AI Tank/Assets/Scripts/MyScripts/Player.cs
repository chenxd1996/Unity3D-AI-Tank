using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Tank {//玩家坦克
	public delegate void destroy();
	public static event destroy destroyEvent;

	void Start() {
		setHp(500f);//设置初始生命值为500
	}
	void Update () {
		if (getHp() <= 0 ) {//生命值<=0,表示玩家坦克被摧毁
			this.gameObject.SetActive(false);
			if (destroyEvent != null) {//执行委托事件
				destroyEvent();
			}
		}
	}
}

