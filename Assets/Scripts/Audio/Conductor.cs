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
        return animationCurve.Evaluate(GetDistanceToClosestBeat() + 0.5f);
    }

    public string GetPlayTime() {
        int hours = (int)Mathf.Floor(dspSongTime / 3600);
        int min = (int)Mathf.Floor(dspSongTime / 60);
        int sec = (int)Mathf.Floor(dspSongTime - (hours * 3600) - min * 60);

        if (hours > 0) {
            return string.Format("{0:D2}h {1:D2}m {2:D2}s", hours, min, sec);
        } else if (min > 0) {
            return string.Format("{0:D2}m {1:D2}s", min, sec);
        } else {
            return string.Format("{0:D2}s", sec);
        }
    }
}
