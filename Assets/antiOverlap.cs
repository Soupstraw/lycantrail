using UnityEngine;
using System.Collections;

public class antiOverlap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		newPos.Scale (new Vector3(1, 1, 0));
		newPos += new Vector3 (0, 0, 1 + newPos.y);
		transform.position = newPos;
	}
}
