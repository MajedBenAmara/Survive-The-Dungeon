using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoor : MonoBehaviour
{
    private BoxCollider2D _collider;
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    public void CloseTheDoor()
    {
        _collider.isTrigger = false;
    }
    public void OpenTheDoor()
    {
        gameObject.SetActive(false);
    }
}
