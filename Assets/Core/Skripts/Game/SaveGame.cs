using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private MoneyManager manager;
    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] private Shop shop;

    private void Awake()
    {
        LoadSaveCloud();
    }
    private void LoadSaveCloud()
    {
        manager.Money = YandexGame.savesData.Money;
        enemyManager.RecordKillEnemy = YandexGame.savesData.record;

        shop.WeaponBuy = YandexGame.savesData.Weapon;

    }
    public void MySave()
    {
        YandexGame.savesData.Weapon = shop.WeaponBuy;

        YandexGame.savesData.record = enemyManager.RecordKillEnemy;
        YandexGame.savesData.Money = manager.Money;
        YandexGame.SaveProgress();
    }

}
