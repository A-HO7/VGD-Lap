using UnityEngine;
using UnityEngine.Analytics;

public class Pickup : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ANALYTICS EVENT
            Analytics.CustomEvent("item_collected", new System.Collections.Generic.Dictionary<string, object>
            {
                { "item_value", value },
                { "item_name", gameObject.name },
                { "position_x", transform.position.x },
                { "position_y", transform.position.y },
                { "position_z", transform.position.z }
            });

            // ADD SCORE
            GameManager.Instance.AddScore(value);

            // ADD UI COUNTER
            if (UIOrbCounter.Instance != null)
                UIOrbCounter.Instance.AddOrb(value);

            Destroy(gameObject);
        }
    }
}
