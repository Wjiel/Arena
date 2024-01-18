using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private Shop shop;

    [SerializeField] private TextMeshProUGUI MoneyText;

    public int Money;
    private void Start()
    {
        MoneyText.text = Money.ToString();
    }
    public void AddMoney(int money)
    {
        Money += money;

        MoneyText.text = Money.ToString();
    }
    public void SetMoney(int money)
    {
        Money -= money;
        MoneyText.text = Money.ToString();
    }
}
