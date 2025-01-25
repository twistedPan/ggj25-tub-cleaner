using System;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    public Action OnPlayerDied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player died");
            OnPlayerDied?.Invoke();
        }
    }
}
