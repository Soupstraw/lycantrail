using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class global : MonoBehaviour {

	public Texture2D cursor;
	public float startTime = 120.0f;
	public float timeLeft = 120.0f;
	public float movementSpeed = 1.0f;
	public float normalMovementSpeed = 1.0f;
	public float boostBonus = 1.0f;
	public int gold = 0;
	public float health = 100.0f;
	public float healthMax = 100.0f;
	public bool pause = false;
	public float food = 100.0f;
	public float foodMax = 100.0f;
	public float foodConsumptionRate = 20.0f;
	public float difficulty = 1.0f;

	public int level = 0;

	public float fireCooldown = 0.5f;
	public float reloadCooldown = 1.0f;
	public float damage = 5.0f;
	public float knockback = 0.1f;

	public int healCost = 3;
	public int foodCost = 3;
	public int cdCost = 10;
	public int dmgCost = 10;
	public int hpCost = 10;
	public int punchCost = 10;

	public float healVal = 40.0f;
	public float foodVal = 20.0f;
	public float cdVal = 0.1f;
	public float dmgVal = 3.0f;
	public float hpVal = 10.0f;
	public float punchVal = 0.04f;

	public int cdLvl = 1;
	public int dmgLvl = 1;
	public int hpLvl = 1;
	public int punchLvl = 1;

	float windowDelay = 3.0f;

	bool gameOver = false;

	float startDelay = 6.0f;
	bool started = false;

	public GameObject daytimePanel;

	// Use this for initialization
	void Start () {
		Cursor.SetCursor (cursor, new Vector2(3, 3), CursorMode.Auto);
	}

	public void StartLevel(){
		level++;
		difficulty = level;
		timeLeft = startTime;
		pause = false;
		daytimePanel.SetActive (false);
		GameObject.Find ("gameOverText").GetComponent<Text>().text = "Night " + level;
		GameObject.Find ("gameOverText").GetComponent<Text>().enabled = true;
		GameObject.Find ("carriage").GetComponent<carriageControl> ().enabled = true;
		GameObject.Find ("shooter").GetComponent<shooting> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			startDelay -= Time.deltaTime;
			if (startDelay < 0) {
				started = true;
				StartLevel ();
			}
		}

		if (Input.GetButtonDown ("Exit")) {
			Application.Quit ();
		}

		if (!pause) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < startTime - 3.0f) {
				GameObject.Find ("gameOverText").GetComponent<Text> ().enabled = false;
			}

			if (timeLeft <= 0) {
				Pause ();
				windowDelay = 3.0f;
			}
		} else if (!gameOver && started) {
			windowDelay -= Time.deltaTime;
			if (windowDelay < 0) {
				daytimePanel.SetActive (true);
				UpdateMenuButtons ();
			}
		} else if(Input.GetButtonDown("Reload") && started){
			Application.LoadLevel (0);
		}
	}

	void Pause(){
		GameObject.Find ("carriage").GetComponent<carriageControl> ().enabled = false;
		GameObject.Find ("shooter").GetComponent<shooting> ().enabled = false;
		GameObject.Find ("shooter").GetComponent<LineRenderer> ().enabled = false;
		pause = true;
		movementSpeed = normalMovementSpeed;
	}

	void UpdateMenuButtons(){

		cdCost = 10 * cdLvl;
		dmgCost = 10 * dmgLvl;
		hpCost = 10 * hpLvl;
		punchCost = 10 * punchLvl;

		GameObject.Find("restText").GetComponent<Text>().text = "Rest\n-" + healCost + "gp +" + healVal + "HP";
		GameObject.Find("foodText").GetComponent<Text>().text = "Refill Boost\n-" + foodCost + "gp +" + healVal + "BOOST";
		GameObject.Find("cdText").GetComponent<Text>().text = "Upgrade Reload Speed Lv." + (cdLvl + 1) + "\n-" + cdCost + "gp +" + cdVal + "RS";
		GameObject.Find("dmgText").GetComponent<Text>().text = "Upgrade Damage Lv." + (dmgLvl + 1) + "\n-" + dmgCost + "gp +" + dmgVal + "DMG";
		GameObject.Find("hpText").GetComponent<Text>().text = "Upgrade Max HP Lv." + (hpLvl + 1) + "\n-" + hpCost + "gp +" + hpVal + "MAX HP";
		GameObject.Find("punchText").GetComponent<Text>().text = "Upgrade Knockback Lv." + (punchLvl + 1) + "\n-" + punchCost + "gp +" + punchVal + "KB";
	}

	public void GameOver(){
		gameOver = true;
		Pause ();
		movementSpeed = 0;
		GameObject.Find ("gameOverText").GetComponent<Text>().text = "Game Over\nPress reload to restart\n'Esc' to exit game";
		GameObject.Find ("gameOverText").GetComponent<Text>().enabled = true;
		Camera.main.GetComponent<AudioListener> ().enabled = false;
	}

	public void Rest(){
		if (healCost <= gold) {
			gold -= healCost;
			health += healVal;
		}
		UpdateMenuButtons ();
	}

	public void BuyFood(){
		if (foodCost <= gold) {
			gold -= foodCost;
			food += foodVal;
		}
		UpdateMenuButtons ();
	}

	public void UpgradeCD(){
		if (cdCost <= gold) {
			gold -= cdCost;
			reloadCooldown -= cdVal;
			cdLvl++;
		}
		UpdateMenuButtons ();
	}

	public void UpgradeDamage(){
		if (dmgCost <= gold) {
			gold -= dmgCost;
			damage += dmgVal;
			dmgLvl++;
		}
		UpdateMenuButtons ();
	}

	public void UpgradeHP(){
		if (hpCost <= gold) {
			gold -= hpCost;
			healthMax += hpVal;
			hpLvl++;
		}
		UpdateMenuButtons ();
	}

	public void UpgradeKnockback(){
		if (punchCost <= gold) {
			gold -= punchCost;
			knockback += punchVal;
			punchLvl++;
		}
		UpdateMenuButtons ();
	}
}
