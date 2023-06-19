using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float speed = 10f; // 총알의 속도

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // 총알이 활성화될 때마다 초기화
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        // 총알을 앞으로 이동시킴
        rb.velocity = transform.forward * speed;
    }

    private void OnDisable()
    {
        // 총알이 비활성화될 때 리셋
        rb.velocity = Vector3.zero;
    }

    /*public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }*/


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
