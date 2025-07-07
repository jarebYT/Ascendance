using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow;

    public bool triggered = false;

    public GameObject wall_boss_room;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("Boss room triggered");

            wall_boss_room.SetActive(true);

            // Limites de cam
            Collider2D wallCollider = wall_boss_room.GetComponent<Collider2D>();
            cameraFollow.leftLimit = wallCollider.bounds.max.x + cameraFollow.camHalfWidth;
        }
    }
}
