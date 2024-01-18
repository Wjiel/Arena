using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    [HideInInspector] public WeaponScript weaponScript;

    public int weaponSwitch;
    public Image BulletImage;
    public Transform Ammo;

    public List<GameObject> Bullet = new();

    private void Start()
    {
        SelectWeapon();
    }
    private void Update()
    {
        int currentWeapon = weaponSwitch;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (weaponSwitch >= transform.childCount - 1)
            {
                weaponSwitch = 0;
            }
            else
                weaponSwitch++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount - 1;
            }
            else
                weaponSwitch--;

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSwitch = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            weaponSwitch = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            weaponSwitch = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            weaponSwitch = 4;
        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }
    }
    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponSwitch)
            {
                weapon.gameObject.SetActive(true);
                weaponScript = weapon.GetComponent<WeaponScript>();
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
    public void SwitchBullet(int maxBulet, int current)
    {
        if (Bullet.Count != 0)
        {
            for (int i = 0; i < Bullet.Count; i++)
            {
                Destroy(Bullet[i]);
            }
        }
        Bullet.Clear();

        for (int i = 0; i < maxBulet; i++)
        {
            var bullet = Instantiate(BulletImage);
            bullet.transform.localScale = Vector3.one;
            bullet.transform.SetParent(Ammo, false);
            Bullet.Add(bullet.gameObject);
        }

        int bulletIsActive = maxBulet - current;

        for (int i = 0; i < bulletIsActive; i++)
        {
            Bullet[i].SetActive(false);
        }

    }
}
