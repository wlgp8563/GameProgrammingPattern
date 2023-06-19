using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject GoldPrefab;
    public float goldInterval = 8f;     //��� ������ �ð� ����
    public int numberOfGolds = 3;      //�� ���� ������ �� ��� ��

    private Coroutine goldsCoroutine;
    private List<Vector3> goldPositions = new List<Vector3>(); // ������ ������ ��ġ ��ġġ �ʵ��� ������ ���� ��ġ ����

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
        goldsCoroutine = StartCoroutine(SpawnGolds());
    }

    public void StopSpawning()
    {
        // ���� �ڷ�ƾ ����
        if (goldsCoroutine != null)
        {
            StopCoroutine(goldsCoroutine);
            goldsCoroutine = null;
        }
    }

    private IEnumerator SpawnGolds()
    {
        while (true)
        {
            // numberOfGolds��ŭ  ��彺��
            for (int i = 0; i < numberOfGolds; i++)
            {
                SpawnGold();
            }

            yield return new WaitForSeconds(goldInterval);
        }
    }

    private void SpawnGold()
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
        GameObject gold = Instantiate(GoldPrefab, spawnPosition, Quaternion.identity);

        // 10�� �ڿ� gold ����
        Destroy(gold, 5f);

        // ������ ��ġ ����
        goldPositions.Add(spawnPosition);
    }

    private bool IsPositionOverlapping(Vector3 position)
    {
        // ������ ��ġ����� ��ħ ���� Ȯ��
        for (int i = 0; i < goldPositions.Count; i++)
        {
            if (Vector3.Distance(position, goldPositions[i]) < 1f)
            {
                // ��ġ�� ��ġ�� ������ true ��ȯ
                return true;
            }
        }
        // ��ġ�� ��ġ�� ������ false ��ȯ
        return false;
    }
}
