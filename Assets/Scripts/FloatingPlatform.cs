using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Vector3 moveAmplitude = new Vector3(0f, 0.6f, 0f);
    public Vector3 moveSpeed = new Vector3(0f, 0.6f, 0f);
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * moveSpeed.x) * moveAmplitude.x;
        float y = Mathf.Sin(Time.time * moveSpeed.y) * moveAmplitude.y;
        float z = Mathf.Sin(Time.time * moveSpeed.z) * moveAmplitude.z;
        transform.position = startPos + new Vector3(x, y, z);
    }
}
