using System.Collections;
using UnityEngine;

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
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _SpawnWave();
        //}
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                _SpawnWave();
                yield return new WaitForSeconds(Random.Range(0.5f, spawnDelay));
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }

    private void _SpawnWave()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(R.instance.asteroid, spawnPosition, spawnRotation);
    }

    public void AddScore(float value)
    {
        PlayerController.instance.score += value;
        UpdateScoreText(PlayerController.instance.score);
    }

    private void UpdateScoreText(float currentScore)
    {
        R.instance.scoreText.text = "Score: " + currentScore.ToString();
    }
}
