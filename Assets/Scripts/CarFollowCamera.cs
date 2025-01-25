using Unity.Cinemachine;
using UnityEngine;

public class CarFollowCamera : MonoBehaviour
{
    public PrometeoCarController carController;
    public Transform carCamTransform;
    public Transform CameraTarget;
    public Vector3 CameraTargetOrigin;
    public float OffsetDamping = 1f;
    public float OffsetFactor = 1f;
    [Header("Debug Values")]
    [SerializeField] private Vector3 carForwartVec;
    [SerializeField] private Vector3 cross;
    [SerializeField] private float targetOffset;

    private CinemachineCamera carCam;
    public bool applyOffset = true;

    void Start()
    {
        carController = FindFirstObjectByType<PrometeoCarController>();
        carCamTransform = transform;
        carCam = GetComponent<CinemachineCamera>();
        CameraTargetOrigin = CameraTarget.localPosition;
    }

    void Update()
    {
        carForwartVec = carController.CarVelocityForward;
        cross = Vector3.Cross(carForwartVec.normalized, carCamTransform.forward.normalized);
        targetOffset = cross.y * OffsetFactor;

        if (applyOffset is false) return;

        CameraTarget.localPosition = new Vector3(targetOffset, CameraTargetOrigin.y, CameraTargetOrigin.z);
        //camHardLookAt.LookAtOffset.x = Mathf.Lerp(camHardLookAt.LookAtOffset.x, targetOffset, Time.deltaTime * OffsetDamping);
    }
}
