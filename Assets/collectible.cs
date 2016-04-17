using UnityEngine;
using System.Collections;

public class collectible : MonoBehaviour {

	public int goldValue = 0;
	public float healthValue = 0f;
	public float foodValue = 0f;

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Collect(){
		globalData.gold += goldValue;
		globalData.health += healthValue;
		if (globalData.health > globalData.healthMax) {
			globalData.health = globalData.healthMax;
		}
		globalData.food += foodValue;
		if (globalData.food > globalData.foodMax) {
			globalData.food = globalData.foodMax;
		}
		Destroy (gameObject);
	}
}
