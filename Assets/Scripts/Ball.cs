using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(SpriteRenderer))]

public class Ball : MonoBehaviour
{
    public int level;

    public bool isDrop;
    public bool isMerge;

    private Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            rigidbody2D.position = new Vector2(mPosition.x, rigidbody2D.position.y);
        }
        else if (Input.GetMouseButtonUp(0) && !isDrop)
        {
            isDrop = true;
            rigidbody2D.gravityScale = 1;
        }
    }

    public void Setlevel()
    {
        level = Random.Range(0, 5);
        spriteRenderer.sprite = GameManager.Instance.ballDatas[level].ballImage;
        circleCollider2D.radius = GameManager.Instance.ballDatas[level].size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == this.gameObject.tag)
        {
            Ball other = collision.gameObject.GetComponent<Ball>();
            
            if(!other.isMerge && !this.isMerge && other.level == this.level)
            {
                float myX = rigidbody2D.position.x;
                float myY = rigidbody2D.position.y;
                float otherX = other.rigidbody2D.position.x;
                float otherY = other.rigidbody2D.position.y;
                
                if(myY < otherY || (myY == otherY && myX > otherX))
                {
                    Destroy(other.gameObject);
                    level++;
                    Setlevel();
                }
            }
        }
    }
}
