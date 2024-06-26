using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    private static SceneSwitcher sceneSwitcher;

	private static int currentScene;

	public static SceneSwitcher _SceneSwitcher { get => sceneSwitcher; }
	public static int _CurrentScene { get => currentScene; }

	private void Awake() {
		if (sceneSwitcher == null) {
			sceneSwitcher = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this);
		}
	}


#if UNITY_EDITOR // debugging in-editor only
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha0)) { LoadScene(0); }
		if (Input.GetKeyDown(KeyCode.Alpha1)) { LoadScene(1); }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { LoadScene(2); }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { LoadScene(3); }
		if (Input.GetKeyDown(KeyCode.Alpha4)) { LoadScene(4); }
	}
#endif

	public void LoadScene(ChickenScene scene) { LoadScene((int)scene); }
	public void LoadScene(int value) {
		if (currentScene != value) {
			SceneManager.LoadScene(value);
			currentScene = value;
		}
	}

}


public enum ChickenScene {
	Intro = 0,
	MainMenu = 1,
	Dressup = 2,
	Collect = 3,
	StrikeAPose = 4,
}