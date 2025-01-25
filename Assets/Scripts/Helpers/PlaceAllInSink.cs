using UnityEngine;

public class PlaceAllInSink : MonoBehaviour
{
    public void PlaceAll()
    {
        var allShits = FindObjectsByType<PlaceInSink>(FindObjectsSortMode.None);
        foreach (var sh in allShits)
        {
            sh.Place();
        }
    }
}
