using UnityEngine;
using System.Collections;

public class ammoCounter : MonoBehaviour {

	public shooting shooter;

	float zeroAmmo = 2.666f;
	float bulletWidth = 0.1333f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (zeroAmmo - shooter.loadedAmmo * bulletWidth, transform.position.y, transform.position.z);
	}
}
