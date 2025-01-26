using UnityEngine;

[CreateAssetMenu(fileName = "GameValues", menuName = "Scriptable Objects/GameValues")]
public class GameValues : ScriptableObject
{
    [Header("Player")]
    public int MaxSpeed = 300;
    public int BaseAcceleration = 20;
    public int MaxSoapedAcceleration = 10;

    [Header("Camera")]
    public float CamTargetDistanceZ = -5;
    public float CamTargetDistanceY = 3;
    public float CamDistanceZ = 8;
    public float VerticalArmLength = 0.8f;
    public float OffsetDamping = 2;
    public float OffsetFactor = 3;

    [Header("Soap and Dirt")]
    public int MaxSoapCapacity = 100;
    public int SoapReplenishmentCapacity = 100;
    public int DirtCleaningCost = 10;

    [Header("Game")]
    public int TimeLimitInSeconds = 60;
    public bool EndlessMode = false;
}
