using UnityEngine;

[System.Serializable]
public class Song
{
  public string name;
  public float bpm;
  public float SecPerBeat { get { return  60f/bpm; } }
  public float offset; // timestamp of the first beat in the song

  public AudioClip clip;

  public bool loop;
  [Range(0f, 1f)]
  public float volume = 1;
  [Range(0.1f, 3f)]
  public float pitch = 1;
}