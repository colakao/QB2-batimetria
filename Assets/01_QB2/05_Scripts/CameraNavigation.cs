using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigation : MonoBehaviour
{
	public Transform focus;
	public float distance;

	public float rotationSpeed = 90f;
	const float e = 0.001f;

	Vector2 orbitAngles = new Vector2(45f, 0f);

	private void Update()
	{
		if (Input.GetMouseButton(0))
			ManualRotation();


		Vector3 pointOfFocus = focus.position;
		Quaternion lookRotation = Quaternion.Euler(orbitAngles);
		Vector3 lookDirection = lookRotation * Vector3.forward;
		Vector3 lookPosition = pointOfFocus - lookDirection * distance;
		transform.SetPositionAndRotation(lookPosition, lookRotation);
	}

	void ManualRotation()
	{
		Vector2 input = new Vector2
		(
			-Input.GetAxis("Mouse Y"),
			Input.GetAxis("Mouse X")
		);

		if (Mathf.Abs(input.x) > e || Mathf.Abs(input.y) > e)
		{
			//orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
			orbitAngles += rotationSpeed * Time.deltaTime * input;
		}
	}
}

