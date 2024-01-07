using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (LevelManager.isGameOver) return;
        if (collision.gameObject.CompareTag("CloseWall"))
        {
            Destroy(this.gameObject);
        }
    }
}
