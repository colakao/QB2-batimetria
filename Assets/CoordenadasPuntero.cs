using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Camera))]
public class CoordenadasPuntero : MonoBehaviour
{
	public GameObject pointerFeedback;
    public TMP_Text tmpTextCoord;
	public TMP_Text tmpTextProf;

	private GameObject _pointerObject;
	private Camera _mainCam;
	private Vector3 _worldPosition;

	//Physics
	private Ray _ray;

	private void Awake()
	{
		_mainCam = GetComponent<Camera>();
		//_terrainCollider = terrain.GetComponent<MeshCollider>();
		_pointerObject = Instantiate(pointerFeedback, this.transform);
	}

	private Vector3 GetCoords()
	{
		var coords = new Vector3();
		return coords;
	}

	private void FixedUpdate()
	{
		//cam = Camera.main.transform;
		//Vector3 cameraRelative = cam.InverseTransformPoint(transform.position);
		_ray = _mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(_ray, out hit, 1000))
		{
			_worldPosition = hit.point;
			_pointerObject.transform.position = hit.point;
		}

		//var vector = Quaternion.AngleAxis(-2.591f, Vector3.up) * _worldPosition;
		var vector = new Vector3(375800, 0, 7701000) - Quaternion.AngleAxis(2.591f, Vector3.down) * _worldPosition;


		tmpTextCoord.text = "Coordenadas (m): " + "(x: " + vector.x.ToString("#.00") + ", y: " + vector.z.ToString("#.00") + ")";
		tmpTextProf.text = "Profundidad (m): " + "z: " + _worldPosition.y.ToString("#.00");
	}
}
