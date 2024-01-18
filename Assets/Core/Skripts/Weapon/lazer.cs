using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Lazer : WeaponScript
{
    [SerializeField] private Image BulletBar;
    [SerializeField] private float CountReload = 100;
    protected override void OnEnable()
    {
        switchWeapon.SwitchBullet(maxCountBullet, countBullet);

        BulletBar.gameObject.SetActive(true);

        BulletBar.fillAmount = CountReload / 100;
    }
    private void OnDisable()
    {
        BulletBar.gameObject.SetActive(false);

        DOTween.Kill(ReloadImg);
        DOTween.Kill(BulletBar);

        if (ReloadImg != null)
        {
            ReloadImg.fillAmount = 0;
            ReloadImg.enabled = false;
        }

        reload = false;
        StopCoroutine(Reload());
    }
    public override void Attack()
    {
        if (reload == true)
            return;

        if (CountReload <= 0 || (Input.GetKeyDown(KeyCode.R)) && CountReload != 100)
        {
            ReloadImg.fillAmount = 0;
            ReloadImg.enabled = true;
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0))
        {
            particleShout.Play();
            particleBullet.Play();

            CountReload -= 0.02f;

            BulletBar.fillAmount = CountReload / 100f;

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir, 100, layerMask);

            foreach (RaycastHit2D iter in hit)
            {
                if (iter.transform.TryGetComponent(out IDamag enmCtr))
                {
                    GameObject part = Instantiate(particleHit, iter.transform.position, iter.transform.rotation);
                    Destroy(part, 1f);
                    enmCtr.GetDamage(damage);
                }
            }
        }
    }
    protected override IEnumerator Reload()
    {
        reload = true;

        BulletBar.DOFillAmount(1, TimeReload);
        ReloadImg.DOFillAmount(1, TimeReload);

        yield return new WaitForSeconds(TimeReload);

        ReloadImg.enabled = false;
        CountReload = 100;
        reload = false;
    }
}
