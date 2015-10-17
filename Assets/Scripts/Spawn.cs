using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Wave[] waves;
	public Enemy enemy;

	int enemiesAlive;
	int enemiesToSpawn;
	float nextSpawnTime;

	Wave currentWave;
	int currentWaveNumber;

	[System.Serializable]
	public class Wave{
		public int EnemyCount;
		public float spawnTime;
	}

	void NextWave()
	{
		currentWaveNumber++;

		if(currentWaveNumber - 1 < waves.Length)
		{
			currentWave = waves[currentWaveNumber - 1];
			
			enemiesAlive = enemiesToSpawn = currentWave.EnemyCount;	
		}
	}

	void Start()
	{
		NextWave();
	}

	void Update(){

		if(enemiesToSpawn > 0 && Time.time > nextSpawnTime){
			enemiesToSpawn--;
			nextSpawnTime = Time.time + currentWave.spawnTime;

			Enemy en = GameObject.Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
			en.OnDeath += OnDeathEnemy;
		}
	}

	void OnDeathEnemy(){
		enemiesAlive--;

		if(enemiesAlive == 0)
		{
			NextWave();
		}

	}
}
