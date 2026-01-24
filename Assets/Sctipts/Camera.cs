using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(0, player.position.y, 0);
    }
}
    
