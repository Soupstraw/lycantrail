using UnityEngine;
using System.Collections;

public class carriageControl : MonoBehaviour {

	public float verticalSpeed = 0.05f;
	public float horizontalSpeed = 0.1f;

	public AudioClip boostSound;

	public BoxCollider2D roadBound;

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveVec = new Vector3 (0, 0, 0);
		moveVec += new Vector3(0, 1, 0) * Input.GetAxis ("Vertical") * verticalSpeed;
		moveVec += new Vector3 (1, 0, 0) * Input.GetAxis ("Horizontal") * horizontalSpeed;
		transform.position += moveVec;

		Vector3 retVec = new Vector3 (0, 0, 0);

		if (transform.position.x < roadBound.bounds.min.x || transform.position.x > roadBound.bounds.max.x) {
			retVec += new Vector3 (-moveVec.x, 0, 0);
		}
		if (transform.position.y < roadBound.bounds.min.y || transform.position.y > roadBound.bounds.max.y) {
			retVec += new Vector3 (0, -moveVec.y, 0);
		}

		transform.position += retVec;

		if (Input.GetButton("Boost") && globalData.food > 0) {
			globalData.movementSpeed = globalData.normalMovementSpeed + globalData.boostBonus;
			globalData.food -= Time.deltaTime * globalData.foodConsumptionRate;
			if (Input.GetButtonDown("Boost")) {
				GetComponent<AudioSource> ().PlayOneShot (boostSound);
			}
		} else {
			globalData.movementSpeed = globalData.normalMovementSpeed;
		}
	}

	public void Damage(float damage){
		if (!globalData.pause) {
			globalData.health -= damage;
			if (globalData.health <= 0) {
				globalData.GameOver ();
			}
		}
	}
}
