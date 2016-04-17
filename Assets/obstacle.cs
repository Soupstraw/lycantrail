using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

	public AudioClip collisionSound;
	public AudioClip breakSound;

	public float speed = 0.8f;
	public float activateDistance = 0.2f;
	public bool destructible = false;
	public float damage = 20.0f;

	public GameObject[] drops;

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3 (globalData.movementSpeed * speed * Time.deltaTime, 0, 0);

		if (transform.position.x < -1.5f) {
			Destroy (gameObject);
		}

		GameObject player = GameObject.Find ("carriage");
		if (player.GetComponent<BoxCollider> ().bounds.Contains (GetComponent<BoxCollider2D>().bounds.center)) {
			if (tag == "Obstacle" && !globalData.pause) {
				player.GetComponent<carriageControl> ().Damage (damage);
			} else if (tag == "Collectible") {
				GetComponent<collectible> ().Collect ();
			}
			GameObject.Find("carriage").GetComponent<AudioSource>().PlayOneShot (collisionSound);

			Destroy (gameObject);
		}
	}

	public void Kill(){
		if (destructible) {
			GameObject.Find("carriage").GetComponent<AudioSource>().PlayOneShot (breakSound);
			Instantiate (drops [Random.Range (0, drops.Length)], transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
