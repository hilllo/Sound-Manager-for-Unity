//@
// Sound Manager.

// Updated 2015.11.13
// Hill Lu
//-----------------------------------------------------

using UnityEngine;
using System.Collections;

//bgm stage enum
public enum BGMStage
{
    TITLE,
    OPENING,
    FIGHT_STAGE_1,
    FIGHT_EVOLUTION,
    FIGHT_STAGE_2,
    END_WIN,
    END_LOSE
}

[System.Serializable]
public class SoundManager : Singleton<SoundManager>
{

    protected SoundManager() { }
    //Sound Manager
    public Transform soundManager;

    //Audio Source
    //public AudioSource bgmAS;
    //public AudioSource[] dialogAS;

    //Audio Clips
    //public AudioClip[] bgmOpeningC;
    //public AudioClip[] dialogC;

    class SoundParam
    {
        public AudioClip clip;
        public float volumn;
        public float pitch;

        public SoundParam(AudioClip clip, float volumn, float pitch)
        {
            this.clip = clip;
            this.volumn = volumn;
            this.pitch = pitch;
        }
        public SoundParam()
        { }
    }

    //declare SoundParam
    //SoundParam bgmOpening;

    void Start()
    {
        // Debug.Log("Sound Manager start working");
        //initialize sound param
        Initialize();
    }

    void Update()
    {
        //test
        //if (Input.GetKeyDown("o")) bgmPlay(BGMStage.OPENING);

    }

    //Initialize SFX Param (volumn and pitch of each clip)
    void Initialize()
    {
        //Debug.Log("Initialize Sounds");
        //bgmOpening = new SoundParam(bgmOpeningC, 1f, 1f);
    }

    //bgm
    public void bgmPlay(BGMStage stage)
    {
        Debug.Log("Play BGM");
        StartCoroutine(CoRoutineBgmPlay(bgmAS, stage, 10f, 10f));
    }

    IEnumerator CoRoutineBgmPlay(AudioSource audioS, BGMStage stage, float fadeOutSpeed, float fadeInSpeed)
    {
        SoundParam sp = new SoundParam();

        StartCoroutine(MusicFadeOut(audioS, fadeOutSpeed, 0.01f, true));
        while (audioS.isPlaying)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //
        switch (stage)
        {
            case BGMStage.TITLE:
                //Title
                break;
            case BGMStage.OPENING:
                //sp = bgmOpening;
                break;
            case BGMStage.FIGHT_STAGE_1:
                //sp = bgmFightStage1;
                break;
            case BGMStage.FIGHT_EVOLUTION:
                //Evolution
                break;
            case BGMStage.FIGHT_STAGE_2:
                //sp = bgmFightStage2;
                break;
            case BGMStage.END_WIN:
                //sp = bgmEndingWin;
                break;
            case BGMStage.END_LOSE:
                //sp = bgmEndingLose;
                break;
            default:
                Debug.Log("bgmPlay input Error");
                break;
        }

        bgmAS.clip = sp.clip;
        bgmAS.pitch = sp.pitch;
        StartCoroutine(MusicFadeIn(audioS, fadeInSpeed, sp.volumn, true));
    }


    //sfx



    //dialog
    IEnumerator DialogPlay(int RoleID, int dialogID, float volume)
    {
        while (dialogAS[RoleID].isPlaying)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        dialogAS[RoleID].clip = dialogC[dialogID];
        dialogAS[RoleID].volume = volume;
        dialogAS[RoleID].Play();
        // Debug.Log (dialogAS[RoleID].clip);
    }


    //private function
    IEnumerator MusicFadeIn(AudioSource audios, float fadeInSpeed, float maxVolumn, bool restart)
    {
        //Debug.Log("Start fade in.");
        if (restart)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            audios.Play();
        }
        while (audios.volume < maxVolumn)
        {
            audios.volume += fadeInSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //		Debug.Log("Complete fade in.");
    }

    void MusicFadeInReplay(AudioSource audios, float fadeInSpeed, float maxVolumn)
    {
        audios.Stop();
        audios.volume = 0;
        audios.pitch = 1f; ;
        StartCoroutine(MusicFadeIn(audios, fadeInSpeed, maxVolumn, true));
    }

    IEnumerator MusicFadeOut(AudioSource audios, float fadeOutSpeed, float minVolumn, bool stop)
    {
        //		Debug.Log("Start fade out.");
        while (audios.volume >= minVolumn)
        {
            audios.volume -= fadeOutSpeed * Time.deltaTime;
            if (audios.isPlaying)
            {
                //Debug.Log("audios.isPlaying = " + audios.isPlaying);
                yield return new WaitForSeconds(Time.deltaTime);
            }

        }
        if (stop)
        {
            audios.Stop();
        }
    }

    IEnumerator RaisePitch(AudioSource audios, float raiseSpeed, float maxPitch)
    {
        while (audios.pitch < maxPitch)
        {
            audios.pitch += raiseSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// Plays a sound by creating an empty game object with an AudioSource
    /// and attaching it to the given transform (so it moves with the transform). Destroys it after it finished playing.
    void AudioPlay(AudioClip clip, Transform emitter, float volume, float pitch)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = emitter.position;
        go.transform.parent = emitter;

        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(go, clip.length);
    }

    void AudioPlay(AudioClip clip)
    {
        AudioPlay(clip, soundManager, 1f, 1f);
    }

    void AudioPlay(AudioClip clip, Transform emitter)
    {
        AudioPlay(clip, emitter, 1f, 1f);
    }

    void AudioPlay(AudioClip clip, Transform emitter, float volume)
    {
        AudioPlay(clip, emitter, volume, 1f);
    }

    /// Plays a sound at the given point in space by creating an empty game object with an AudioSource
    /// in that place and destroys it after it finished playing.
    void AudioPlay(AudioClip clip, Vector3 point, float volume, float pitch)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = point;

        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(go, clip.length);
    }

    void AudioPlay(AudioClip clip, Vector3 point)
    {
        AudioPlay(clip, point, 1f, 1f);
    }

    void AudioPlay(AudioClip clip, Vector3 point, float volume)
    {
        AudioPlay(clip, point, volume, 1f);
    }


}
