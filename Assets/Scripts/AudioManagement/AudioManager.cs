using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioClip musicClip;
	public AudioClip applauseClip;

	[Header("OneShots")]
	public AudioClip[] buttonHover;
	public AudioClip[] buttonPress;
	public AudioClip[] dialogueAdvance;
	public AudioClip[] chickenStep;

	[Header("Chaos Chickens")]
	public AudioClip chickensClip;
	public AudioClip chickensReverbClip;
	public AnimationCurve volumeCurve;
	public float chickensCurveLength = 15;
	public float chickensReverbCurveLenght = 20;
	public float chickensVolumeMultiplier = .3f;
	public float chickensReverbVolumeMultiplier = .5f;

	[Header("Tools For Us")]
	public bool noMusic = false;

	private static AudioManager audioManager;

	private float volume = 1;
	private int currentScene = -1;
	private AudioSource musicSource;
	private AudioSource oneShotSource;
	private AudioSource chickensSource;
	private AudioSource chickensReverbSource;
	private AudioSource applauseSource;

	private float chickensVolume;
	private float chickensReverbVolume;

	public static AudioManager _AudioManager { get => audioManager; }
	public float Volume { get => volume; set => SetVolume(value); }

	private void Awake() {
		if (audioManager == null) {
			audioManager = this;
			DontDestroyOnLoad(this.gameObject);
			InitializeAudio();
		}
		else {
			Destroy(this);
		}
	}

	private void InitializeAudio() {
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.playOnAwake = false;
		musicSource.loop = true;
		musicSource.clip = musicClip;
		musicSource.volume = noMusic ? 0 : volume;
		StartMusic();

		oneShotSource = gameObject.AddComponent<AudioSource>();
		oneShotSource.playOnAwake = false;
		oneShotSource.loop = false;

		chickensSource = gameObject.AddComponent<AudioSource>();
		chickensSource.playOnAwake = true;
		chickensSource.loop = true;
		chickensSource.clip = chickensClip;
		chickensSource.enabled = false;

		chickensReverbSource = gameObject.AddComponent<AudioSource>();
		chickensReverbSource.playOnAwake = true;
		chickensReverbSource.loop = true;
		chickensReverbSource.clip = chickensReverbClip;
		chickensReverbSource.enabled = false;

		applauseSource = gameObject.AddComponent<AudioSource>();
		applauseSource.playOnAwake = true;
		applauseSource.loop = true;
		applauseSource.clip = applauseClip;
		applauseSource.enabled = false;
	}

	private void Update() {
		if (SceneSwitcher._SceneSwitcher != null) {
			if (currentScene != SceneSwitcher._CurrentScene) {
				currentScene = SceneSwitcher._CurrentScene;
				chickensSource.enabled = currentScene > 2;
				chickensReverbSource.enabled = currentScene == 3;
				applauseSource.enabled = currentScene == 4;
				if (currentScene == 0) {
					StartMusic();
				}
			}

			if (currentScene > 2) {
				chickensVolume = volumeCurve.Evaluate(Mathf.Clamp01((Time.time / chickensCurveLength) % 1)) * chickensVolumeMultiplier * volume;
				chickensReverbVolume = volumeCurve.Evaluate(Mathf.Clamp01((Time.time / chickensReverbCurveLenght) % 1)) * chickensReverbVolumeMultiplier * volume;
				chickensSource.volume = chickensVolume;
				chickensReverbSource.volume = chickensReverbVolume;
			}
		}
	}



	public void SetVolume(float value) {
		volume = value;
		musicSource.volume = value;
		musicSource.mute = value < .1f || noMusic;
		oneShotSource.volume = value;
		oneShotSource.mute = value < .1f;

		chickensSource.volume = value;
		chickensSource.mute = value < .1f;
		chickensReverbSource.volume = value;
		chickensReverbSource.mute = value < .1f;

		applauseSource.volume = value;
		applauseSource.mute = value < .1f;
	}

	public void StartMusic() {
		if (!noMusic)
			musicSource.Play();
	}

	public void StopMusic() {
		musicSource.Stop();
	}

	public void PlayOneShot(OneShot oneShot) {
		if (volume < .1f)
			return;

		oneShotSource.Stop();
		oneShotSource.volume = volume;
		AudioClip clip = null;
		switch (oneShot) {
			case OneShot.ButtonHover:
				clip = buttonHover[Random.Range(0, buttonHover.Length)];
				oneShotSource.volume = volume * .3f;
				break;
			case OneShot.ButtonPress:
				clip = buttonPress[Random.Range(0, buttonPress.Length)];
				break;
			case OneShot.DialogueAdvance:
				clip = dialogueAdvance[Random.Range(0, dialogueAdvance.Length)];
				break;
			case OneShot.ChickenStep:
				clip = chickenStep[Random.Range(0, chickenStep.Length)];
				break;
		}
		oneShotSource.clip = clip;
		oneShotSource.Play();
	}
}

public enum OneShot {
	ButtonHover = 1,
	ButtonPress = 2,
	DialogueAdvance = 3,
	ChickenStep = 4
}
