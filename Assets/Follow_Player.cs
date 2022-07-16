using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;

    }
}
