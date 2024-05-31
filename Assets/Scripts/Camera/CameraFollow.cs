using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(GameManager.Instance.PlayerTransform.gameObject != null)
        {
            transform.position = new Vector3(GameManager.Instance.PlayerTransform.position.x,
                GameManager.Instance.PlayerTransform.position.y, transform.position.z);
        }
    }
}
