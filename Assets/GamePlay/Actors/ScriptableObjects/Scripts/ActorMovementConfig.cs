using UnityEngine;


[CreateAssetMenu(fileName = "ActorMovementConfig", menuName = "Actor/MovementConfig", order = 2)]
public class ActorMovementConfig : ScriptableObject
{
    //[Header("Stats")]
    //public PlayerStats playerStats;
    [Header("Movimiento")]
    public float movementSpeed = 5;
    public float acceleration = 1;
    public float deceleration = 1;

    [Header("Salto")]
    public float jumpSpeed;
    [Range(0, 1)] public float airMomentum = 0.8f;
    [Range(1, 5)] public int jumpMax = 1;

    [Header("Invulnerability")]
    public Color invulnerabilityColor = Color.red;
    [Range(0, 3)] public float invulnerabilitySeconds = 1f;

    private void OnValidate()
    {
        // Ensure movement speed is positive
        if (movementSpeed < 0)
        {
            Debug.LogWarning($"{nameof(movementSpeed)} cannot be negative. Resetting to default value of 5.");
            movementSpeed = 5f;
        }

        // Ensure acceleration and deceleration are non-negative
        acceleration = Mathf.Max(0f, acceleration);
        deceleration = Mathf.Max(0f, deceleration);

        // Ensure jumpSpeed is positive
        if (jumpSpeed < 0)
        {
            Debug.LogWarning($"{nameof(jumpSpeed)} cannot be negative. Resetting to default value of 5.");
            jumpSpeed = 5f;
        }

        // Ensure invulnerabilitySeconds is within range
        if (invulnerabilitySeconds < 0f || invulnerabilitySeconds > 3f)
        {
            Debug.LogWarning($"{nameof(invulnerabilitySeconds)} must be between 0 and 3. Clamping to valid range.");
            invulnerabilitySeconds = Mathf.Clamp(invulnerabilitySeconds, 0f, 3f);
        }

        // Ensure airMomentum is within valid range
        airMomentum = Mathf.Clamp(airMomentum, 0f, 1f);

        // Ensure jumpMax is at least 1
        jumpMax = Mathf.Max(1, jumpMax);
    }
}

