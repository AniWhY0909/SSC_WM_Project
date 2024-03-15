using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(SpriteRenderer))]

public class Ball : MonoBehaviour
{
    public int level;

    public bool isDrop;
    public bool isMerge;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        isDrop = false;
        isMerge = false;

        Setlevel();      
    }

    private void Update()
    { 
        if (Input.GetMouseButton(0) && !isDrop)
        {
            Vector2 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.position = new Vector2(mPosition.x, rb.position.y);
        }
        else if (Input.GetMouseButtonUp(0) && !isDrop)
        {
            rb.gravityScale = 1;
        }
    }

    public void Setlevel()
    {
        isMerge = false;
        if (!isDrop) level = Random.Range(0, 4);

        if (level < 4)
        {
            spriteRenderer.sprite = GameManager.Instance.ballDatas[level].ballImage;
            circleCollider2D.radius = GameManager.Instance.ballDatas[level].size;
        }

        if(level >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDrop = true;
        if (collision.collider.tag == this.gameObject.tag)
        {
            Ball other = collision.gameObject.GetComponent<Ball>();

            if (!other.isMerge && !this.isMerge && other.level == this.level)
            {
                float myX = rb.position.x;
                float myY = rb.position.y;
                float otherX = other.rb.position.x;
                float otherY = other.rb.position.y;

                if (myY < otherY || (myY == otherY && myX > otherX))
                {

                    Destroy(other.gameObject);
                    level++;
                    isMerge = true;
                    Setlevel();
                }
            }
        }
    }
}
