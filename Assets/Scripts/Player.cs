using UnityEngine;

public class Player : MonoBehaviour
{
  public float thrustSpeed = 1f;
  public float turnSpeed = 1f;
  public Bullet bulletPrefab;

  private Rigidbody2D rigidBody;
  private bool thrustingUp, thrustingDown;
  private float turnDirection;
  private SoundEffectsPlayer sound;

  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    sound = GetComponent<SoundEffectsPlayer>();
  }

  private void Update()
  {
    thrustingUp = Input.GetKey(KeyCode.UpArrow);
    thrustingDown = Input.GetKey(KeyCode.DownArrow);

    if (Input.GetKey(KeyCode.LeftArrow))
      turnDirection = 1f;
    else if (Input.GetKey(KeyCode.RightArrow))
      turnDirection = -1f;
    else
      turnDirection = 0f;

    if (Input.GetKeyDown(KeyCode.Space))
      Shoot();
  }

  private void FixedUpdate()
  {
    if (thrustingUp)
      rigidBody.AddForce(this.transform.up * this.thrustSpeed);
    if (thrustingDown)
      rigidBody.AddForce(-this.transform.up * this.thrustSpeed);
    if (turnDirection != 0f)
      rigidBody.AddTorque(turnDirection * turnSpeed);
  }

  private void Shoot()
  {
    Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
    bullet.Project(this.transform.up);
    sound.playshootSound();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Asteroid"))
    {
      sound.playbigSound();
      rigidBody.velocity = Vector3.zero;
      rigidBody.angularVelocity = 0f;

      this.gameObject.SetActive(false);

      FindAnyObjectByType<GameManager>().PlayerDied();
    }
  }
}