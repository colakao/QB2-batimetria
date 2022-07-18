using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Scriptable Objects/Grua/Tasks")]
public class GruaTasks : MonoBehaviour
{
    [Serializable]
    private class Tareas
    {
        public GameObject targetObj;
        public float startTime;
        public float endTime;
    }

    [SerializeField] private Tareas[] gruaTasks;

    public GameObject hookObj;
    private GameObject currentTarget;
    private Vector3 currentTargetPos;
    private Vector3 hookPosition;
    private Vector3 desiredPos;
    private Vector3 currentPos;
    public float hookSpeed = 0.8f;
    public Vector3 hookOffset;

    private void Start()
    {
        //hookPosition = hookObj.transform.position;
        currentTarget = gruaTasks[0].targetObj;
        currentTargetPos = currentTarget.transform.position;
    }

    void Update()
    {
        SetTargetsByTime();        
        FollowHook();
    }
    private void SetTargetsByTime()
    {
        for (int i = 0; i < gruaTasks.Length; i++)
        {
            if (Time.time > gruaTasks[i].startTime && Time.time < gruaTasks[i].endTime)
            {
                currentTarget = gruaTasks[i].targetObj;
            }            
        }
    }
    

    private void FollowHook()
    {
        currentPos = hookObj.transform.position;
        currentTargetPos = currentTarget.transform.position;
        desiredPos = Vector3.Lerp(currentPos, currentTargetPos, hookSpeed);
        hookObj.transform.position = desiredPos;
    }
}
