using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Find ("Gold").gameObject.GetComponent<Text> ().text = "Gold: " + globalData.gold;
	}
}
