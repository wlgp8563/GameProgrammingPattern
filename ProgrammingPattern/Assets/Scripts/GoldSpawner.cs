using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject GoldPrefab;
    public float goldInterval = 8f;     //골드 리스폰 시간 간격
    public int numberOfGolds = 3;      //한 번에 리스폰 될 골드 수

    private Coroutine goldsCoroutine;
    private List<Vector3> goldPositions = new List<Vector3>(); // 스폰될 선물의 위치 겹치치 않도록 스폰된 적의 위치 저장

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
        goldsCoroutine = StartCoroutine(SpawnGolds());
    }

    public void StopSpawning()
    {
        // 스폰 코루틴 정지
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
            // numberOfGolds만큼  골드스폰
            for (int i = 0; i < numberOfGolds; i++)
            {
                SpawnGold();
            }

            yield return new WaitForSeconds(goldInterval);
        }
    }

    private void SpawnGold()
    {
        //겹치지 않는 위치를 찾을 때까지 반복
        Vector3 spawnPosition;
        do
        {
            // x, z 좌표를 -9.5부터 9.5 사이의 랜덤 값으로 설정
            float x = Random.Range(-9.5f, 9.5f);
            float z = Random.Range(-9.5f, 9.5f);
            spawnPosition = new Vector3(x, 0.3f, z);
        } while (IsPositionOverlapping(spawnPosition));

        // Gold Prefab을 스폰 위치에 생성
        GameObject gold = Instantiate(GoldPrefab, spawnPosition, Quaternion.identity);

        // 5초 뒤에 gold 제거
        Destroy(gold, 5f);

        // 스폰된 위치 저장
        goldPositions.Add(spawnPosition);
    }

    private bool IsPositionOverlapping(Vector3 position)
    {
        // 스폰된 위치들과의 겹침 여부 확인
        for (int i = 0; i < goldPositions.Count; i++)
        {
            if (Vector3.Distance(position, goldPositions[i]) < 1f)
            {
                // 겹치는 위치가 있으면 true 반환
                return true;
            }
        }
        // 겹치는 위치가 없으면 false 반환
        return false;
    }
}
