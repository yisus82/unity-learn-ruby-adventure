using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  public AudioClip collectedClip;

  void OnTriggerEnter2D(Collider2D other)
  {
    RubyController player = other.GetComponent<RubyController>();

    if (player != null)
    {
      if (player.health < player.maxHealth)
      {
        player.ChangeHealth(1);
        player.PlaySound(collectedClip);
        Destroy(gameObject);
      }
    }
  }
}
