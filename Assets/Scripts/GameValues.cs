using UnityEngine;

[CreateAssetMenu(fileName = "GameValues", menuName = "Scriptable Objects/GameValues")]
public class GameValues : ScriptableObject
{
    [Header("Player")]
    public int MaxSpeed;
    public int BaseAcceleration;
    public int SoapedAcceleration;

    [Header("Camera")]
    public float DistanceZ;
    public float DistanceY;
    public float OffsetDamping;
    public float OffsetFactor;

    [Header("Soap and Dirt")]
    public int SoapCapacity;
    public int DirtStrength;

    [Header("Game")]
    public int TimeLimit;
    public bool EndlessMode;
}
