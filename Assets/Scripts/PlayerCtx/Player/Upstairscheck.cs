using UnityEngine;

public class UpStairsCheck : MonoBehaviour
{
    public float upwardRaycastDistance;
    public float downwardRaycastDistance;
    public BoxCollider2D playerBoxCollider;
    public LayerMask groundLayerMask;

    void Start()
    {
        playerBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //上方射线检测
        RaycastHit2D upwardHit = Physics2D.Raycast(transform.position, Vector2.up, upwardRaycastDistance, groundLayerMask);
        if (upwardHit.collider != null)
        {
            Debug.Log("collision above");
            if (upwardHit.collider.gameObject.tag == "Platform")
            {
                Debug.Log("platform above");
                BoxCollider2D platformCollider = upwardHit.collider.GetComponent<BoxCollider2D>();
                if (platformCollider != null)
                {
                    platformCollider.enabled = false;
                }
            }
        }

        //下方射线检测
        RaycastHit2D downwardHit = Physics2D.Raycast(transform.position, Vector2.down, downwardRaycastDistance, groundLayerMask);
        if (downwardHit.collider != null)
        {
            Debug.Log("collision below");
            if (downwardHit.collider.gameObject.tag == "Platform")
            {
                Debug.Log("already upstairs");
                BoxCollider2D platformCollider = downwardHit.collider.GetComponent<BoxCollider2D>();
                if (platformCollider != null)
                {
                    Debug.Log("platform below");
                    float playerY = transform.position.y;
                    float platformY = downwardHit.collider.transform.position.y;
                    if (playerY > platformY)
                    {
                        Debug.Log("higher than platform");
                        platformCollider.enabled = true;
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * upwardRaycastDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * downwardRaycastDistance);
    }
}