using UnityEngine;

/// <summary>
/// EosFollow - follows a target smoothly while adding a hover/bob effect and pulsing the attached light.
/// Put this on the Eos prefab (no other scripts should overwrite transform).
/// </summary>
public class EosFollow : MonoBehaviour
{
    [Header("Follow")]
    public Transform target;                     // assign player transform
    public Vector3 followOffset = new Vector3(0.6f, 0.8f, 0f);
    public float followSpeed = 5f;               // lerp speed

    [Header("Hover (local)")]
    public bool enableHover = true;
    public float hoverAmplitude = 0.15f;         // how high it bobs
    public float hoverFrequency = 1.5f;          // how fast it bobs
    private Vector3 hoverOffset = Vector3.zero;

    [Header("Light Pulse")]
    public Light eosLight;                       // assign the Light component (Point or Spot)
    public float baseRange = 2f;
    public float pulseAmount = 0.4f;
    public float pulseSpeed = 2f;

    [Header("Urgency")]
    public Color normalColor = Color.white;
    public Color urgentColor = Color.red;
    public float urgentPulseSpeed = 6f;

    // internal
    Vector3 _targetPosition;                     // computed desired world position (target + followOffset)
    Vector3 _spawnPosition;                      // if no target, hover around this
    float _time;

    void Awake()
    {
        _spawnPosition = transform.position;
        if (eosLight != null)
        {
            baseRange = eosLight.range;
            eosLight.color = normalColor;
        }
    }

    void Update()
    {
        _time += Time.deltaTime;

        // Compute desired world position
        if (target != null)
            _targetPosition = target.position + followOffset;
        else
            _targetPosition = _spawnPosition;

        // Compute hover offset (local up)
        hoverOffset = Vector3.up * (enableHover ? Mathf.Sin(_time * hoverFrequency) * hoverAmplitude : 0f);

        // Smooth follow: move current position toward the desired position
        Vector3 desiredWorld = _targetPosition + hoverOffset;
        transform.position = Vector3.Lerp(transform.position, desiredWorld, 1f - Mathf.Exp(-followSpeed * Time.deltaTime));

        // Light pulsing
        if (eosLight != null)
        {
            eosLight.range = baseRange + Mathf.Sin(_time * pulseSpeed) * pulseAmount;
        }
    }

    /// <summary>
    /// Use to toggle urgent behaviour (called by gameplay scripts).
    /// </summary>
    public void SetUrgent(bool urgent)
    {
        pulseSpeed = urgent ? urgentPulseSpeed : 2f;
        if (eosLight != null) eosLight.color = urgent ? urgentColor : normalColor;
    }

    /// <summary>
    /// Optional: assign target at runtime
    /// </summary>
    public void SetTarget(Transform t)
    {
        target = t;
    }
}
