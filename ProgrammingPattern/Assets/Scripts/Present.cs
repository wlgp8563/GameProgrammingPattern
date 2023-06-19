using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    PresentMessage presentMessage;

    private void Start()
    {
        presentMessage = PresentMessage.Instance;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            presentMessage.OnPresentCollision();
            Destroy(gameObject);
        }
    }
}
