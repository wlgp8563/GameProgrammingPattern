using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;     //적 리스폰 시간 간격
    public int numberOfEnemies = 2;      //한 번에 리스폰 될 적 수

    private Coroutine spawnCoroutine;
    private List<Vector3> spawnedPositions = new List<Vector3>(); // 스폰될 적의 위치 겹치치 않도록 스폰된 적의 위치 저장

    private void Start()
    {
        // 시작 시 스폰 시작
        StartSpawning();
    }

    private void OnDestroy()
    {
        // 스폰 중이었다면 정지
        StopSpawning();
    }

    public void StartSpawning()
    {
        // 스폰 코루틴 시작
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        // 스폰 코루틴 정지
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
            // numberOfEnemies만큼 적 스폰
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        //겹치지 않는 위치를 찾을 때까지 반복
        Vector3 spawnPosition;
        do
        {
            // x, z 좌표를 -10부터 10 사이의 랜덤 값으로 설정
            float x = Random.Range(-9.5f, 9.5f);
            float z = Random.Range(-9.5f, 9.5f);
            spawnPosition = new Vector3(x, 0.3f, z);
        } while (IsPositionOverlapping(spawnPosition));

        // Enemy Prefab을 스폰 위치에 생성
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 10초 뒤에 Enemy 제거
        //Destroy(enemy, 10f);

        // 스폰된 위치 저장
        spawnedPositions.Add(spawnPosition);
    }

    private bool IsPositionOverlapping(Vector3 position)
    {
        // 스폰된 위치들과의 겹침 여부 확인
        for (int i = 0; i < spawnedPositions.Count; i++)
        {
            if (Vector3.Distance(position, spawnedPositions[i]) < 1f)
            {
                // 겹치는 위치가 있으면 true 반환
                return true;
            }
        }
        // 겹치는 위치가 없으면 false 반환
        return false;
    }
}
