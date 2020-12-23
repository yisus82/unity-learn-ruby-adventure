using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float timeToLive = 3f;

  Rigidbody2D rb2d;
  float timer;

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    timer = timeToLive;
  }

  void Update()
  {
    timer -= Time.deltaTime;
    if (timer > 0)
    {
      return;
    }
    Destroy(gameObject);
  }

  public void Launch(Vector2 direction, float force)
  {
    rb2d.AddForce(direction * force);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    EnemyController enemy = other.collider.GetComponent<EnemyController>();
    if (enemy != null)
    {
      enemy.Fix();
    }

    Destroy(gameObject);
  }
}
