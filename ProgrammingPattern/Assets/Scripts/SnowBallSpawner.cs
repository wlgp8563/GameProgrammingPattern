using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SD;

public class SnowBallSpawner : MonoBehaviour
{

    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private Transform snowBallPos;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    private void Fire()
    {
        GameObject snowball = ObjectPool.instance.GetPooledObject();

        if(snowball != null)
        {
            snowball.transform.position = snowBallPos.position;
            snowball.SetActive(true);

            Vector3 fireDirection = transform.forward;
            snowball.GetComponent<Rigidbody>().velocity = fireDirection * snowball.GetComponent<SnowBall>().speed;
            if(playerController != null)
            {
                playerController.StopRotation();
                //Vector3 playerForward = playerController.transform.forward;
                //snowball.GetComponent<SnowBall>().SetDirection(playerForward);
            }
        }
    }

    private void FixedUpdate()
    {
        if(playerController != null)
        {
            playerController.StopRotation();
        }
    }
}
