using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform[] PointsSpawner;

    [SerializeField] private Waves[] waves;
    public float TimeWaves;

    private int _currentWaveIndex;
    public void StartGame()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while (waves.Length != _currentWaveIndex)
        {
            for (int i = 0; i < waves.Length; i++)
            {
                _currentWaveIndex++;
                yield return new WaitForSeconds(TimeWaves);

                for (int j = 0; j < waves[i].WaveSettings.NumberOfEnemy; j++)
                {
                    GameObject enemy = Instantiate(waves[i].WaveSettings.Enemy[Random.Range(0, waves[i].WaveSettings.Enemy.Length)],
                        PointsSpawner[Random.Range(0, PointsSpawner.Length)].position,
                        Quaternion.identity);

                    enemy.GetComponent<EnemyController>().target = Player;
                    enemyManager.AddEnemy(enemy);
                    yield return new WaitForSeconds(waves[i].WaveSettings.TimeBetwenSpawnEnemy);
                }
            }
        }
    }
}
[System.Serializable]
public class Waves
{
    [SerializeField] private SettingsWaves SettingsWaves;
    public SettingsWaves WaveSettings { get => SettingsWaves; }
}

[System.Serializable]
public class SettingsWaves
{
    [SerializeField] private GameObject[] _enemy;
    public GameObject[] Enemy { get => _enemy; }

    [SerializeField] private int _numberOfEnemy;
    public int NumberOfEnemy { get => _numberOfEnemy; }

    [SerializeField] private float _timeBetwenSpawnEnemy;
    public float TimeBetwenSpawnEnemy { get => _timeBetwenSpawnEnemy; }
}