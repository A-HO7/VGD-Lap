using UnityEngine;
using EasyDoorSystem; // Required to see  EasyDoor script
public class SceneStartCloseDoor : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Time in seconds before the door starts closing")]
    [SerializeField] private float closeDelay = 2.0f;

    private EasyDoor door;

    private void Awake()
    {
        door = GetComponent<EasyDoor>();
    }

    private void Start()
    {
        // 1. Force the logic to acknowledge the door is currently Open
        // (We use a trick here so we don't have to rewrite our main script logic)
        SetDoorStateToOpen();

        // 2. Trigger the close function after the delay
        Invoke(nameof(TriggerClose), closeDelay);
    }

    private void TriggerClose()
    {
        if (door != null)
        {
            door.CloseDoor();
        }
    }

    // Helper to force the 'IsOpen' variable to true using Reflection
    // This allows us to bypass the 'private set' without editing your EasyDoor.cs
    private void SetDoorStateToOpen()
    {
        System.Reflection.PropertyInfo isOpenProp = typeof(EasyDoor).GetProperty("IsOpen");
        if (isOpenProp != null)
        {
            isOpenProp.SetValue(door, true, null);
        }
    }
}
