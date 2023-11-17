using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHP : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null) 
        {
            if (controller.health < controller.maxhp)
            {
                controller.Changehp(1);
                Destroy(gameObject);
            }
        }
    }
    /* Start is called before the first frame update
    void Start()
    {
        We don't need start for this code.    
    }

     Update is called once per frame
    void Update()
    {
      We don't need update for this code.
    }
    */
}
