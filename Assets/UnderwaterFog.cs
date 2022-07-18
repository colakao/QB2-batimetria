// Author : asimov99@outlook.com

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attach this script on CAMERA First Person
[RequireComponent(typeof(Camera))]
public class UnderwaterFog : MonoBehaviour
{

    public bool showSurfaceWaterInTheWater = true; // Set true if you use WaterProDayTime for example
    public Color colorInTheWater = new Color(0f, 0.4f, 0.7f, 0.6f);
    public float densityInTheWater = 0.04f;

    private GameObject[] tabWater;
    private Transform transform;
    private Camera camera;
    private GameObject oldWaterActive;
    // FOG SAVE
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;

    void Start()
    {
        // All layer water (Layer n°4)
        tabWater = FindGameObjectsWithLayer(4);
        // Camera player
        camera = GetComponent<Camera>();
        // Transform player
        transform = camera.transform;
    }

    void Update()
    {
        GameObject waterActive = getWaterByTransformPosition();
        if (waterActive != null && oldWaterActive == null)
        {
            // Player go in the water, save FOG
            defaultFog = RenderSettings.fog;
            defaultFogColor = RenderSettings.fogColor;
            defaultFogDensity = RenderSettings.fogDensity;
            // Go to water view
            setFOGSettings(true, colorInTheWater, densityInTheWater);
            if (showSurfaceWaterInTheWater)
            {
                waterActive.transform.Rotate(180f, 0f, 0f);
            }
            //
            oldWaterActive = waterActive;
        }
        else if (waterActive == null && oldWaterActive != null)
        {
            // FOG return to normal view
            setFOGSettings(defaultFog, defaultFogColor, defaultFogDensity);
            if (showSurfaceWaterInTheWater)
            {
                oldWaterActive.transform.Rotate(-180f, 0f, 0f);
            }
            //
            oldWaterActive = null;
        }
    }

    // Return the GameObject Water if transform is in a layer water
    private GameObject getWaterByTransformPosition()
    {
        for (int i = 0; i < tabWater.Length; i++)
        {
            GameObject water = tabWater[i];
            // Check y level
            if (water.transform.position.y > transform.position.y)
            {
                // Check x and z position
                Renderer renderer = water.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (renderer.bounds.Contains(new Vector3(transform.position.x, water.transform.position.y, transform.position.z)))
                    {
                        return water;
                    }
                }
            }
        }
        return null;
    }

    // Set FOG settings
    private void setFOGSettings(bool fog, Color color, float density)
    {
        RenderSettings.fog = fog;
        RenderSettings.fogColor = color;
        RenderSettings.fogDensity = density;
    }

    // Return table with all objects with layer xxx
    private GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        else
        {
            return goList.ToArray();
        }
    }
}
