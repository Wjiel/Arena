using DG.Tweening;
using UnityEngine;

public class Racet : WeaponScript
{
    [SerializeField] private float radiusExp;

    [SerializeField] private GameObject explosionParticle;
    public override void Attack()
    {
        if (reload == true)
            return;

        if (countBullet == 0 || (Input.GetKeyDown(KeyCode.R)) && countBullet != maxCountBullet)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && time < 0)
        {
            time = TimeBetwenShots;
            countBullet--;
            switchWeapon.Bullet[countBullet].SetActive(false);

            particleShout.Play();
            particleBullet.Play();

            _camera.transform.DOShakePosition(timeDt, power);

            RaycastHit2D hitPoint = Physics2D.Raycast(transform.position, dir, 50f, layerMask);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(hitPoint.point, radiusExp, Vector2.zero);

            if(hitPoint == true)
            {
                GameObject efect = Instantiate(explosionParticle, hitPoint.transform.position, Quaternion.identity);

                Destroy(efect, 2);
            } 

            foreach (RaycastHit2D enemy in hits)
            {
                if (enemy.transform.TryGetComponent(out IDamag enmCtr))
                {
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
