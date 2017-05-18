using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float explosionRadius = 3f;//子弹的伤害半径
	private tankType type;//发射子弹的坦克类型
	void OnCollisionEnter(Collision other) {
		myFactory mF = Singleton<myFactory>.Instance;
		ParticleSystem explosion = mF.getPs();//获取爆炸的粒子系统
		explosion.transform.position = transform.position;//设置粒子系统位置
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);//获取爆炸范围内的所有碰撞体
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].tag == "tankPlayer" && this.type == tankType.Enemy 
				|| colliders[i].tag == "tankEnemy" && this.type == tankType.Player) {//发射子弹的坦克类型和被击中的坦克类型不同，才造成伤害
				float distance = Vector3.Distance(colliders[i].transform.position, transform.position);//被击中坦克与爆炸中心的距离
				float hurt = 100f / distance;//受到的伤害
				float current = colliders[i].GetComponent<Tank>().getHp();//当前的生命值
				colliders[i].GetComponent<Tank>().setHp(current - hurt);//设置受伤后生命值
			}
		}
		explosion.Play();//播放爆炸的粒子系统
		if (this.gameObject.activeSelf) {
			mF.recycleBullet(this.gameObject);//回收子弹
		}
		
	}

		
	public void setTankType(tankType type) {//设置发射子弹的坦克类型
		this.type = type;
	}
}