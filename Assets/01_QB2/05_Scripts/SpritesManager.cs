using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesManager : MonoBehaviour
{
    public Image prefabUi;
    private Image uiUse;
    // Start is called before the first frame update
    void Start()
    {
        uiUse = Instantiate(prefabUi, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
