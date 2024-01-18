using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;


public class PlayerController : MonoBehaviour, IDamag
{
    public GameScript gameScr;

    [Header("ParticleSystem")]
    [SerializeField] private GameObject JumpParticl;

    [Header("Settings")]
    [SerializeField] private float speed;

    [SerializeField] private float MaxSpeed;

    [SerializeField] private float MaxHealth;

    [SerializeField] private Gradient gradient;

    [SerializeField] private float JumpForce;

    [Header("UI")]
    [SerializeField] private Image HealthBar;

    [SerializeField] private Image StaminaBar;

    [SerializeField] private Image Blood;

    [Header("Objects")]

    [SerializeField] private PostProcessVolume postProcessing;

    [SerializeField] private Camera _camera;

    private Rigidbody2D rb;
    private Vignette vignette;

    private float normalSpeed;
    public float Health { get; set; }

    private bool CanStamina;

    private Sequence mySequence;

    private bool isJump;
    private void Start()
    {
        postProcessing.GetComponent<PostProcessVolume>().profile.TryGetSettings(out vignette);
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        normalSpeed = speed;
    }
    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal") * speed;
        float vert = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(hor, vert, -1f);
    }
    private void Update()
    {
        BoostMove();

        Jump();
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            isJump = true;

            GameObject effect = Instantiate(JumpParticl, transform.position, Quaternion.identity);
            Destroy(effect, 2);

            rb.AddForce(transform.right * JumpForce);
            Invoke("LockIsJump", 2);
        }
    }
    private void LockIsJump()
    {
        isJump = false;
    }
    private void BoostMove()
    {
        if (StaminaBar.fillAmount == 0 || CanStamina == true)
            Stamina();

        if (StaminaBar.fillAmount == 1)
            StaminaBar.DOFade(0, 1).SetLink(gameObject);

        if (CanStamina == true)
            return;

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = MaxSpeed;

            StaminaBar.DOFillAmount(0, 10).SetLink(gameObject);

            StaminaBar.DOFade(100, 0.5f).SetLink(gameObject);

            _camera.DOOrthoSize(8, 1).SetLink(gameObject);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = normalSpeed;

            StaminaBar.DOFillAmount(1, 10).SetLink(gameObject);

            _camera.DOOrthoSize(7, 1).SetLink(gameObject);
        }
    }
    private void Stamina()
    {
        CanStamina = true;

        speed = 1.5f;

        StaminaBar.fillAmount += 0.2f * Time.deltaTime;

        _camera.DOOrthoSize(6.5f, 1).SetLink(gameObject);

        if (StaminaBar.fillAmount == 1)
            CanStamina = false;
    }
    public void GetDamage(int damage)
    {
        _camera.transform.DOShakePosition(0.1f, 1f).SetLink(gameObject);

        Health -= damage;

        HealthBar.DOFillAmount(Health / MaxHealth, 0.4f);

        if (Health < MaxHealth / 2)
        {
            Blood.gameObject.SetActive(true);
            mySequence = DOTween.Sequence()
               .Append(Blood.DOFade(1, 1f))
               .AppendInterval(0.1f)
               .Append(Blood.DOFade(0, 1f))
               .AppendInterval(0.1f)
               .SetLoops(-1).SetLink(gameObject);
        }

        if (Health <= 0)
        {
            gameScr.DeathPlaer();
        }

        if (vignette.intensity.value < 0.25f)
            vignette.intensity.value += 0.04f;

        HealthBar.color = gradient.Evaluate(Health / MaxHealth);
    }
    public void SetHealth(float health)
    {
        Health += health;

        HealthBar.DOFillAmount(Health / MaxHealth, 0.4f);

        if (Health > MaxHealth / 2)
        {
            Blood.gameObject.SetActive(false);

            Blood.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            mySequence.Kill();
        }

        if (Health > MaxHealth)
        {
            Health = 100;
        }

        if (vignette.intensity.value > 0.25f)
            vignette.intensity.value -= 0.04f;

        HealthBar.color = gradient.Evaluate(Health / MaxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out ILooting Loot))
        {
            Loot.PickUp();
        }
    }

}
