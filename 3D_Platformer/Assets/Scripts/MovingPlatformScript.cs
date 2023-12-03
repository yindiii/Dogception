using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 0.2f;

    private float timeCounter = 0;

    void Update()
    {
        // Move the platform back and forth between pointA and pointB
        timeCounter += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(timeCounter, 1));
    }

    public Vector3 GetPlatformMovement()
    {
        return Vector3.Lerp(pointA, pointB, Mathf.PingPong((timeCounter + Time.deltaTime * speed), 1)) - transform.position;
    }
}
