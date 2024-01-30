using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
  public Asteroid asteroidPrefab;
  public float spawnRate = 2f;
  public int spawnAmt = 1;
  public float spawnDistance = 12f;
  public float trajectoryVariance = 15f;

  private void Start()
  {
    InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
  }

  private void Spawn()
  {
    for (int i = 0; i < this.spawnAmt; i++)
    {
      Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
      Vector3 spawnPoint = this.transform.position + spawnDirection;

      float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
      Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

      Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
      asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

      asteroid.SetTrajectory(rotation * -spawnDirection);
    }

  }
}
