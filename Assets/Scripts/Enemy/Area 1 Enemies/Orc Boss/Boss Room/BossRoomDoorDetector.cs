using UnityEngine;

public class BossRoomDoorDetector : MonoBehaviour
{
    [SerializeField]
    private BossRoomManager doormanager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doormanager.playerEntered = true;
        }
    }

}
