using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

	public GameObject[] spawnables;

	public GameObject[] obstacles;
	public GameObject warningArrow;
	public GameObject blueWarningArrow;

	public float yMin = -0.8f;
	public float yMax = -0.3f;

	float nextSpawn = 3.0f;
	float nextObstacle = 5.0f;

	global globalData;

	// Use this for initialization
	void Start () {
		globalData = GameObject.FindGameObjectWithTag ("Logic").GetComponent<global> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!globalData.pause) {
			nextSpawn -= Time.deltaTime;
			nextObstacle -= Time.deltaTime;

			if (nextSpawn < 0) {
				nextSpawn = 5/globalData.difficulty + Random.Range (2/globalData.difficulty, 4/globalData.difficulty);
				int res = 0;
				float rand = Random.Range (0f, 1f);
				Debug.Log (rand);
				if (rand <= 0.5f) {
					res = 0;
				} else if (rand <= 0.7f) {
					res = 2;
				} else {
					res = 1;
				}
				Instantiate (spawnables [res], new Vector3 (-1.5f, Random.Range (yMin, yMax), 0), transform.rotation);
			}

			if (nextObstacle < 0) {
				nextObstacle = 5/globalData.difficulty + Random.Range (2/globalData.difficulty, 4/globalData.difficulty);
				float y = Random.Range (yMin, yMax);
				int roll = (int) Random.Range (0, obstacles.Length);
				Instantiate (obstacles [roll], new Vector3 (2.5f, y, 0), transform.rotation);
				if(roll == 0){
					Instantiate (warningArrow, new Vector3 (1.3f, y, -1f), transform.rotation);
				}else{
					Instantiate (blueWarningArrow, new Vector3 (1.3f, y, -1f), transform.rotation);
				}
			}
		}
	}
}
