using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float timePassed = 0;
    private ChickenAnimator chickenAnimator;

    private void Start()
    {
        chickenAnimator = GetComponent<ChickenAnimator>();
        
        startPosition = transform.position;
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                timePassed = 0;
                startPosition = transform.position;
                targetPosition = hit.point;
                if (hit.point.x > startPosition.x) {
                    transform.localScale = new Vector3(-1, 1, 1);
				}
				else {
                    transform.localScale = Vector3.one;
                }
            }
        }

        if (targetPosition != transform.position)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (timePassed += Time.deltaTime));
        }

        if (chickenAnimator != null) {
            chickenAnimator.SetWalking(Vector3.Distance(targetPosition, transform.position) > .3f);
		}
    }
}
