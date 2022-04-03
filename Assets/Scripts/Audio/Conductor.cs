using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor instance;
    public Song song;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;



    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    void Awake() {
        instance = this;
    }

    void Start() {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        musicSource.clip = song.clip;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }


   void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - song.offset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / song.SecPerBeat;
    }

    public float GetDistanceToNextBeat() {
        return Mathf.Ceil(songPositionInBeats) - songPositionInBeats;
    }

    public float GetAccuracy() {
        return songPositionInBeats - Mathf.Round(songPositionInBeats);
    }
}
