using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Necromancer : EnemyController
{
    [SerializeField] private Transform[] PointToSpawn;
    [SerializeField] private float TimeToSpawn;
    [SerializeField] private GameObject[] Enemy;
    public override void Start()
    {
        base.Start();
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            speed = 0;
            for (int i = 0; i < PointToSpawn.Length; i++)
            {
                GameObject enemy = Instantiate(Enemy[Random.Range(0, Enemy.Length)], PointToSpawn[i].position,
                                Quaternion.identity);
                enemy.GetComponent<EnemyController>().target = target;
                enemyManager.AddEnemy(enemy);
            }
            speed = Random.Range(MinSpeed, MaxSpeed);
        }
    }
}
