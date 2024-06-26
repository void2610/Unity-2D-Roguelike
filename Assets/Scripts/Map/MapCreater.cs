namespace NMap
{
    using UnityEngine;
    using System.Collections.Generic;

    public class MapCreater : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int mapSize;
        [SerializeField]
        private int enemyNum;
        [SerializeField]
        private List<GameObject> enemyPrefabs;

        void Start()
        {
            for (int i = 0; i < enemyNum; i++)
            {
                int x = Random.Range(0, mapSize.x);
                int y = Random.Range(0, mapSize.y);
                int enemyIndex = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], this.transform.position + new Vector3(x - mapSize.x / 2, y - mapSize.y / 2, 0), Quaternion.identity);
                enemy.transform.SetParent(this.transform);
            }
        }
    }
}