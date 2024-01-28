using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{

    private Button button;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

#if !UNITY_STANDALONE_WIN

		Destroy(this.gameObject);

#endif

	}


#if !UNITY_STANDALONE_WIN
	private void OnEnable() {
		Destroy(this.gameObject);
	}
#endif

	public void ButtonPressed() {
		Application.Quit();
	}
}
