using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[choice], this.transform.position, this.transform.rotation);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
    }
}
