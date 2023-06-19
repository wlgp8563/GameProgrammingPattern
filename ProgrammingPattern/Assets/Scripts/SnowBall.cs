using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float speed = 10f; // �Ѿ��� �ӵ�

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // �Ѿ��� Ȱ��ȭ�� ������ �ʱ�ȭ
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        // �Ѿ��� ������ �̵���Ŵ
        rb.velocity = transform.forward * speed;
    }

    private void OnDisable()
    {
        // �Ѿ��� ��Ȱ��ȭ�� �� ����
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
