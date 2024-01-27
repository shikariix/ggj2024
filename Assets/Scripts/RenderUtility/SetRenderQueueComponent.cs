using UnityEngine;

/// <summary>
/// Sets the render queue or sorting order of a material instance
/// This is necessary for some (standard) shaders as it is (often) not an exposed property
/// It can be used to reduce Z-fighting or fix render order problems
/// </summary>
public class SetRenderQueueComponent : MonoBehaviour {

	public SortingType sortingType;
	public int value = 3000;

	public enum SortingType {
		RenderQueue = 0,
		SortingOrder = 1
		// OrderInLayer = 2
	}

	private Renderer rend;
	private Material material;

	private void Start() {
		switch (sortingType) {
			case SortingType.RenderQueue:
				material = transform.GetComponent<Renderer>().material;
				material.renderQueue = value;
				break;
			case SortingType.SortingOrder:
				rend = transform.GetComponent<Renderer>();
				rend.sortingOrder = value;
				break;
		}
	}
}
