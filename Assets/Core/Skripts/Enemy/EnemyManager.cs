using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI TextScore;
    [SerializeField] private TextMeshProUGUI TextRecord;
    [SerializeField] private TextMeshProUGUI TextEnemyCount;

    [Header("Loot")]
    [SerializeField] private GameObject[] Loot;

    [Header("Record")]
    public int RecordKillEnemy;

    private int killEnemy;

    [Header("Enemy")]
    public List<GameObject> EnemyList = new();
    private int countEnemy;
    private void Start()
    {
        TextRecord.text = "Рекорд: " + RecordKillEnemy.ToString();
    }
    public void AddEnemy(GameObject Enemy)
    {
        EnemyList.Add(Enemy);

        countEnemy++;
        TextEnemyCount.text = countEnemy.ToString();
    }
    public void RemoveEnemy(GameObject Enemy)
    {
        FallingOutOfObjects(Enemy);
        EnemyList.Remove(Enemy);
        killEnemy++;
        TextScore.text = killEnemy.ToString();

        Record();

        countEnemy--;
        TextEnemyCount.text = countEnemy.ToString();
    }
    private void Record()
    {
        if (killEnemy > RecordKillEnemy)
        {
            RecordKillEnemy = killEnemy;
            TextRecord.text = "Рекорд: " + RecordKillEnemy.ToString();
        }
    }
    private void FallingOutOfObjects(GameObject Enemy)
    {
        int random = Random.Range(0, 10);

        if (random >= 5 || random <= 7)
        {
            Instantiate(Loot[Random.Range(0, Loot.Length)], Enemy.transform.position, Quaternion.identity);
        }
    }
}
