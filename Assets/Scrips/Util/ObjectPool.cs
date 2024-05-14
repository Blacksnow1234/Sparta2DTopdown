using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;           // 이 풀의 식별 태그
        public GameObject prefab;    // 풀에서 생성할 프리팹
        public int size;             // 풀에서 관리할 객체의 초기 크기
    }

    public List<Pool> pools = new List<Pool>();    // 다양한 종류의 풀을 관리하기 위한 리스트
    public Dictionary<string, Queue<GameObject>> PoolDictionary;  // 식별 태그를 통해 풀에 접근하기 위한 사전

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();  // 사전 초기화

        // 모든 풀에 대해 반복하여 초기화
        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();  // 풀에 넣을 큐 생성
            // 초기 크기만큼 객체를 생성하여 풀에 추가
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);  // 프리팹을 이용해 객체 생성
                obj.SetActive(false);  // 비활성화 상태로 설정
                queue.Enqueue(obj);     // 큐에 객체 추가
            }

            PoolDictionary.Add(pool.tag, queue);  // 사전에 태그와 큐를 추가하여 풀 초기화
        }
    }

    // 풀에서 객체를 가져오는 함수
    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))   // 태그가 사전에 없는 경우
        {
            return null;  // null 반환
        }

        GameObject obj = PoolDictionary[tag].Dequeue();  // 해당 태그의 풀에서 객체를 가져옴
        PoolDictionary[tag].Enqueue(obj);  // 가져온 객체를 다시 풀에 넣음

        obj.SetActive(true); // 가져온 객체를 활성화
        return obj;  // 가져온 객체 반환
    }
}
