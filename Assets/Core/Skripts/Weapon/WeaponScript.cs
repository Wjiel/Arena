using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class WeaponScript : MonoBehaviour
{
    public SwitchWeapon switchWeapon;

    [Header("Particl")]
    [SerializeField] protected GameObject particleHit;
    [SerializeField] protected ParticleSystem particleShout;
    [SerializeField] protected ParticleSystem particleBullet;

    [Header("Objects")]
    [SerializeField] protected Camera _camera;
    [SerializeField] protected Transform Player;
    [SerializeField] protected Transform Pricel;
    [SerializeField] protected Image ReloadImg;

    [Header("Settings")]
    [SerializeField] protected int damage;
    [SerializeField] protected float TimeBetwenShots;
    [SerializeField] protected float TimeReload;

    [SerializeField] protected int maxCountBullet;
    protected int countBullet;
    protected bool reload;
    protected const float offset = 360;

    [Header("Other")]

    public LayerMask layerMask;

    [Header("DOTween")]
    [SerializeField] protected float power;
    [SerializeField] protected float timeDt;

    protected float time;
    protected Vector3 dir;
    private void Awake()
    {
        countBullet = maxCountBullet;
    }
    protected virtual void OnEnable()
    {
        switchWeapon.SwitchBullet(maxCountBullet, countBullet);
    }
    private void OnDisable()
    {
        DOTween.Kill(ReloadImg);

        if (ReloadImg != null)
        {
            ReloadImg.fillAmount = 0;
            ReloadImg.enabled = false;
        }
        reload = false;
    }
    public void Update()
    {
        Controller();

        Attack();
    }
    public virtual void Attack()
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50f, layerMask);

            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out IDamag enmCtr))
                {
                    GameObject part = Instantiate(particleHit, hit.transform.position, hit.transform.rotation);
                    Destroy(part, 1f);
                    enmCtr.GetDamage(damage);
                }
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
    protected void Controller()
    {
        dir = _camera.ScreenToWorldPoint(Input.mousePosition) - Player.position;
        float rotateZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Player.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }
    protected virtual IEnumerator Reload()
    {
        reload = true;
        ReloadImg.fillAmount = 0;
        ReloadImg.enabled = true;

        ReloadImg.DOFillAmount(1, TimeReload);

        for (int i = 0; i < switchWeapon.Bullet.Count; i++)
        {
            yield return new WaitForSeconds(TimeReload / maxCountBullet);
            switchWeapon.Bullet[i].SetActive(true);
        }

        ReloadImg.enabled = false;
        countBullet = maxCountBullet;
        reload = false;
    }
}
