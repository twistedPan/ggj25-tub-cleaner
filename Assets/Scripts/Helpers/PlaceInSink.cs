using UnityEngine;

public class PlaceInSink : MonoBehaviour
{
    public void Place()
    {
        var startPos = transform.position + Vector3.up * 2;
        if (Physics.Raycast(startPos, -transform.forward, out RaycastHit hit, 1000f))
        {
            transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
        }
        else
        {
            Debug.LogWarning("No sink found");
        }
    }

    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(-90f,0,0);
        transform.position += Vector3.up * 3f;
    }

    private void OnDrawGizmosSelected()
    {
        var startPos = transform.position + Vector3.up * 10;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startPos, -transform.forward * 100);
    }
}
