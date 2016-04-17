using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour {

	public float scrollSpeed = 1f;
	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<MeshRenderer> ().material.mainTextureOffset += new Vector2(scrollSpeed * globalData.movementSpeed * Time.deltaTime, 0);
	}
}
