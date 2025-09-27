using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, 0, 180f); // deg/sec

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
