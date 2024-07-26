using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
    }

    void Update()
    {
        float dist = (cam.transform.position.x * parallaxEffect * 2);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
