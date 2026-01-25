using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float chunkHeight = 10f;
    [SerializeField] float totalChunks = 0f;
    [SerializeField] Transform cameraTransform;
    private float heightY = 0f;
    void Start()
    {
        if (transform.position.y > heightY)
        {
            heightY = transform.position.y;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + chunkHeight < cameraTransform.position.y)
        {
            Debug.Log("Move Wall Up");
            MoveOnTop();
        }
    }
    void MoveOnTop()
    {
        heightY = heightY + chunkHeight * totalChunks;
        transform.position = new Vector3(transform.position.x, heightY,transform.position.z);
    }

}
