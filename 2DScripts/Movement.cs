using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;//speed
    private float jumpingpower = 16f;//jumpforce
    private bool isfacingright = true;//to make sure which direction we are facing

    //import rigidbody
    [SerializeField] private Rigidbody2D rb;
    //import Groundcheck(Child)
    [SerializeField] private Transform groundcheck;
    //import Groundlayer(layer)
    [SerializeField] private LayerMask groundlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get horizantal input
        horizontal = Input.GetAxisRaw("Horizontal");

        //if press space and is grounded
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            //leave x velocity as is, and add force equivalent to jumping power
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
        }

        //if finger is lifted from jump and vertical velocity is greater than 0
        if(Input.GetButtonUp("Jump") && rb.velocity.y>0f)
        {
            //leave x velocity and halve the vertical velocity 
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //
        flip();


    }

    private void FixedUpdate()
    {
        //if left or right pressed then add x velocity in accordance to speed and leave y as is
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        //check if ground layer and the groundcheck object are overlapping
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }

    private void flip()
    {
        //if direction facing and input direction are opposite
        if (isfacingright   && horizontal <0f || !isfacingright && horizontal>0f)
        {
            //flip boolean
            isfacingright = !isfacingright;
            //todo
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale; 
        }
    }
}
