using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D;

public class Ball : MonoBehaviour
{
    SpriteRenderer renderer;
    public SpriteRenderer Renderer
    {
        get
        {
            if (renderer == null)
                renderer = GetComponent<SpriteRenderer>();
            return renderer;
        }
    }

    CircleCollider2D collider;
    public CircleCollider2D Collider
    {
        get
        {
            if (collider == null)
                collider = GetComponent<CircleCollider2D>();
            return collider;
        }
    }

    float Radius { get { return (Collider).radius/2; } }

    Rigidbody2D rigid;
    const string fruitPrefix = "fruit_";
    public int step;

    int order;
    public int Order { get { return order; } set { order = value; } }

    public float expandTime = 0.2f;
    public float moveTIme = 0.2f;
    bool dropped;
    bool isMerging;
    float targetRadius;

    public bool CanMerge { get { return dropped && isMerging == false; } }
    Camera cam;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        //Vector3 nextPos = transform.position + (Vector3)rigid.velocity * Time.deltaTime;
        //if (CheckBorder(nextPos))
        //    rigid.velocity = Vector3.zero;
    }

    bool CheckBorder(Vector3 nextPos)
    {
        Vector3 rightNextPos = nextPos + Vector3.right * Radius;
        Vector3 leftNextPos = nextPos - Vector3.right * Radius;

        Vector3 camPos = cam.WorldToViewportPoint(rightNextPos);
        if (camPos.x >= 1f)
        {
            return true;
        }


        camPos = cam.WorldToViewportPoint(leftNextPos);
        if (camPos.x <= 0f)
        {
            return true;
        }

        else return false;
    }

    public virtual void Drop()
    {
        collider.enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        dropped = true;
    }

    public void PushToPool()
    {
        Destroy(gameObject);
    }

    public void TouchControl(Vector3 nextPos)
    {
        nextPos.z = 0;

        if (CheckBorder(nextPos))
            nextPos.x = transform.position.x;

        transform.position = nextPos;
    }


    public void OnMatching(bool higher, Transform hitBall)
    {
        if (higher)
        {
            Merge(hitBall);
        }
        else
        {
            PushToPool();
        }
    }

    void Merge(Transform hitBall)
    {
        isMerging = true;
        GameManager.instance.AddScore(step);

        transform.DOMove(hitBall.transform.position, moveTIme).onComplete += () =>
            {
                rigid.velocity = Vector3.zero;
                SetFruit(step + 1);
                Expand();
            };

    }

    public void PopFromPool(int _step)
    {
        SetFruit(_step);
    }

    public void SetFruit(int _step)
    {
        this.step = _step;
        Renderer.sprite = GameManager.instance.GetSprite(step - 1);

        targetRadius = Collider.radius = Renderer.sprite.rect.width / (2 * Renderer.sprite.pixelsPerUnit);
        Collider.radius = targetRadius;
    }

    public void Expand()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, expandTime);
        
        Collider.radius = 0;
        DOTween.To(() => Collider.radius, x => Collider.radius = x, targetRadius, expandTime).onComplete += () =>
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = 0;
        };
        isMerging = false;
    }

    public void Cut(Vector3 dir)
    {
        collider.enabled = false;
        Vector3 Dir = dir + Vector3.up;
        rigid.AddForce(Dir * 3f,ForceMode2D.Impulse);

        Invoke("PushToPool", 3);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball hitBall = collision.collider.GetComponent<Ball>();

        if (hitBall != null && CanMerge && hitBall.CanMerge && hitBall.step == this.step)
        {
            bool isHigher = hitBall.order <= order;

            if(isHigher)
            {
                hitBall.PushToPool();
                Merge(hitBall.transform);
            }
        }
    }
}
