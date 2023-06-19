using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float attackDistance = 1.5f;
    public float holdDistance = 6.0f;
    public float walkDistance = 6.0f;
    public float timeToDestroy = 10f;
    public Animator anim;

    //private bool isHit = false;
    private Transform playerTransform;
    // private Rigidbody rigid;
    private NavMeshAgent agent;
    private bool isDead = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private void Update()
    {
        if (isDead)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer > holdDistance)
        {
            anim.SetBool("Iswalking", false);
            agent.isStopped = true;
        }
        else if(distanceToPlayer <= walkDistance)
        {
            anim.SetBool("Iswalking", true);
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
        }

        if(distanceToPlayer <= attackDistance)
        {
            anim.SetTrigger("IsClose");
            //anim.SetBool("Iswalking", true);
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            isDead = true;
            anim.SetTrigger("IsDead");
            Destroy(gameObject, 0.5f);
        }
    }
}
