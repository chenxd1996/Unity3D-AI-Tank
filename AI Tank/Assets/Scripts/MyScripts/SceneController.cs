using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneController : MonoBehaviour, IUserAction {
	public GameObject player;//玩家坦克

	private bool gameOver = false;//游戏是否结束 

	private int enemyConut = 6;//游戏的npc数量
	private myFactory mF;//工厂
	

	void Awake() {//一些初始的设置
		GameDirector director = GameDirector.getInstance();
		director.currentSceneController = this;
		mF = Singleton<myFactory>.Instance;
		player = mF.getPlayer();
	}
	void Start () {
		for (int i = 0; i < enemyConut; i++) {//获取npc坦克
			mF.getTank();
		}
		Player.destroyEvent += setGameOver;//如果玩家坦克被摧毁，则设置游戏结束
	}
	
	void Update () {
		// 相机跟随玩家坦克
		Camera.main.transform.position = new Vector3(player.transform.position.x, 15, player.transform.position.z);		
	}

	public Vector3 getPlayerPos() {//返回玩家坦克的位置
		return player.transform.position;
	}

	public bool isGameOver() {//返回游戏是否结束
		return gameOver;
	}
	public void setGameOver() {//设置游戏结束
		gameOver = true;
	}

	public void moveForward() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
	}
	public void moveBackWard() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
	}
	public void turn(float offsetX) {//通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
		float x = player.transform.localEulerAngles.y + offsetX * 5;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3 (y, x, 0);
	}	

	public void shoot() {
		GameObject bullet = mF.getBullet(tankType.Player);//获取子弹，传入的参数表示发出子弹的坦克类型
		bullet.transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) +
			player.transform.forward * 1.5f;//设置子弹位置
		bullet.transform.forward = player.transform.forward;//设置子弹方向
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);//发射子弹
	}

}
