using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    [Header("Limites dynamiques via murs")]
    public Collider2D wallLeft;
    public Collider2D wallRight;

    public float leftLimit;
    private float rightLimit;
    public float camHalfWidth;



    void Start()
    {
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        camHalfWidth = camWidth / 2f;

        // Déduction des limites en fonction des murs gauche et droite
        leftLimit = wallLeft.bounds.max.x + camHalfWidth;
        rightLimit = wallRight.bounds.min.x - camHalfWidth;
    }

    void LateUpdate()
    {
        float targetX = player.position.x + offset.x;
        targetX = Mathf.Clamp(targetX, leftLimit, rightLimit);

        transform.position = new Vector3(targetX, transform.position.y, offset.z);
    }
}