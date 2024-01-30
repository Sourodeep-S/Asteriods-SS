using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
  public AudioSource src;
  public AudioClip shootSound, bigSound, smallSound;

  public void playshootSound()
  {
    src.clip = shootSound;
    src.Play();
  }

  public void playbigSound()
  {
    src.clip = bigSound;
    src.Play();
  }

  public void playsmallSound()
  {
    src.clip = smallSound;
    src.PlayDelayed(0.01f);
  }
}
