using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;     //�� ������ �ð� ����
    public int numberOfEnemies = 2;      //�� ���� ������ �� �� ��

    private Coroutine spawnCoroutine;
    private List<Vector3> spawnedPositions = new List<Vector3>(); // ������ ���� ��ġ ��ġġ �ʵ��� ������ ���� ��ġ ����

    private void Start()
    {
        // ���� �� ���� ����
        StartSpawning();
    }

    private void OnDestroy()
    {
        // ���� ���̾��ٸ� ����
        StopSpawning();
    }

    public void StartSpawning()
    {
        // ���� �ڷ�ƾ ����
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        // ���� �ڷ�ƾ ����
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // numberOfEnemies��ŭ �� ����
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        //��ġ�� �ʴ� ��ġ�� ã�� ������ �ݺ�
        Vector3 spawnPosition;
        do
        {
            // x, z ��ǥ�� -10���� 10 ������ ���� ������ ����
            float x = Random.Range(-9.5f, 9.5f);
            float z = Random.Range(-9.5f, 9.5f);
            spawnPosition = new Vector3(x, 0.3f, z);
        } while (IsPositionOverlapping(spawnPosition));

        // Enemy Prefab�� ���� ��ġ�� ����
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 10�� �ڿ� Enemy ����
        //Destroy(enemy, 10f);

        // ������ ��ġ ����
        spawnedPositions.Add(spawnPosition);
    }

    private bool IsPositionOverlapping(Vector3 position)
    {
        // ������ ��ġ����� ��ħ ���� Ȯ��
        for (int i = 0; i < spawnedPositions.Count; i++)
        {
            if (Vector3.Distance(position, spawnedPositions[i]) < 1f)
            {
                // ��ġ�� ��ġ�� ������ true ��ȯ
                return true;
            }
        }
        // ��ġ�� ��ġ�� ������ false ��ȯ
        return false;
    }
}
