using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SweepSFX : MonoBehaviour
{
    [Header("Volume")]
    public float StartVolume = 0.0f;
    public float EndVolume = 22f;
    public float MinVolume = 0.0f;
    public float MaxVolume = 1.0f;

    [Header("Pitch")]
    public float StartPitch = 0.0f;
    public float EndPitch = 10f;
    public float MinPitch = 0.8f;
    public float MaxPitch = 1.2f;

    private PrometeoCarController carController;
    private Rigidbody carRb;
    private AudioSource audioSource;

    public bool debugging = false;

    void Start()
    {
        carController = GetComponentInParent<PrometeoCarController>();
        carRb = carController.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float mag = carRb.linearVelocity.magnitude;
        var sweepVolume = mag.MapRange(StartVolume, EndVolume, MinVolume, MaxVolume);
        audioSource.volume = sweepVolume;

        var localVelocityX = Mathf.Abs(transform.InverseTransformDirection(carRb.linearVelocity).x);
        var speepPitch = localVelocityX.MapRange(StartPitch, EndPitch, MinPitch, MaxPitch);
        audioSource.pitch = speepPitch;

        if (debugging)
            Debug.Log($"mag: {mag.RoundToDecimal(2)}, localVelocityX: {localVelocityX.RoundToDecimal(2)}");
    }
}
