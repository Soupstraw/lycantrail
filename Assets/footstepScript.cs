using UnityEngine;
using System.Collections;

public class footstepScript : MonoBehaviour {

	public float rate = 0.2f;

	bool pitch = true;
	float timePassed;

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed > rate) {
			timePassed = 0;
			if (pitch) {
				source.pitch = 0.9f;
				source.Play ();
			} else {
				source.pitch = 1.1f;
				source.Play ();
			}
			pitch = !pitch;
		}
	}
}
