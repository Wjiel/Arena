using UnityEngine;
using DG.Tweening;
using TMPro;

public class Shop : MonoBehaviour
{

    [SerializeField] private MoneyManager uiManager;
    [SerializeField] private GameObject PanelShop;

    private bool isHide;

    public bool[] WeaponBuy;

    [SerializeField] private int[] Price;
    [SerializeField] private TextMeshProUGUI[] PriceText;
    [SerializeField] private GameObject[] Weapons;
    [SerializeField] private Transform Weapon;

    private void Start()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (WeaponBuy[i] == true)
            {
                Weapons[i].transform.SetParent(Weapon, true);
                PriceText[i].text = "Куплено";
            }
        }
    }
    public void HidePanel()
    {
        if (isHide == false)
        {
            isHide = true;
            PanelShop.transform.DOMoveX(-480, 1);
        }
        else
        {
            isHide = false;
            PanelShop.transform.DOMoveX(480, 1);
        }
    }
    public void BuyWeapon(int i)
    {
        switch (i)
        {
            case 0:
                if (uiManager.Money >= Price[0] && WeaponBuy[0] == false)
                {
                    WeaponBuy[0] = true;
                    Weapons[0].transform.SetParent(Weapon, true);
                    uiManager.SetMoney(Price[0]);
                    PriceText[0].text = "Куплено";
                }
                break;
            case 1:
                if (uiManager.Money >= Price[1] && WeaponBuy[1] == false)
                {
                    WeaponBuy[1] = true;
                    Weapons[1].transform.SetParent(Weapon, true);
                    uiManager.SetMoney(Price[1]);
                    PriceText[1].text = "Куплено";
                }
                break;
            case 2:
                if (uiManager.Money >= Price[2] && WeaponBuy[2] == false)
                {
                    WeaponBuy[2] = true;
                    uiManager.SetMoney(Price[2]);
                    Weapons[2].transform.SetParent(Weapon, true);
                    PriceText[2].text = "Куплено";
                }
                break;
            case 3:
                if (uiManager.Money >= Price[3] && WeaponBuy[3] == false)
                {
                    WeaponBuy[3] = true;
                    uiManager.SetMoney(Price[3]);
                    Weapons[3].transform.SetParent(Weapon, true);
                    PriceText[3].text = "Куплено";
                }
                break;

        }
    }
}
