using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Waves
    {
        public GameObject[] enemyPrefabs;
        public float rate;
        public float enemiesCount;
    }

    private int waveIndex;
    private int randomIndex;
    private float randomValue;
    private bool canSpawn = true;
    public Waves waves;
    [SerializeField] private GameObject[] spawnPosition;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private TextMeshProUGUI wavesText;
    private GameManager gm;
    private AudioManager am;

    void Start()
    {
        gm = GameManager.instance;
        am = AudioManager.instance;
    }

    void Update()
    {
        if(canSpawn) 
        {
            StartCoroutine(SpawnWave());
            canSpawn = false;
        }
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        wavesText.text = "Wave " + waveIndex.ToString();

        Debug.Log(waveIndex);

        if(waveIndex % 5 == 0)
        {
            am.PlayBossIncomingSFX();
            
            randomIndex = Random.Range(0, spawnPosition.Length);

            GameObject go = Instantiate(waves.enemyPrefabs[2], spawnPosition[randomIndex].transform.position, spawnPosition[randomIndex].transform.rotation);

            go.transform.parent = enemyParent;
        }

        for(int i = 0; i < waves.enemiesCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1.0f / waves.rate);
        }
    }

    private void SpawnEnemy()
    {   
        randomIndex = Random.Range(0, spawnPosition.Length);
        randomValue = Random.value;

        if(randomValue < 0.5f) 
        {
            GameObject go = Instantiate(waves.enemyPrefabs[0], spawnPosition[randomIndex].transform.position, spawnPosition[randomIndex].transform.rotation);

            go.transform.parent = enemyParent;
        }
        else if(randomValue >= 0.5f) 
        {
            GameObject go = Instantiate(waves.enemyPrefabs[1], spawnPosition[randomIndex].transform.position, spawnPosition[randomIndex].transform.rotation);

            go.transform.parent = enemyParent;
        }
    }

    public void UpgradeWave()
    {
        Debug.Log("Next Wave");
        Debug.Log(waveIndex);

        waves.enemiesCount = Mathf.Floor(waves.enemiesCount + 0.1f * waves.enemiesCount);
        if(waves.rate <= 8.0f) waves.rate += 0.2f;
        
        if(waveIndex % 5 == 0) gm.UpgradeEnemyStats();

        canSpawn = true;
    } 
}
