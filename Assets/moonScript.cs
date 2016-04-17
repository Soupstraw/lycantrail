using UnityEngine;
using System.Collections;

public class moonScript : MonoBehaviour {

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = -(1f - 2f*globalData.timeLeft/globalData.startTime)*1.4f;
		float y = -0.4f * Mathf.Pow(x, 2) + 0.8f;
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
