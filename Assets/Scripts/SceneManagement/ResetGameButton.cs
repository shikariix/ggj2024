using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResetGameButton : MonoBehaviour
{

	public GameObject[] disableOnStart;
	
	private Button button;
	
	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());
	}

	private void Start() {
		foreach(GameObject gameObject in disableOnStart) {
			gameObject.SetActive(false);
		}
		StartCoroutine(DisableAfterFrames());
	}

	public void ButtonPressed() {
		DressupOutfit._DressupOutfit.Reset();
		Inventory._Inventory.ResetInventory();
	}

	private IEnumerator DisableAfterFrames() {
		yield return null;
		foreach (GameObject gameObject in disableOnStart) {
			gameObject.SetActive(true);
			yield return null;
			gameObject.SetActive(false);
		}
	}
}
