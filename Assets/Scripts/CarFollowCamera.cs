using UnityEngine;

public class CarFollowCamera : MonoBehaviour
{
    public PrometeoCarController carController;
    public Transform carCamTransform;
    private Vector3 carForwartVec = Vector3.zero;

    void Start()
    {
        carController = FindFirstObjectByType<PrometeoCarController>();
        carCamTransform = transform;
    }

    void Update()
    {
        carForwartVec = carController.CarVelocityForward;
        //carCamTransform.rotation = Quaternion.LookRotation(carForwartVec);
    }
}
