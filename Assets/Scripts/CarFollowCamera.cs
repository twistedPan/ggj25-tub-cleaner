using Unity.Cinemachine;
using UnityEngine;

public class CarFollowCamera : MonoBehaviour
{
    public GameValues GameValues;
    public PrometeoCarController carController;
    public Transform carCamTransform;
    public Transform CameraTarget;
    public Vector3 CameraTargetOrigin;
    public bool applyOffset = true;
    private CinemachineThirdPersonFollow followThirdPerson;

    [Header("Debug Values")]
    [SerializeField] private Vector3 carForwartVec;
    [SerializeField] private Vector3 cross;
    [SerializeField] private float targetOffset;
    private float currentOffset;

    void Start()
    {
        carController = FindFirstObjectByType<PrometeoCarController>();
        carCamTransform = transform;
        followThirdPerson = GetComponent<CinemachineThirdPersonFollow>();
        CameraTargetOrigin = CameraTarget.localPosition;

        followThirdPerson.CameraDistance = GameValues.CamDistanceZ;
        followThirdPerson.VerticalArmLength = GameValues.VerticalArmLength;
    }

    void Update()
    {
        carForwartVec = carController.CarVelocityForward;
        cross = Vector3.Cross(carForwartVec.normalized, carCamTransform.forward.normalized);
        targetOffset = cross.y * GameValues.OffsetFactor;

        if (applyOffset is false) return;

        currentOffset = Mathf.Lerp(currentOffset, targetOffset, Time.deltaTime * GameValues.OffsetDamping);
        CameraTarget.localPosition = new Vector3(currentOffset, GameValues.CamTargetDistanceY, GameValues.CamTargetDistanceZ);
    }
}
