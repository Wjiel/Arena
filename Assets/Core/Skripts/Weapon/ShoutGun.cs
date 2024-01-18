using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ShoutGun : WeaponScript
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void Attack()
    {
        if (reload == true)
            return;

        if (countBullet == 0 || (Input.GetKeyDown(KeyCode.R)) && countBullet != maxCountBullet)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButtonDown(0) && time < 0)
        {
            time = TimeBetwenShots;
            countBullet--;
            switchWeapon.Bullet[countBullet].SetActive(false);

            particleShout.Play();
            particleBullet.Play();
            _camera.transform.DOShakePosition(timeDt, power);

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir, 10f, layerMask);

            foreach (RaycastHit2D iter in hit)
            {
                if (iter.transform.TryGetComponent(out IDamag enmCtr))
                {
                    GameObject part = Instantiate(particleHit, iter.transform.position, iter.transform.rotation);
                    Destroy(part, 1f);

                    if (iter.distance <= 2f)
                        damage = 50;
                    else if (iter.distance > 2 && iter.distance <= 4)
                        damage = 30;
                    else if (iter.distance > 4 && iter.distance <= 10)
                        damage = 10;

                    enmCtr.GetDamage(damage);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && countBullet == 2 && reload == false && time < 0)
        {
            time = TimeBetwenShots;
            countBullet--;
            switchWeapon.Bullet[countBullet].SetActive(false);
            countBullet--;
            switchWeapon.Bullet[countBullet].SetActive(false);

            particleShout.Play();
            particleBullet.Play();
            _camera.transform.DOShakePosition(timeDt, power);

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir, 10f, layerMask);

            foreach (RaycastHit2D iter in hit)
            {
                if (iter.transform.TryGetComponent(out IDamag enmCtr))
                {
                    GameObject part = Instantiate(particleHit, iter.transform.position, iter.transform.rotation);
                    Destroy(part, 1f);

                    if (iter.distance <= 2f)
                        damage = 70 * 2;
                    else if (iter.distance > 2 && iter.distance <= 4)
                        damage = 40 * 2;
                    else if (iter.distance > 4 && iter.distance <= 10)
                        damage = 15 * 2;

                    enmCtr.GetDamage(damage);
                }
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
