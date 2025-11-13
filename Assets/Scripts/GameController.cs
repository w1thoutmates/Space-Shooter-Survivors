using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Vector3 spawnValues;
    public int asteroidCount;
    public float spawnDelay;
    public float waveDelay;
    public float startDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _SpawnEnemy(R.instance.enemySpaceShip, _GetSpawnPosition(), _GetSpawnRotation());
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                _SpawnWave();
                _SpawnEnemy(R.instance.enemySpaceShip, _GetSpawnPosition(), _GetSpawnRotation());
                _SpawnPickup(R.instance.pickups[Random.Range(0, R.instance.pickups.Length)], _GetSpawnPosition(), _GetSpawnRotation());
                yield return new WaitForSeconds(Random.Range(0.5f, spawnDelay));
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }

    private void _SpawnWave()
    {
        Instantiate(R.instance.asteroid, _GetSpawnPosition(), _GetSpawnRotation());
    }

    public void AddScore(float value)
    {
        PlayerController.instance.score += (value * PlayerController.instance.scoreMultiplier);
        UpdateScoreText(PlayerController.instance.score);
    }

    private void UpdateScoreText(float currentScore)
    {
        R.instance.scoreText.text = "Score: " + currentScore.ToString();
    }

    private void _SpawnPickup(GameObject pickup, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (_Roll(PlayerController.instance.luck))
        {
            Instantiate(pickup, spawnPosition, spawnRotation);
        }
    }

    private void _SpawnEnemy(GameObject enemyPf, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (_Roll(PlayerController.instance.difficulty))
        {
            Instantiate(enemyPf, spawnPosition, spawnRotation);
        }
    }

    private bool _Roll(float luck)
    {
        if(Random.value < luck)
        {
            return true;
        }

        return false;
    }
    
    private Vector3 _GetSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
    }

    private Quaternion _GetSpawnRotation()
    { 
        return Quaternion.identity;
    }
}
