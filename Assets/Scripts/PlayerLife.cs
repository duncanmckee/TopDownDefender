using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {

    public int spawnRange = 29;
    private bool readyRound = true;
    private float spawningTimer = 0;
    public int round = 0;
    public int score = 0;
    private int remaining = 0;
    public int difficulty = 1;
    public int health;

    public GameObject basic;
    public GameObject fast;
    public GameObject leaper;
    public GameObject shooter;
    public GameObject strong;
    public GameObject swarmer;
    public GameObject summoner;
    public GameObject berserker;
    public GameObject cannoner;


    public GameObject wall;
    private GameObject tempWall;

    private Text healtBox;
    private Text roundBox;
    private Text scoreBox;

	// Use this for initialization
	void Start () {
        health = 5;
        healtBox = GameObject.Find("HealthText").GetComponent<Text>();
        scoreBox = GameObject.Find("ScoreText").GetComponent<Text>();
        roundBox = GameObject.Find("RoundText").GetComponent<Text>();
        Instantiate(basic, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        for(int i = 0; i < 80; i++)
        {
            if(i% 4 ==1 || i % 4 == 2)
            {
                tempWall = Instantiate(wall, new Vector3((int)Random.Range(-spawnRange, spawnRange), (int)Random.Range(5f, spawnRange) * ((((int)Random.Range(0, 2)) * 2) - 1), 0), this.transform.rotation);
            }
            else
            {
                tempWall = Instantiate(wall, new Vector3((int)Random.Range(5f, spawnRange) * ((((int)Random.Range(0, 2)) * 2) - 1), (int)Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            if (i % 2 == 0)
            {
                tempWall.transform.localScale = new Vector3(0.9f, (((int)Random.Range(1.0f, 4.0f)) * 2) - 1.1f);
            }
            else
            {
                tempWall.transform.localScale = new Vector3((((int)Random.Range(1.0f, 4.0f)) * 2) - 1.1f, 0.9f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        scoreBox.text = "Score: " + score;
        healtBox.text = "Health: " + health;
        remaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        roundBox.text = "Round " + round + ": " + remaining;
        if (health <= 0)
        {
            Debug.Log("Dead");
            int highScore = SaveLoad.Load();
            if (score > highScore)
            {
                SaveLoad.Save(score);
            }
            SceneManager.LoadScene("Menu");
        }

        if (remaining == 0 && readyRound)
        {
            round++;
            readyRound = false;
            spawningTimer = Time.time;
        }
        if (spawningTimer + 2.5f < Time.time && !readyRound)
        {
            readyRound = true;
            int roundMult = (int)Mathf.Floor(round / 20) + 1;
            switch (round%20)
            {
                case 2:
                    SpawnBasic(3 * roundMult);
                    break;
                case 3:
                    SpawnBasic(2 * roundMult);
                    SpawnFast(2 * roundMult);
                    break;
                case 4:
                    SpawnBasic(4 * roundMult);
                    SpawnLeaper(1 * roundMult);
                    break;
                case 5:
                    SpawnBasic(4 * roundMult);
                    SpawnFast(2 * roundMult);
                    break;
                case 6:
                    SpawnBasic(4 * roundMult);
                    SpawnFast(2 * roundMult);
                    SpawnLeaper(1 * roundMult);
                    break;
                case 7:
                    SpawnBasic(4 * roundMult);
                    SpawnFast(1 * roundMult);
                    SpawnLeaper(1 * roundMult);
                    SpawnStrong(2 * roundMult);
                    break;
                case 8:
                    SpawnBasic(5 * roundMult);
                    SpawnFast(2 * roundMult);
                    SpawnStrong(2 * roundMult);
                    SpawnShooter(2 * roundMult);
                    break;
                case 9:
                    SpawnBasic(5 * roundMult);
                    SpawnFast(3 * roundMult);
                    SpawnShooter(2 * roundMult);
                    break;
                case 10:
                    SpawnSwarm(20 * roundMult);
                    break;
                case 11:
                    SpawnBasic(10 * roundMult);
                    SpawnLeaper(2 * roundMult);
                    break;
                case 12:
                    SpawnStrong(5 * roundMult);
                    SpawnShooter(5 * roundMult);
                    SpawnSwarm(6 * roundMult);
                    break;
                case 13:
                    SpawnFast(8 * roundMult);
                    SpawnLeaper(6 * roundMult);
                    break;
                case 14:
                    SpawnBasic(10 * roundMult);
                    SpawnStrong(2 * roundMult);
                    SpawnShooter(2 * roundMult);
                    break;
                case 15:
                    SpawnLeaper(4 * roundMult);
                    SpawnSwarm(15 * roundMult);
                    break;
                case 16:
                    SpawnBasic(8 * roundMult);
                    SpawnFast(3 * roundMult);
                    SpawnStrong(6 * roundMult);
                    break;
                case 17:
                    SpawnBasic(10 * roundMult);
                    SpawnFast(2 * roundMult);
                    SpawnSwarm(8 * roundMult);
                    break;
                case 18:
                    SpawnBasic(10 * roundMult);
                    SpawnLeaper(3 * roundMult);
                    SpawnStrong(6 * roundMult);
                    break;
                case 19:
                    SpawnBasic(10 * roundMult);
                    SpawnFast(2 * roundMult);
                    SpawnLeaper(2 * roundMult);
                    SpawnShooter(2 * roundMult);
                    SpawnStrong(2 * roundMult);
                    SpawnSwarm(2 * roundMult);
                    break;
                case 0:
                    health += roundMult;
                    SpawnBoss(roundMult);
                    break;
                case 1:
                    SpawnBasic(1);
                    break;
            }
        }
    }

    private void SpawnBoss(int megaRound)
    {
        if (megaRound % 5 == 1)
        {
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(summoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(1f, 0.729411765f, 0.729411765f);
        }
        else if(megaRound % 5 == 2)
        {
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(berserker, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.729411765f, 1f, 0.729411765f);
        }
        else if (megaRound % 5 == 3)
        {
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(cannoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.729411765f, 0.729411765f, 1f);
        }
        else if (megaRound % 5 == 4)
        {
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(summoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(cannoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(1f, 0.729411765f, 1f);
        }
        else if (megaRound % 5 == 0)
        {
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(summoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(berserker, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            for (int i = 0; i < ((megaRound / 5) + 1); i++)
            {
                Instantiate(cannoner, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
            }
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.729411765f, 0.729411765f, 0.729411765f);
        }
    }

    private void SpawnBasic(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(basic, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        }
    }

    private void SpawnFast(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(fast, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        }
    }

    private void SpawnLeaper(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(leaper, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        }
    }

    private void SpawnShooter(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(shooter, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        }
    }

    private void SpawnStrong(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(strong, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), this.transform.rotation);
        }
    }

    private void SpawnSwarm(int count)
    {
        float xPos = Random.Range(-spawnRange, spawnRange - count);
        float yPos = Random.Range(-spawnRange, spawnRange);
        for (int i = 0; i < count; i++)
        {
            Instantiate(swarmer, new Vector3(xPos + i, yPos, 0), this.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBolt")
        {
            health -= (int)collision.gameObject.GetComponent<BoltController>().damage;
            Destroy(collision.gameObject);
        }
    }
}
