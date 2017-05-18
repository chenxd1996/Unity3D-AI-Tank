using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tankType:int{Player, Enemy}
public class myFactory : MonoBehaviour {//工厂
	public GameObject player;//玩家坦克
	public GameObject tank;//npc坦克
	public GameObject bullet;//子弹
	public ParticleSystem pS;//爆炸粒子系统

	private Dictionary<int, GameObject> usingTanks;
	private Dictionary<int, GameObject> freeTanks;
	private Dictionary<int, GameObject> usingBullets;
	private Dictionary<int, GameObject> freeBullets;

	private List<ParticleSystem> psContainer;

	void Awake() {
		usingTanks = new Dictionary<int, GameObject>();
		freeTanks = new Dictionary<int, GameObject>();
		usingBullets = new Dictionary<int, GameObject>();
		freeBullets = new Dictionary<int, GameObject>();
		psContainer = new List<ParticleSystem>();
	}

	void Start() {
		Enemy.recycleEvent += recycleTank;//npc坦克被摧毁时，会执行这个委托函数
	}
				
	public GameObject getPlayer() {//获取玩家坦克
		return player;
	}

	public GameObject getTank() {//获取npc坦克
		if (freeTanks.Count == 0) {
			GameObject newTank = Instantiate<GameObject>(tank);
			usingTanks.Add(newTank.GetInstanceID(), newTank);
			//在一个随机范围内设置坦克位置
			newTank.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
			return newTank;
		}
		foreach (KeyValuePair<int, GameObject> pair in freeTanks) {
			pair.Value.SetActive(true);
			freeTanks.Remove(pair.Key);
			usingTanks.Add(pair.Key, pair.Value);
			pair.Value.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
			return pair.Value;
		}
		return null;
	}	

	public GameObject getBullet(tankType type) {
		if (freeBullets.Count == 0) {
			GameObject newBullet = Instantiate(bullet);
			newBullet.GetComponent<Bullet>().setTankType(type);
			usingBullets.Add(newBullet.GetInstanceID(), newBullet);
			return newBullet;
		}
		foreach (KeyValuePair<int, GameObject> pair in freeBullets) {
			pair.Value.SetActive(true);
			pair.Value.GetComponent<Bullet>().setTankType(type);
			freeBullets.Remove(pair.Key);
			usingBullets.Add(pair.Key, pair.Value);
			return pair.Value;
		}
		return null;
	}

	public ParticleSystem getPs() {
		for (int i = 0; i < psContainer.Count; i++) {
			if (!psContainer[i].isPlaying) {
				return psContainer[i];
			}
		}
		ParticleSystem newPs = Instantiate<ParticleSystem>(pS);
		psContainer.Add(newPs);
		return newPs;
	}

	public void recycleTank(GameObject tank) {
		usingTanks.Remove(tank.GetInstanceID());
		freeTanks.Add(tank.GetInstanceID(), tank);
		tank.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		tank.SetActive(false);
	}

	public void recycleBullet(GameObject bullet) {
		usingBullets.Remove(bullet.GetInstanceID());
		freeBullets.Add(bullet.GetInstanceID(), bullet);
		bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		bullet.SetActive(false);
	}
	
}
