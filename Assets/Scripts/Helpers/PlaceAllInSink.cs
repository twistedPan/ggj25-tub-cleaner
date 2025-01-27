using UnityEngine;

public class PlaceAllInSink : MonoBehaviour
{
    public bool PlaceChildren = false;
    public void PlaceAll()
    {
        if (PlaceChildren)
        {
            var allShitsInGroup = GetComponentsInChildren<PlaceInSink>();
            foreach (var sh in allShitsInGroup)
            {
                sh.Place();
            }

            return;
        }

        var allShits = FindObjectsByType<PlaceInSink>(FindObjectsSortMode.None);
        foreach (var sh in allShits)
        {
            sh.Place();
        }
    }
}
