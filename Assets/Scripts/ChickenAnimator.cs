using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{

	public float averageGroundPeckTime = 25;
	
	private Animator animator;
	private float lastChangeTime;
	private bool canRandom = true;

	private Vector3 scale = Vector3.one;

	private void Awake() {
		animator = GetComponent<Animator>();

		lastChangeTime = Time.time;
		StartCoroutine(RandomPeckTimer());

		if (GetComponent<DressupChicken>() != null) {
			if (!GetComponent<DressupChicken>().isMainCharacter) {
				scale.x = Mathf.Sign(Random.Range(-1f, 1f));
				transform.localScale = scale;
				StartCoroutine(RandomDirectionSwap());
			}
		}
	}

	public void SetWalking(bool value) {
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

	private IEnumerator RandomDirectionSwap() {
		yield return new WaitForSeconds(Random.Range(2f, 15f));
		scale.x *= -1;
		transform.localScale = scale;
		StartCoroutine(RandomDirectionSwap());
	}

}
