using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class EnemyController : MonoBehaviour, IDamag
{
    protected EnemyManager enemyManager;
    [SerializeField] protected GameObject HealthBar;
    [SerializeField] protected Image HealthBarFill;

    [HideInInspector] public Transform target;

    [SerializeField] private int damage;

    [SerializeField] protected int MaxHealth;

    [SerializeField] protected float Coldown;
    [SerializeField] protected float MaxSpeed;
    [SerializeField] protected float MinSpeed;
    protected float speed;


    public float Health { get; set; }
    private float time;
    private float timeToDamage;

    private bool CanAttack;
    public virtual void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>().GetComponent<EnemyManager>();
        Health = MaxHealth;
        speed = Random.Range(MinSpeed, MaxSpeed);
    }
    private void Update()
    {
        if (target != null && CanAttack == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        CheakSpeed();
    }
    private void CheakSpeed()
    {
        speed = Random.Range(MinSpeed, MaxSpeed);
    }
    public void GetDamage(int damage)
    {
        Health -= damage;

        speed = MinSpeed;

        Invoke("CheakSpeed", 1);

        HealthBarFill.DOFillAmount(Health / MaxHealth, 0.2f);

        if (Health <= 0)
        {
            Death();
        }

        if (HealthBar.activeSelf == false)
            HealthBar.SetActive(true);
        if (gameObject.activeSelf == true)
            StartCoroutine(SetActive());
    }
    protected void Death()
    {
        enemyManager.RemoveEnemy(gameObject);
        gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }
    protected IEnumerator SetActive()
    {
        yield return new WaitForSeconds(10);
        HealthBar.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController controller))
        {
            CanAttack = true;
            timeToDamage -= Time.deltaTime;

            if (timeToDamage <= 0f)
            {
                timeToDamage = Coldown;
                controller.GetDamage(damage);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CanAttack = false;
    }
}
