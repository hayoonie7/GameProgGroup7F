using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingRoom : MonoBehaviour
{
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var levelManager = FindObjectOfType<LevelManager>();
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            levelManager.LevelBeat();
        }
        levelManager.SetButtonPromptActive(this.inRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ready");
            inRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
