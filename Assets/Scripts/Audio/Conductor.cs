using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor instance;
    public Song song;
    public float bpm { get { return song.bpm; }}
    public float secondPerBeat { get { return song.SecPerBeat; }}

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;
    public AnimationCurve animationCurve;
    public float userOffset = 0;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
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

    public void Stop()
    {
        musicSource.Stop();
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

    public float GetDistanceToClosestBeat() {
        return songPositionInBeats - Mathf.Round(songPositionInBeats);
    }

    public float GetAccuracy() {
        return animationCurve.Evaluate(Mathf.Repeat(GetDistanceToClosestBeat() + 0.5f - userOffset, 1f));
    }

    public string GetPlayTime() {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(songPosition);
        return timeSpan.ToString(@"mm\:ss");
    }
}
