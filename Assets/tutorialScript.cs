using UnityEngine;
using System.Collections;

public class tutorialScript : MonoBehaviour {

	public float lifeTime = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0) {
			Destroy (gameObject);
		}
	}
}
