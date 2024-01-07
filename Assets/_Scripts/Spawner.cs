using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[choice], this.transform.position, this.transform.rotation);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
