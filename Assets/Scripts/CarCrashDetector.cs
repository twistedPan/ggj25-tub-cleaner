using System;
using UnityEngine;

public class CarCrashDetector : MonoBehaviour
{
    public Action OnCarCrash;
    private bool hasCrashed;
    public float crashProtectionTime = 2f;
    private float lastCrashTime;

    void Update()
    {
        if (hasCrashed is false) return;

        if (Time.time - lastCrashTime > crashProtectionTime)
        {
            hasCrashed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && hasCrashed is false)
        {
            Debug.Log("Car Crash");
            OnCarCrash?.Invoke();
            hasCrashed = true;
            lastCrashTime = Time.time;
        }
    }
}
