using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    private Vector3 target;

    void Start()
    {
        target = endPoint.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
            target = (target == endPoint.position) ? startPoint.position : endPoint.position;
    }
}
