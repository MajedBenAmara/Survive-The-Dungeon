using UnityEngine;

public class Weapon : MonoBehaviour
{

    #region Variabales

    internal Animator Anim;

    #endregion

    #region Unity Func

    protected void Start()
    {
        Anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (GetAttackAnimationCondition(.95f))
        {
            Anim.Play("Idle");
            //transform.rotation = Quaternion.identity;
        }
    }

    #endregion

    #region Built Func

    public bool GetAttackAnimationCondition(float time)
    {
        return Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time;
    }

    public bool GetIdleAnimationCondition(float time)
    {
        return Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time;
    }

    protected void GetMouseDirection(Transform rotationOrigin, Transform bodyToRotate)
    {

        Vector3 mousePosition;
        Vector3 shootDirection;
        float angle;
        Quaternion rotation;

        // getting the mouse coor in world space coordiante system
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculating the direction from the weapon holder to the mouse
        shootDirection = (mousePosition - rotationOrigin.position).normalized;
        // calculating the produced angle by the shooting direction vector annd transforming into into degree
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        // Creating a rotation using the angle that we previously made;
        // Creating a rotation using the angle that we previously made
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // We rotate the wheapon holder using that rotation
        bodyToRotate.rotation = rotation;
    }

    public float GetMouseAngle(Transform rotationOrigin)
    {

        Vector3 mousePosition;
        Vector3 shootDirection;
        float angle;

        // getting the mouse coor in world space coordiante system
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculating the direction from the weapon holder to the mouse
        shootDirection = (mousePosition - rotationOrigin.position).normalized;
        // calculating the produced angle by the shooting direction vector annd transforming into into degree
        return angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

    }


    #endregion

}

