//@
// Sound Manager
// for Building Virtual Worlds 2015 Round 2 "Race to the Treehouse"

// Updated 2015.10.05
// Hill Lu
//-----------------------------------------------------

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public float fadeSpeed = 0.4f;
	public float maxVolumn_Rising = 1f;

	public GameObject player;

	public AudioSource title;
	public AudioSource bgmMain;
	public AudioSource bgmRise;
	public AudioSource bgmEnding; 
	public AudioSource fellowDialog;
	public AudioSource sfxCrash;
	public AudioSource sfxWaterfall;
	public AudioSource sfxCoin;
	public AudioSource sfxRockFall;


	public AudioClip bgmRace;
	public AudioClip bgmEndingWin;
	public AudioClip bgmEndingLose;
	public AudioClip sfxReach;
	public AudioClip sfxFirework;

	public AudioClip[] dialogTutorial;

	bool isRising = false;
	bool isFalling = false;
	bool hasFallen = false;


	//test
	AudioSource testAS;
	GameObject testGO;

	// Use this for initialization
	void Start () {
//		Debug.Log("Sound Manager start working");
//		mainThemeStart ();
	}
	
	// Update is called once per frame
	void Update () {
		//test
		if (Input.GetKeyDown ("up"))	RisingMusicStart ();
		if (Input.GetKeyDown ("down"))	FallingMusicStart ();
		if (Input.GetKeyDown ("left"))	RisingOrFallingMusicStop ();
		if (Input.GetKeyDown ("m"))	mainThemeStop ();
		if (Input.GetKeyDown ("n"))	mainThemeStart ();
		if (Input.GetKeyDown ("r"))	raceThemeStart ();
		if (Input.GetKeyDown ("e"))	raceThemeStop ();
		if (Input.GetKeyDown ("c"))	SFXCrash ();
		if (Input.GetKeyDown ("x"))	SFXCoin ();
		if (Input.GetKeyDown ("f"))	SFXFirework ();
		if (Input.GetKeyDown ("t"))	SFXRockFall ();

		if (Input.GetKeyDown ("w"))	endWin ();
		if (Input.GetKeyDown ("l"))	endLose ();

		if (Input.GetKeyDown ("0")) titleMusicStart ();
		if (Input.GetKeyDown ("1")) {
			dialog01AHello ();
			dialog01BTurnAroundLookAtMe ();
		}
		if (Input.GetKeyDown ("2")) dialog02LetsGo2SP ();
		if (Input.GetKeyDown ("3")) {
			dialog03AUseDandelion2Fly ();
			dialog03BComeOnWait4U ();
			dialog03CHurryUp4lowMe();
		}
		if (Input.GetKeyDown ("4")) dialog04TurnRight ();
		if (Input.GetKeyDown ("5")) {
			dialog05GJOnReachStart ();
			dialog06NowLetsStartRaceReady321 ();
			dialog07Go ();
		}
		if (Input.GetKeyDown ("6")) {
			dialog08Congra ();
			dialog09A1st ();
			dialog09B2nd ();
			dialog09C3rd ();
		}
	}

	public void titleMusicStart(){
		title.Play ();
	}

	public void mainThemeStart(){
//		Debug.Log ("Start Main Theme");
		StartCoroutine(MusicFadeIn (bgmMain, 1f,1f));
		bgmRise.Play ();
	}

	//Call when the guest arrive starting point (hit the ground)
	//Beware!!!! This function must be called after the mainTheme fade in is COMPLETELY DONE (volumn = 1)!
	//Cause I'm too lazy to adjust it XD
	public void mainThemeStop(){
		StartCoroutine(MusicFadeOut (bgmMain,1f));
		StartCoroutine(MusicFadeOut (bgmRise,1f));
	}

	//Call when the race begins
	public void raceThemeStart(){
		bgmMain.clip = bgmRace;
		bgmMain.volume = 1f;
		bgmMain.Play ();
		bgmRise.Play ();
	}

	public void raceThemeStop(){
		sfxCrash.clip = sfxReach;
		sfxCrash.Play ();

		StartCoroutine(MusicFadeOut (bgmMain,1f));
		StartCoroutine(MusicFadeOut (bgmRise,1f));
	}

	//Call when the guest starts to rise
	public void RisingMusicStart(){ 
//		Debug.Log("Start Rising");
		FallingMusicStop();
		isRising = true;
		StartCoroutine(FRMusicFadeIn(bgmRise,maxVolumn_Rising));
	}

	//Call when the guest starts to fall or drop
	public void FallingMusicStart(){
//		Debug.Log("Start Falling");
		GameObject go;
		AudioSource audios;
		go = GameObject.Find("bgmRise");
		audios = go.GetComponent<AudioSource> ();
		RisingMusicStop();
		isFalling = true;
		StartCoroutine(FRMusicFadeOut(bgmRise));
	}

	//Call when the guest stay stable (not rising, not falling)
	//If the guest switch from rising to falling (or oposite) abruptly, this function is no need to call.
	public void RisingOrFallingMusicStop(){
		RisingMusicStop ();
		FallingMusicStop ();
	}

	public void endWin(){
		bgmEnding.clip = bgmEndingWin;
		bgmEnding.Play ();
	}

	public void endLose(){
		bgmEnding.clip = bgmEndingLose;
		bgmEnding.Play ();
	}

	//sfx
	public void SFXCrash (){
		if (!sfxCoin.isPlaying) {
			sfxCrash.volume = 1f;
			sfxCrash.Play ();
		}
	}

	public void SFXCoin(){
		sfxCoin.volume = 0.4f;
		sfxCoin.Play ();
	}

	public void SFXFirework(){
		AudioPlay (sfxFirework, 1f, 1f);
	}

	public void SFXRockFall(){
		if (!hasFallen) {
			sfxRockFall.volume = 1f;
			sfxRockFall.Play ();
			hasFallen = true;
		}
	}

	//Dialogs
	public void dialog01AHello(){
		StartCoroutine(dialogPlay(0,0.35f));
	}

	public void dialog01BTurnAroundLookAtMe(){
		StartCoroutine(dialogPlay(1,0.35f));
	}

	public void dialog02LetsGo2SP(){
		StartCoroutine(dialogPlay(2,0.35f));
	}

	public void dialog03AUseDandelion2Fly(){
		StartCoroutine(dialogPlay(3,0.5f));
	}

	public void dialog03BComeOnWait4U(){
		StartCoroutine(dialogPlay(4,0.55f));
	}

	public void dialog03CHurryUp4lowMe(){
		StartCoroutine(dialogPlay(5,0.55f));
	}

	public void dialog04TurnRight(){
		StartCoroutine(dialogPlay(6,0.55f));
	}

	public void dialog05GJOnReachStart(){
		StartCoroutine(dialogPlay(7,0.35f));
	}

	public void dialog06NowLetsStartRaceReady321(){
		StartCoroutine(dialogPlay(8,0.35f));
	}

	public void dialog07Go(){
		StartCoroutine(dialogPlay(9,0.35f));
	}

	public void dialog08Congra(){
		StartCoroutine(dialogPlay(10,0.55f));
	}

	public void dialog09A1st(){
		StartCoroutine(dialogPlay(11,0.8f));
	}

	public void dialog09B2nd(){
		StartCoroutine(dialogPlay(12,0.8f));
	}

	public void dialog09C3rd(){
		StartCoroutine(dialogPlay(13,0.8f));
	}

	IEnumerator dialogPlay(int dialogNum,float dialogVol){
		while (fellowDialog.isPlaying) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		fellowDialog.clip = dialogTutorial [dialogNum];
		fellowDialog.volume = dialogVol;
		fellowDialog.Play ();
		Debug.Log (fellowDialog.clip);
		yield return null;
	}


	//AudioManager
	/// Plays a sound by creating an empty game object with an AudioSource
	/// and attaching it to the given transform (so it moves with the transform). Destroys it after it finished playing.
	AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
	{
		//Create an empty game object
		GameObject go = new GameObject ("Audio: " + clip.name);
		go.transform.position = emitter.position;
		go.transform.parent = emitter;
		
		//Create the source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play ();
		Destroy (go, clip.length);
		return source;
	}

	void AudioPlay(AudioClip clip, float volume, float pitch){
		GameObject go = new GameObject ("Audio: " + clip.name);
		GameObject SM;
		SM = GameObject.Find ("SoundManager");
		go.transform.parent = SM.transform;
		
		//Create the source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.Play ();
//		Destroy (go, clip.length);
	}

	void RisingMusicStop(){
//		Debug.Log("Stop Rising or Falling.");
		isRising = false;
	}

	void FallingMusicStop(){
//		Debug.Log("Stop Rising or Falling.");
		isFalling = false;
	}

	IEnumerator FRMusicFadeIn(AudioSource audios, float maxVolumn){
		//		Debug.Log("Start fade in." + "isRisingOrFalling = " + isRising);
		if(audios.isPlaying)
			while (audios.volume <= maxVolumn && isRising){
				//			Debug.Log("volumn up");
				audios.volume += fadeSpeed * Time.deltaTime;
				yield return new WaitForSeconds(Time.deltaTime);
			}
	}

	IEnumerator FRMusicFadeOut(AudioSource audios){
		//		Debug.Log("Start fade out." + "isRisingOrFalling = " + isFalling);
		if(audios.isPlaying)
			while (audios.volume >= 0.01 && isFalling){
				//			Debug.Log("volumn down");
				audios.volume -= fadeSpeed * Time.deltaTime;
				yield return new WaitForSeconds(Time.deltaTime);
			}
	}



	IEnumerator MusicFadeIn(AudioSource audios, float maxVolumn){
//		Debug.Log("Start fade in.");
		audios.Play();
		while (audios.volume < maxVolumn){
			audios.volume += fadeSpeed * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
//		Debug.Log("Complete fade in.");
	}

	IEnumerator MusicFadeIn(AudioSource audios, float maxVolumn, float fadeInSpeed){
		//		Debug.Log("Start fade in.");
		audios.Play ();
		while (audios.volume < maxVolumn){
			audios.volume += fadeInSpeed * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		//		Debug.Log("Complete fade in.");
	}


	IEnumerator MusicFadeOut(AudioSource audios){
//		Debug.Log("Start fade out.");
		while (audios.volume >= 0.01){
			audios.volume -= fadeSpeed * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		audios.Stop ();
	}

	IEnumerator MusicFadeOut(AudioSource audios, float fadeOutSpeed){
		//		Debug.Log("Start fade out.");
		while (audios.volume >= 0.01){
			audios.volume -= fadeOutSpeed * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		audios.Stop ();
	}

}
