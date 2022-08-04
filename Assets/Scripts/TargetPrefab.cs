using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrefab : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameHandler gameHandler;
    public ParticleSystem explosionParticle;

    private float xSpawnRange = 16.0f;
    private float ySpawnPos = -2.5f;
    private float minSpeed = 17.0f;
    private float maxSpeed = 22.0f;
    private float maxTorque = 35.0f;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        targetRb = GetComponent<Rigidbody>();

        transform.position = RandomSpawnPosition();
        targetRb.AddForce(RandomUwardForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if (gameHandler.isGameActive)
        {
            Destroy(gameObject);
            gameHandler.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameHandler.GameOver();
            gameHandler.isGameActive = false;
        }
    }
    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
    Vector3 RandomUwardForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return maxTorque;
    }
}
