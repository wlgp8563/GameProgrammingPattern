using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test1 : MonoBehaviour
{
    private UnityAction someListener;

    void Awake()
    {
        someListener = new UnityAction(SomeFunction);
    }
    void OnEnable()
    {
        EventManager.StartListening("test1", someListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("test1", someListener);
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }
}
