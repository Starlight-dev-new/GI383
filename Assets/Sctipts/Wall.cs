using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float chunkHeight = 12f;
    [SerializeField] float totalChunks = 0f;
    [SerializeField] Transform player;
    
    private float heightY = 0f;
    void Start()
    {
        if (transform.position.y > heightY)
        {
            heightY = transform.position.y;
        }

    }
    void Update()
    {
        if (transform.position.y + chunkHeight < player.position.y)
        {
            MoveOnTop();
        }
    }

    void MoveOnTop()
    {
        heightY = heightY + (chunkHeight * totalChunks);
        transform.position = new Vector3(transform.position.x, heightY,transform.position.z);
    }

}
