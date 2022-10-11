using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    Vector2 posOffset;

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float topLimit;
    [SerializeField]
    float bottomLimit;

    private Vector3 velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        Vector3 startPos = transform.position;

        Vector3 endPos = player.transform.position;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;*/
        
        
        
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        ); 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //top
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        //right
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        //bottom
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        //left
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}

