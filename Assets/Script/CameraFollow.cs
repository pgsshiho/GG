using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset; // 카메라와 플레이어 사이의 거리

    void LateUpdate()
    {
        // 카메라의 위치를 플레이어의 위치에 오프셋만큼 더해서 설정
        transform.position = player.position + offset;
    }
}
