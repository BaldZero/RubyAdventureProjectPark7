using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxhp = 5;
    public float timeInvincible = 2.0f;
    public int health { get {  return currenthp; } }
    int currenthp;
    public float speed = 3.0f;

    bool isInvincible;
    float invincibleTime;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public GameObject projectilePrefab;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        currenthp = maxhp;

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2 (horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if(isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if(Input.GetKeyDown (KeyCode.X))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit2D.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit2D.collider.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

    public void Changehp(int amount)
    {
        if(amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTime = timeInvincible;
            animator.SetTrigger("Hit");

        }
        currenthp = Mathf.Clamp(currenthp + amount, 0, maxhp);
        UIHealth.instance.SetValue(currenthp/(float)maxhp);
    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        BulletThing projectile = projectileObject.GetComponent<BulletThing>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

}
