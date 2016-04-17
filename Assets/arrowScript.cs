using UnityEngine;
using System.Collections;

public class arrowScript : MonoBehaviour {

	public float blinkRate = 0.5f;
	public float lifeTime = 2.0f;

	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (Mathf.Floor (lifeTime / blinkRate) % 2 == 0) {
			rend.enabled = false;
		} else {
			rend.enabled = true;
		}
		if (lifeTime < 0) {
			Destroy (gameObject);
		}
	}
}
