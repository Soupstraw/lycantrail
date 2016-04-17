using UnityEngine;
using System.Collections;

public class wolfAI : MonoBehaviour {
	public GameObject[] drops;

	public AudioClip onHit;

	public GameObject target;
	public float speed = 2.0f;
	public float hitpointsMax = 3.0f;
	public float hitpoints = 3.0f;
	public float attackRange = 0.1f;

	public float dropChance = 1.0f;
	public int dropMin = 1;
	public int dropMax = 2;

	public float damage = 20.0f;
	public float weight = 1.0f;

	public float reboundTime = 1.0f;

	float rebound = 0f;

	global globalData;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
		hitpoints = hitpointsMax;
		transform.FindChild ("healthbarBack").localScale = new Vector3(0.02f * hitpointsMax, transform.FindChild ("healthbarBack").localScale.y, transform.FindChild ("healthbarBack").localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		rebound -= Time.deltaTime;
		transform.position -= new Vector3 (globalData.movementSpeed, 0, 0) * Time.deltaTime;
		if (rebound <= 0 && !globalData.pause) {
			transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * speed;
		}

		if ((target.transform.position - transform.position).magnitude <= attackRange && rebound <= 0) {
			target.GetComponent<carriageControl> ().Damage (damage);
			GetComponent<AudioSource> ().PlayOneShot (onHit);
			rebound = reboundTime;
		}

		if (transform.position.x < -1.6f) {
			Destroy (gameObject);
		}

		transform.FindChild ("healthbar").localScale = new Vector3(0.02f * hitpoints, transform.FindChild ("healthbar").localScale.y, transform.FindChild ("healthbar").localScale.z);
	}

	public void Damage(float damage){
		GetComponent<AudioSource> ().PlayOneShot (onHit);
		hitpoints -= damage;
		if (hitpoints <= 0) {
			Die ();
		}
	}

	public void ApplyKnockback(Vector3 knockback){
		transform.position += knockback/weight;
	}

	void Die(){
		if (Random.Range (0f, 1f) <= dropChance) {
			globalData.gold += Random.Range (dropMin, dropMax);
		}
		GameObject.Destroy (gameObject);
	}
}
