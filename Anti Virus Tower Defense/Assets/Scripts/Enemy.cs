using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float speed = 5;
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<BoxCollider2D>().transform.position = gameObject.transform.position;
        if (health <= 0)
        {
            GameState.score += 100;
            GameState.currency += 25;
            GameObject.Find("EnemyManager").GetComponent<EnemyManager>().destroyEnemy();
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            this.health -= collision.gameObject.GetComponent<BasicBullet>().damage;
            Destroy(collision.gameObject);
        }
    }
}
