using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour {

	public AudioClip shootingSound;
	public AudioClip reloadSound;

	public float cooldown = 0.5f;
	public float lineDuration = 0.2f;

	public int loadedAmmo = 5;
	public int clipSize = 5;
	public int ammoReserve = 20;

	LineRenderer line;
	AudioSource audio;

	float reloadCd = 0;
	float cd = 0;
	float cleanup = 0;

	bool reloading = false;

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
		line = gameObject.GetComponent<LineRenderer> ();
		audio = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		cd -= Time.deltaTime;
		reloadCd -= Time.deltaTime;
		cleanup -= Time.deltaTime;

		if (cleanup <= 0 && line.isVisible) {
			line.enabled = false;
		}

		if (Input.GetButtonDown ("Fire1") && cd <= 0 && loadedAmmo > 0) {
			reloading = false;
			reloadCd = reloadCd + 0.5f;
			loadedAmmo--;
			audio.PlayOneShot (shootingSound);

			Vector3 aimVector = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - gameObject.transform.position);
			aimVector.Scale (new Vector3 (1, 1, 0));
			aimVector.Normalize();
			aimVector *= 100;

			Vector3 scaled = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			scaled.Scale (new Vector3 (1, 1, 0));
			line.SetPosition (0, transform.FindChild("start").position);
			line.SetPosition (1, scaled);
			line.SetWidth(0.01f, 0.01f);
			line.enabled = true;

			RaycastHit2D hit = Physics2D.GetRayIntersection (Camera.main.ScreenPointToRay(Input.mousePosition));

			if (hit.collider != null && hit.collider.gameObject.tag == "Enemy") {
				wolfAI ai = hit.collider.gameObject.GetComponent<wolfAI> ();
				ai.Damage (globalData.damage + Random.Range(-globalData.damage * 0.5f, globalData.damage * 0.5f));
				ai.ApplyKnockback (aimVector.normalized * globalData.knockback);
			} else if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle") {
				obstacle obs = hit.collider.gameObject.GetComponent<obstacle> ();
				obs.Kill ();
			}
			cd = cooldown;
			cleanup = lineDuration;
		}

		if (reloading && reloadCd <= 0) {
			if (loadedAmmo == clipSize) {
				reloading = false;
			} else {
				audio.PlayOneShot (reloadSound);
				reloadCd = globalData.reloadCooldown;
				loadedAmmo++;
			}
		}

		if (Input.GetButtonDown ("Reload")) {
			reloading = true;
		}
	}
}
