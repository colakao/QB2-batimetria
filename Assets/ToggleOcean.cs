using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOcean : MonoBehaviour
{
    public void ToggleGameObject(GameObject obj)
	{
		obj.SetActive(!obj.activeInHierarchy);
	}
}
