using UnityEngine;

public class Bullet : MonoBehaviour
{
  private Rigidbody2D rigidBody;
  private float bulletSpeed = 500f;
  private float maxLifetime = 5f;

  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }

  public void Project(Vector2 direction)
  {
    rigidBody.AddForce(direction * this.bulletSpeed);
    Destroy(this.gameObject, this.maxLifetime);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(this.gameObject);
  }
}
