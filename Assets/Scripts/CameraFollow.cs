using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        // La caméra suit uniquement la position X du joueur, tout en conservant sa propre position Y
        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, offset.z);
    }
}
