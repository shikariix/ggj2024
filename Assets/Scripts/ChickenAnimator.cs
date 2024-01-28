using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{

	public float averageGroundPeckTime = 25;
	
	private Animator animator;
	private float lastChangeTime;
	private bool canRandom = true;
	private bool isWalking = false;

	private void Awake() {
		animator = GetComponent<Animator>();

		lastChangeTime = Time.time;
		StartCoroutine(RandomPeckTimer());
	}

	public void SetWalking(bool value) {
		isWalking = value;
		canRandom = !value;
		animator.SetBool("IsWalking", value);
		lastChangeTime = Time.time;
	}

	public void PlayStepSound() {
		if (AudioManager._AudioManager != null)
			AudioManager._AudioManager.PlayOneShot(OneShot.ChickenStep);
	}

	private IEnumerator RandomPeckTimer() {
		yield return new WaitForSeconds(averageGroundPeckTime * Random.Range(.7f, 1.5f));
		if (lastChangeTime + averageGroundPeckTime * .5f < Time.time && canRandom) {
			if (Random.Range(0f, 1f) > .5f) {
				animator.SetTrigger("PeckGround");
			}
			else {
				animator.SetTrigger("TapTap");
			}
			lastChangeTime = Time.time;
		}
		StartCoroutine(RandomPeckTimer());
	}

}
