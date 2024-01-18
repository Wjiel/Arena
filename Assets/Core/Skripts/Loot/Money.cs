using UnityEngine;

public class Money : MonoBehaviour, ILooting
{
    [SerializeField] private int AmountOfMoney;
    [SerializeField] private ParticleSystem ParticlMoney;
    [SerializeField] private GameObject audioCoinPickUp;
    public void PickUp()
    {
        FindObjectOfType<MoneyManager>().AddMoney(AmountOfMoney);
        ParticleSystem partcl = Instantiate(ParticlMoney,transform.position, Quaternion.identity);
        GameObject audio = Instantiate(audioCoinPickUp,transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        Destroy(audio,1f);
        Destroy(partcl.gameObject, 1);
        Destroy(transform.gameObject, 1);
    }
}
