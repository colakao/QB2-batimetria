using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class CameraNavigationMouse : MonoBehaviour
{
	[Header("Variables de velocidad")]
	public float orbitSpeed = 3f;
	public float panSpeed = 5f;
	public float zoomSpeed = 20f;

	[Header("Opciones zoom"), Tooltip("Distancia relativa a viewFocus.")]
	public float minZoom = 0f;
	[Tooltip("Distancia relativa a viewFocus.")]
	public float maxZoom = 320f;

	[Header("Cam view/target")]
	public Transform[] viewsPos;
	public Transform[] viewsFocus;

	[Tooltip("Valor que equivale a segundos de traslado.")]
	public float cameraTravelSpeed = 4f;

	#region private variables
	private Coroutine _cameraTravelCoroutine;
	private Camera _cam;

	private bool _initialized = false;
	private bool draggingUI;

	private Vector3 _virtualPosition;
	private Vector3 _virtualFocus;

	private float zoomLevel = 0f;
	private float panSpeedScaled;
	private float focusDist;
	private float camDist;
	#endregion

	[Space(), Header("Índice de cam view/target")]
	public int index;

	private void Awake()
	{
		_cam = GetComponent<Camera>();
	}

	//Si tenemos poblado "viewsPos" se ejecuta el método SetCameraTransforms(). Si no tenemos nada nos tira un warning. 
	void Start()
	{
		if (viewsPos.Length > 0)
			SetCameraTransforms(0);
		else
			Debug.LogWarning("It is highly recommended to set position and focus transforms.");
	}

	//Este método revisa si tenemos el mismo número de pos que de focus.
	//Luego resetea la posición y foco virtual.
	//	(Usamos virtual para no mover los transform originales de posición y foco).
	//Finalmente ejecuta el método para desplazar la cámara a su objetivo de posición y foco.

	public void SetCameraTransforms(int index)
	{
		if (viewsFocus.Length != viewsPos.Length)
			Debug.LogError("The number of focus transforms (viewsFocus) doesn't match the number of camera transforms (viewsPos).");
		
		_virtualPosition = viewsPos[index].position;
		_virtualFocus = viewsFocus[index].position;

		_cameraTravelCoroutine = StartCoroutine(CameraTravel());
	}

	private IEnumerator CameraTravel()
	{
		var time = 0;
		//esto debería correr una sola vez. Cuando se inicializa el script.
		if (!_initialized)
		{
			//Debug.Log("Camera position initialized.");

			transform.position = _virtualPosition;
			transform.LookAt(_virtualFocus);

			_initialized = true;
			yield break;
		}

		//Corre en paralelo hasta que llegue a su posición de destino
		while (true)
		{
			yield return null;

			if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.mouseScrollDelta.y > 0.1f)
				yield break;

			transform.position = Vector3.Lerp(transform.position, _virtualPosition, (time + Time.deltaTime)/cameraTravelSpeed);
			transform.LookAt(_virtualFocus);

			if (camDist < 0.05f)
			{
				transform.position = _virtualPosition;
				transform.LookAt(_virtualFocus);

				yield break;
			}
		}
	}

	private void CameraZoom()
	{
		var mouseScrollDelta = Input.mouseScrollDelta.y;
		if (focusDist > maxZoom)
		mouseScrollDelta = Mathf.Clamp(mouseScrollDelta, 0, Mathf.Infinity);
		if (focusDist < minZoom)
			mouseScrollDelta = Mathf.Clamp(mouseScrollDelta, Mathf.NegativeInfinity, 0);
		transform.position += focusDist / 2 * mouseScrollDelta * zoomSpeed * transform.forward * Time.deltaTime;
	}

	void Update()
	{
		focusDist = Vector3.Distance(transform.position, _virtualFocus);
		camDist = Vector3.Distance(transform.position, _virtualPosition);
		CameraZoom();
		//DraggingUI();		
		if (!draggingUI)
        {
			MouseControls();
		}	
	}

	public void EnableCameraMove()
    {
		draggingUI = false;
	}
	public void DisableCameraMove()
    {
		draggingUI = true;
    }

	private void MouseControls()
	{
		// Left Mouse to Orbit
		if (Input.GetMouseButton(0))
		{
			//float mouseMove = Math.Abs(Input.GetAxis("Mouse X"));
			transform.RotateAround(_virtualFocus, Vector3.up, Input.GetAxis("Mouse X") * orbitSpeed);

			float pitchAngle = Vector3.Angle(Vector3.up, transform.forward);
			float pitchDelta = -Input.GetAxis("Mouse Y") * orbitSpeed;
			float newAngle = Mathf.Clamp(pitchAngle + pitchDelta, 0f, 180f);
			pitchDelta = newAngle - pitchAngle;

			transform.RotateAround(_virtualFocus, transform.right, pitchDelta);
		}

		// Right Mouse To Pan
		if (Input.GetMouseButton(1))
		{
			panSpeedScaled = Mathf.Clamp(focusDist, 0.1f, maxZoom) * panSpeed * 0.5f * Time.deltaTime;

			Vector3 offset = transform.right * -Input.GetAxis("Mouse X") * panSpeedScaled + transform.up * -Input.GetAxis("Mouse Y") * panSpeedScaled;

			transform.position += offset;
			_virtualFocus += offset;
		}
	}
}