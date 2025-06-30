using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    [Header("Limites dynamiques via murs")]
    public Collider2D wallLeft;
    public Collider2D wallRight;

    private float minX;
    private float maxX;
    private float camHalfWidth;

    void Start()
    {
        // Calcul de la moitié de la largeur de la caméra (en unités monde)
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        camHalfWidth = camWidth / 2f;

        // Déduction des limites en fonction des murs
        minX = wallLeft.bounds.max.x + camHalfWidth;
        maxX = wallRight.bounds.min.x - camHalfWidth;
    }

    void LateUpdate()
    {
        float targetX = player.position.x + offset.x;
        targetX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector3(targetX, transform.position.y, offset.z);
    }
}