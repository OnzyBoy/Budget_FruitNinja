using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> targetPrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private AudioSource spawnSource;
    public AudioClip spawnClipSound;

    public bool isGameActive = true;
    private int score;
    public float timeLeft;
    private float spawnRate = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefab.Count);

            Instantiate(targetPrefab[index]);
            spawnSource.PlayOneShot(spawnClipSound, 1.0f);
        }
    }

    public void UpdateScore(int scoretoadd)
    {
        score += scoretoadd;
        scoreText.text = "Score: " + score;
    }

    public void CountDownTimer()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Round(timeLeft);
    }
    public void StartGame(int difficulty)
    {
        score = 0;
        spawnRate /= difficulty;
        timeLeft = 60;
        isGameActive = true;

        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        titleScreen.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
        
}
