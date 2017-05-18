using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour {

	public float rotSpeed;
	public float minRot = - 10;
	public float maxRot = 10;

	public float minSize;
	public float maxSize;
	public float sizeSpeed = 0.005f;

	public Quaternion targetRot;

	public Vector3 targetSize;

	void Start() {
		NewTarget ();
		NewSize ();
	}

	void ApplyScaleRate() {
		transform.localScale += Vector3.one * sizeSpeed;
	}

	void Update() {

		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, rotSpeed * Time.deltaTime);

		if (transform.rotation == targetRot) {
			NewTarget ();
		}
		if (transform.localScale.x < minSize) {
			sizeSpeed = Mathf.Abs (sizeSpeed);
		} else if (transform.localScale.x > maxSize) {
			sizeSpeed = -Mathf.Abs (sizeSpeed);
		}
		ApplyScaleRate ();
	
	}

	void NewTarget() {
		targetRot = Quaternion.Euler (0, 0, Random.Range (minRot, maxRot));
	}
	void NewSize() {
		targetSize = new Vector3 (Random.Range (minSize, maxSize), Random.Range (minSize, maxSize), Random.Range (minSize, maxSize)); 
	}
}
