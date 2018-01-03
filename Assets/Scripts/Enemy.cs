using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType
    {
        Basic, Fast, Strong, Shooter, Leaper, Swarmer, Summoner, Berserker, Cannoner
    }

    public GameObject Upgrade;

    public Material red;
    public Material orange;
    public Material yellow;
    public Material green;
    public Material blue;
    public Material purple;
    public Material pink;

    public GameObject shooterBolt;
    public GameObject basicEnemy;

    public EnemyType type = EnemyType.Basic;
    private GameObject player;
    private int difficulty;
    private bool attacking = false;
    private bool walledIn = false;
    private float lastAttackTime;
    private float lastSpecialTime;
    private float specialAttackSpeed = 1;
    private float specicalDuration = 0.5f;
    private Transform playerTransform;
    private float moveSpeed = 5;
    private float attackSpeed = 1;
    private float maxDistance = 1000;
    private float specialDistance = 100;
    private float minDistance = 0.5f;
    private int damage = 1;
    public float health = 1;


	// Use this for initialization
	void Start () {
        lastAttackTime = Time.time;
        player = GameObject.Find("Player");
        difficulty = player.GetComponent<PlayerLife>().difficulty;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        switch (type)
        {
            case EnemyType.Basic:
                moveSpeed = 5 + Random.Range(0.0f, 1.5f);
                attackSpeed = 1.2f - (difficulty / 5f);
                maxDistance = 1000;
                specialDistance = 100;
                minDistance = 0f;
                damage = 1;
                health = 1 + difficulty;
                this.GetComponent<Renderer>().material = red;
                break;
            case EnemyType.Fast:
                moveSpeed = 5f + (difficulty * 2f) + Random.Range(0.0f, 1.0f);
                attackSpeed = 1f - (difficulty / 5f);
                maxDistance = 1000;
                specialDistance = 100;
                minDistance = 0f;
                damage = 1;
                health = difficulty;
                this.GetComponent<Renderer>().material = yellow;
                break;
            case EnemyType.Leaper:
                moveSpeed = 6;
                attackSpeed = 1f - (difficulty / 10f);
                maxDistance = 1000;
                specialDistance = 0;
                specialAttackSpeed = 4;
                specicalDuration = 0.5f;
                minDistance = 0f;
                damage = 1;
                health = 1 + difficulty;
                this.GetComponent<Renderer>().material = orange;
                break;
            case EnemyType.Shooter:
                moveSpeed = 5 + (difficulty * 1.5f) + Random.Range(0.0f, 0.5f);
                attackSpeed = 1;
                maxDistance = 1000;
                specialDistance = 3 + (difficulty * 2f);
                minDistance = 4f + (difficulty / 2f);
                damage = 1;
                health = 2;
                this.GetComponent<Renderer>().material = purple;
                break;
            case EnemyType.Strong:
                moveSpeed = 4 + Random.Range(0.0f, 1.0f);
                attackSpeed = 1.2f;
                maxDistance = 1000;
                specialDistance = 100;
                minDistance = 0f;
                damage = 2 + (difficulty * 2);
                health = 2 + difficulty;
                this.GetComponent<Renderer>().material = pink;
                break;
            case EnemyType.Swarmer:
                moveSpeed = 6;
                attackSpeed = 1.0f;
                maxDistance = 1000;
                specialDistance = 100;
                minDistance = 0f;
                damage = 1;
                health = 0.5f;
                this.GetComponent<Renderer>().material = green;
                break;
            case EnemyType.Summoner:
                moveSpeed = 8;
                attackSpeed = 5f;
                maxDistance = 1000;
                specialDistance = 0f;
                damage = 1;
                health = 10f;
                specialAttackSpeed = 1.5f;
                this.GetComponent<Renderer>().material = blue;
                break;
            case EnemyType.Berserker:
                moveSpeed = 9;
                attackSpeed = 0.25f;
                maxDistance = 1000;
                specialDistance = 0f;
                damage = 2;
                health = 10f;
                this.GetComponent<Renderer>().material = blue;
                break;
            case EnemyType.Cannoner:
                moveSpeed = 6;
                attackSpeed = 0.5f;
                maxDistance = 1000;
                specialDistance = 0f;
                minDistance = 6f;
                damage = 1;
                health = 10f;
                this.GetComponent<Renderer>().material = blue;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            if (Random.Range(0, 10) < 1)
            {
                Instantiate(Upgrade, this.transform.position, this.transform.rotation);
            }

            switch (type)
            {
                case EnemyType.Basic:
                    player.GetComponent<PlayerLife>().score += 5;
                    break;
                case EnemyType.Fast:
                    player.GetComponent<PlayerLife>().score += 10;
                    break;
                case EnemyType.Leaper:
                    player.GetComponent<PlayerLife>().score += 10;
                    break;
                case EnemyType.Shooter:
                    player.GetComponent<PlayerLife>().score += 10;
                    break;
                case EnemyType.Strong:
                    player.GetComponent<PlayerLife>().score += 10;
                    break;
                case EnemyType.Swarmer:
                    player.GetComponent<PlayerLife>().score += 1;
                    break;
                case EnemyType.Summoner:
                    player.GetComponent<PlayerLife>().score += 500;
                    break;
                case EnemyType.Berserker:
                    player.GetComponent<PlayerLife>().score += 500;
                    break;
                case EnemyType.Cannoner:
                    player.GetComponent<PlayerLife>().score += 500;
                    break;
            }

            Destroy(this.gameObject);
        }

        if(lastAttackTime + attackSpeed < Time.time && attacking)
        {
            player.GetComponent<PlayerLife>().health -= damage;
            lastAttackTime = Time.time;
        }

        //transform.LookAt(player);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerTransform.position.y-transform.position.y, playerTransform.position.x - transform.position.x));

        if (walledIn)
        {
            if (Mathf.Abs(this.transform.position.x - playerTransform.position.x)>=15|| Mathf.Abs(this.transform.position.y - playerTransform.position.y) >= 10)
            {
                transform.position += (new Vector3((playerTransform.position.x - transform.position.x) / Vector2.Distance(transform.position, playerTransform.position), (playerTransform.position.y - transform.position.y) / Vector2.Distance(transform.position, playerTransform.position), 0));
            }
        }

        if (type == EnemyType.Leaper)
        {
            if(lastSpecialTime + specicalDuration > Time.time)
            {
                moveSpeed = 14;
            }
            else
            {
                moveSpeed = 6;
            }
        }

        if(Vector2.Distance(transform.position, playerTransform.position) >= specialDistance)
        {
            switch (type)
            {
                case EnemyType.Leaper:
                    if(lastSpecialTime + specialAttackSpeed < Time.time)
                    {
                        lastSpecialTime = Time.time;
                    }
                    break;
                case EnemyType.Shooter:
                    if(lastAttackTime + attackSpeed < Time.time)
                    {
                        GameObject shot = Instantiate(shooterBolt, transform.position, transform.rotation);
                        shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
                        lastAttackTime = Time.time;
                    }
                    break;
                case EnemyType.Cannoner:
                    if (lastAttackTime + attackSpeed < Time.time)
                    {
                        GameObject shot = Instantiate(shooterBolt, transform.position, transform.rotation);
                        shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(750, 0));
                        lastAttackTime = Time.time;
                    }
                    break;
                case EnemyType.Summoner:
                    if(lastSpecialTime + specialAttackSpeed < Time.time)
                    {
                        Instantiate(basicEnemy, this.transform.position + new Vector3(0, 1.5f, 0), this.transform.rotation);
                        lastSpecialTime = Time.time;
                    }
                    break;
            }
        }

        if(Vector2.Distance(transform.position, playerTransform.position) > minDistance)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector3((playerTransform.position.x - transform.position.x) / Vector2.Distance(transform.position, playerTransform.position), (playerTransform.position.y - transform.position.y) / Vector2.Distance(transform.position, playerTransform.position), 0)) * moveSpeed;
            if(Vector2.Distance(transform.position, playerTransform.position) >= maxDistance)
            {
                Destroy(this.gameObject);
            }
        }
        else if(Vector2.Distance(transform.position, playerTransform.position) <= minDistance/2)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector3((playerTransform.position.x - transform.position.x) / Vector2.Distance(transform.position, playerTransform.position), (playerTransform.position.y - transform.position.y) / Vector2.Distance(transform.position, playerTransform.position), 0)) * moveSpeed * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bolt")
        {
            health -= collision.gameObject.GetComponent<BoltController>().damage;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            attacking = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            walledIn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            attacking = false;
        }
        if(collision.gameObject.tag == "Wall")
        {
            walledIn = false;
        }
    }
}
