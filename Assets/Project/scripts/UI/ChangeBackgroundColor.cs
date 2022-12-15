using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Color _backgroundColor;
    
    void OnEnable()
    {
        _camera.backgroundColor = _backgroundColor;
    }
}
