using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsIconManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireIconPrefab;

    [SerializeField]
    private GameObject _thunderIconPrefab;

    [SerializeField]
    private GameObject _earthIconPrefab;

    public void ActivateFireIcon(bool value)
    {
        _fireIconPrefab.SetActive(value);
    }
    public void ActivateEarthIcon(bool value)
    {
        _earthIconPrefab.SetActive(value);
    }
    public void ActivateThunderIcon(bool value)
    {
        _thunderIconPrefab.SetActive(value);
    }
}
