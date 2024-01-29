using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopDoor : MonoBehaviour
{

    public GameObject inside;
    public GameObject outside;

    public void UseCoopDoor() {
        if (inside == null) {
            SceneSwitcher._SceneSwitcher.LoadScene(ChickenScene.Dressup);
		}
		else if (outside.activeSelf) {
            outside.SetActive(false);
            inside.SetActive(true);
		}
		else {
            outside.SetActive(true);
            inside.SetActive(false);
        }
        if (AudioManager._AudioManager != null)
            AudioManager._AudioManager.PlayOneShot(OneShot.ButtonPress);
	}

}
