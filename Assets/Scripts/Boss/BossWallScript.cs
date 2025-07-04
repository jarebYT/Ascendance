using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow; // Script qui gère la caméra

    public bool triggered = false;

    public GameObject wall_boss_room; // GameObject du mur à activer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("Boss room triggered");

            // Active le mur
            wall_boss_room.SetActive(true);

            // Accéder au collider pour les bounds
            Collider2D wallCollider = wall_boss_room.GetComponent<Collider2D>();
            cameraFollow.leftLimit = wallCollider.bounds.max.x + cameraFollow.camHalfWidth;
        }
    }
}
