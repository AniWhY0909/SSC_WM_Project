using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int level;

    public bool isDrop;
    public bool isFall;
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
        isFall = false;
        isMerge = false;

        Setlevel();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isDrop && !isFall)
        {
            Vector2 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rightBorder = 8.5f - circleCollider2D.radius;
            float leftBorder = -8.5f + circleCollider2D.radius;

            if (mPosition.x >= rightBorder) mPosition.x = rightBorder;
            if (mPosition.x <= leftBorder) mPosition.x = leftBorder;

            transform.position = new Vector2(mPosition.x, transform.position.y);

        }
        else if (Input.GetMouseButtonUp(0) && !isDrop)
        {
            rb.gravityScale = 1;
            isFall = true;
        }
    }

    public void Setlevel()
    {
        isMerge = false;
        if (!isDrop)
        {
            level = GameManager.Instance.currentBallLevel;
            GameManager.Instance.nextBallLevel = Random.Range(0, 5);
            GameManager.Instance.UpdateNextBall(GameManager.Instance.nextBallLevel);
        }

        spriteRenderer.sprite = GameManager.Instance.ballDatas[level].ballImage;
        if (level == 2 || level == 5)
        {
            gameObject.AddComponent<PolygonCollider2D>();
            circleCollider2D.isTrigger = true;
        }
        else
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            circleCollider2D.isTrigger = false;
        }
        circleCollider2D.radius = GameManager.Instance.ballDatas[level].size;
    }

    public void Merge(Vector2 position)
    {
        GameManager.Instance.UpdateScore(level);
        this.gameObject.transform.position = position;  
        Setlevel();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isDrop = true;
        isFall = false;
        this.gameObject.tag = "Drop";
        //Debug.Log("ball collision detect");

        if (collision.collider.gameObject.layer == this.gameObject.layer && level < 10)
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
                    Merge(new Vector2((myX + otherX) / 2, (myY + otherY) / 2));
                }
            }
        }


    }
}
