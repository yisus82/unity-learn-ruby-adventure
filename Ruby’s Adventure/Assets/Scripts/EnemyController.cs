using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float speed = 3f;
  public bool vertical;
  public float changeTime = 1.5f;
  public ParticleSystem smokeEffect;

  Rigidbody2D rb2d;
  Animator animator;
  AudioSource audioSource;
  float timer;
  int direction = 1;
  bool broken = true;

  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    timer = changeTime;
  }

  void Update()
  {
    if (!broken)
    {
      return;
    }

    timer -= Time.deltaTime;

    if (timer < 0)
    {
      direction = -direction;
      timer = changeTime;
    }
  }

  void FixedUpdate()
  {
    if (!broken)
    {
      return;
    }

    Vector2 position = rb2d.position;

    if (vertical)
    {
      position.y = position.y + Time.deltaTime * speed * direction;
      animator.SetFloat("Move X", 0);
      animator.SetFloat("Move Y", direction);
    }
    else
    {
      position.x = position.x + Time.deltaTime * speed * direction;
      animator.SetFloat("Move X", direction);
      animator.SetFloat("Move Y", 0);
    }

    rb2d.MovePosition(position);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    RubyController player = other.gameObject.GetComponent<RubyController>();

    if (player != null)
    {
      player.ChangeHealth(-1);
    }
  }

  public void Fix()
  {
    broken = false;
    rb2d.simulated = false;
    animator.SetTrigger("Fixed");
    smokeEffect.Stop();
    audioSource.Stop();
  }
}
