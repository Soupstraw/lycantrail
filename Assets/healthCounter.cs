using UnityEngine;
using System.Collections;

public class healthCounter : MonoBehaviour {

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(globalData.health/100f, 1, 1);
	}
}
