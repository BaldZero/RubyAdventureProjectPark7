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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currenthp = maxhp;
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0)
                isInvincible = false;
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

        }
        currenthp = Mathf.Clamp(currenthp = amount, 0, maxhp);
        Debug.Log(currenthp + "/" + maxhp);
    }
}
