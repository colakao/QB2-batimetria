using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDrift : MonoBehaviour
{
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
