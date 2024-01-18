using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private GameObject partHP;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.SetHealth(Health);

            GameObject hp = Instantiate(partHP, transform.position, Quaternion.identity);
            transform.gameObject.SetActive(false);

            Destroy(hp, 1.5f);
            Destroy(transform.gameObject, 1.5f);
        }
    }
}
