using UnityEngine;

public class RubyController : MonoBehaviour
{
  public float speed = 3f;
  public int maxHealth = 5;
  public float timeInvincible = 2f;
  public GameObject projectilePrefab;
  public AudioClip cogClip;
  public AudioClip hitClip;

  public int health { get { return currentHealth; } }

  Rigidbody2D rb2d;
  Animator animator;
  AudioSource audioSource;
  int currentHealth;
  bool isInvincible;
  float invincibleTimer;
  Vector2 lookDirection = new Vector2(1, 0);
  float horizontal;
  float vertical;

  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    currentHealth = maxHealth;
  }
  void Update()
  {
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");

    Vector2 move = new Vector2(horizontal, vertical);

    if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
    {
      lookDirection.Set(move.x, move.y);
      lookDirection.Normalize();
    }

    animator.SetFloat("Look X", lookDirection.x);
    animator.SetFloat("Look Y", lookDirection.y);
    animator.SetFloat("Speed", move.magnitude);

    if (isInvincible)
    {
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer < 0)
        isInvincible = false;
    }

    if (Input.GetButtonDown("Fire1"))
    {
      Launch();
    }

    if (Input.GetButtonDown("Fire3"))
    {
      RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
      if (hit.collider != null)
      {
        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
        if (character != null)
        {
          character.DisplayDialog();
        }
      }
    }
  }

  void FixedUpdate()
  {
    Vector2 position = rb2d.position;
    position.x = position.x + speed * horizontal * Time.deltaTime;
    position.y = position.y + speed * vertical * Time.deltaTime;
    rb2d.MovePosition(position);
  }

  void Launch()
  {
    GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);

    Projectile projectile = projectileObject.GetComponent<Projectile>();
    projectile.Launch(lookDirection, 300);

    animator.SetTrigger("Launch");
    PlaySound(cogClip);
  }

  public void ChangeHealth(int amount)
  {
    if (amount < 0)
    {
      if (isInvincible)
      {

        return;
      }

      isInvincible = true;
      invincibleTimer = timeInvincible;
      PlaySound(hitClip);
    }
    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
  }

  public void PlaySound(AudioClip clip)
  {
    audioSource.PlayOneShot(clip);
  }
}