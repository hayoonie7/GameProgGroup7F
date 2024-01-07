using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 5;
    public AudioClip healthSFX1;
    public AudioClip healthSFX2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver) return;
        transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        if (transform.position.y < Random.Range(1.0f, 3.0f)) {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (LevelManager.isGameOver) return;
        if (other.CompareTag("Player")) {
            gameObject.SetActive(false);

            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeHealth(healthAmount);
            
            AudioSource.PlayClipAtPoint(healthSFX1, this.transform.position);
            AudioSource.PlayClipAtPoint(healthSFX2, this.transform.position);

            Destroy(gameObject, 0.5f);
        }
    }
}
