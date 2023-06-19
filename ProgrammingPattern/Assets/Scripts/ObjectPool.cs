using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 15;
    private float snowBallSpeed = 10f;

    [SerializeField] private GameObject snowBallPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(snowBallPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void InitializeSnowBall(GameObject snowBall)
    {
        SnowBall snowballScript = snowBall.GetComponent<SnowBall>();
        if(snowballScript != null)
        {
            snowballScript.speed = snowBallSpeed;
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
