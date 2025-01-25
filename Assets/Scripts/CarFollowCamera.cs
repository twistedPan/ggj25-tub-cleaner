using UnityEngine;

public class CarFollowCamera : MonoBehaviour
{
    public PrometeoCarController carController;
    public Transform carCamTransform;
    public Transform CameraTarget;
    public Vector3 CameraTargetOrigin;
    public float OffsetDamping = 2f;
    public float OffsetFactor = 3f;
    public bool applyOffset = true;

    [Header("Debug Values")]
    [SerializeField] private Vector3 carForwartVec;
    [SerializeField] private Vector3 cross;
    [SerializeField] private float targetOffset;
    private float currentOffset;

    void Start()
    {
        carController = FindFirstObjectByType<PrometeoCarController>();
        carCamTransform = transform;
        CameraTargetOrigin = CameraTarget.localPosition;
    }

    void Update()
    {
        carForwartVec = carController.CarVelocityForward;
        cross = Vector3.Cross(carForwartVec.normalized, carCamTransform.forward.normalized);
        targetOffset = cross.y * OffsetFactor;

        if (applyOffset is false) return;

        currentOffset = Mathf.Lerp(currentOffset, targetOffset, Time.deltaTime * OffsetDamping);
        CameraTarget.localPosition = new Vector3(currentOffset, CameraTargetOrigin.y, CameraTargetOrigin.z);
    }
}
