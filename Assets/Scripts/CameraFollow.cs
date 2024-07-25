using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    private void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}