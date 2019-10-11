using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectTap : MonoBehaviour
{
    public UnityEvent OnTap;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnTap != null)
            {
                OnTap.Invoke();
            }
        }
        if (Input.touchCount > 0)
        {
            if (OnTap != null)
            {
                OnTap.Invoke();
            }
        }
    }
}
