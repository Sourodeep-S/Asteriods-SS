using UnityEngine;

public class GameManager : MonoBehaviour
{
  public Player player;
  public float invulnerabilityTime = 3f;
  public ParticleSystem explosion;

  private int lives = 3;
  private float respawnTime = 3f;
  private int score = 0;

  public void AsteroidDestroyed(Asteroid asteroid)
  {
    this.explosion.transform.position = asteroid.transform.position;
    this.explosion.Play();

    if (asteroid.size < 0.75f)
      this.score += 100;
    else if (asteroid.size < 1.2f)
      this.score += 50;
    else
      this.score += 100;
  }

  public void PlayerDied()
  {
    this.explosion.transform.position = this.player.transform.position;
    this.explosion.Play();

    this.lives--;
    if (this.lives <= 0)
      GameOver();
    else
      Invoke(nameof(Respawn), this.respawnTime);

  }

  private void Respawn()
  {
    this.player.transform.position = Vector3.zero;
    this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
    this.player.gameObject.SetActive(true);
    Invoke(nameof(TurnOnCollisions), invulnerabilityTime);
  }

  private void TurnOnCollisions()
  {
    this.player.gameObject.layer = LayerMask.NameToLayer("Player");
  }

  private void GameOver()
  {
    
  }
}
