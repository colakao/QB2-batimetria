using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraneColor : MonoBehaviour
{
    Renderer[] craneRenderers;
    public Color craneColor; 

    void Start()
    {
        craneRenderers = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < craneRenderers.Length; i++)
        {
            craneRenderers[i].material.color = craneColor;
        }
    }

   

}
