using UnityEngine;
using System.Collections;

public class sky : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (new Vector3(0, 0, Time.time));
	}
}
