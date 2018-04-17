using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

    public float speed = 2.0f;
    public int damage = 25;
    private Vector3 direction;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
