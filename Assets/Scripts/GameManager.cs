using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public Player player;
  public float invulnerabilityTime = 3f;
  public ParticleSystem explosion;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI livesText;
  public TextMeshProUGUI gameOverText;
  public Button retryButton;

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

  private void Update()
  {
    this.scoreText.text = score.ToString("D5");
    this.livesText.text = lives.ToString();
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
    enabled = false;
    this.gameOverText.gameObject.SetActive(true);
    this.retryButton.gameObject.SetActive(true);
  }

  public void NewGame()
  {
    Asteroid[] asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);

    foreach (var obs in asteroids)
    {
      Destroy(obs.gameObject);
    }

    this.gameOverText.gameObject.SetActive(false);
    this.retryButton.gameObject.SetActive(false);
    this.lives = 3;
    this.score = 0;
    enabled = true;
    Invoke(nameof(Respawn), 0f);
  }
}
