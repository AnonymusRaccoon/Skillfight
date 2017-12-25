using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	public enum RotationAxes {MouseXandY = 0, MouseX = 1, MouseY = 2}
	public RotationAxes axes = RotationAxes.MouseXandY;
	public float sensitivityX = 6F;
	public float sensitivityY = 6F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public GameObject cam;


	void FixedUpdate () {
	
		if (axes == RotationAxes.MouseXandY) {

			float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			cam.transform.localEulerAngles = new Vector3 (-rotationY, 0, 0);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, rotationX, 0);

		} else if (axes == RotationAxes.MouseX) {

			transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);

		} else {

			rotationY += Input.GetAxis ("MouseY") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			cam.transform.localEulerAngles = new Vector3 (-rotationY, 0, 0);
		}
	}
}