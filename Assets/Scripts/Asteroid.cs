using UnityEngine;

public class Asteroid : MonoBehaviour
{
  public Sprite[] sprites;
  public float size = 1f;
  public float minSize = 0.5f;
  public float maxSize = 2f;
  public float asteroidSpeed = 100f;

  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rigidBody2d;
  private float maxLifetime = 30f;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    rigidBody2d = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
    this.transform.localScale = Vector3.one * this.size;
    rigidBody2d.mass = this.size;
  }

  public void SetTrajectory(Vector2 direction)
  {
    rigidBody2d.AddForce(direction * this.asteroidSpeed);

    Destroy(this.gameObject, this.maxLifetime);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Bullet"))
    {
      if (this.size * 0.5f >= this.minSize)
      {
        CreateSplit();
        CreateSplit();
      }

      Destroy(this.gameObject);
    }
  }

  private void CreateSplit()
  {
    Vector2 position = this.transform.position;
    position += Random.insideUnitCircle * 0.5f;

    Asteroid half = Instantiate(this, position, this.transform.rotation);
    half.size = this.size * 0.5f;
    half.SetTrajectory(Random.insideUnitCircle.normalized * this.asteroidSpeed);
  }
}
