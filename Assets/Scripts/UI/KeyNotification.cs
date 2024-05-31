using UnityEngine;

public class KeyNotification : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void AppearKeyNotification()
    {
        _anim.Play("appearing_anim");
    }
}
