using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool isWalkingRight = true;
    public Transform rayStart;
    private Animator anim;
    private GameManager gameManager;

    public GameObject crystalEffect;
    void Awake()
    {
      
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
      
    }

    private void FixedUpdate()
    {
        if(!gameManager.isGameStarted)
        {
            return;
        }
        else
        {  
            anim.SetTrigger("GameStarted");
        }
        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDirection();
        }

     
        RaycastHit hit;

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
            anim.SetTrigger("IsFalling");
        else
        {
            anim.SetTrigger("Save");
        }
        

        if (transform.position.y < -2)
        {
            gameManager.EndGame();
        }
        
    }

    private void ChangeDirection()
    {
        if (!gameManager.isGameStarted)
        {
            return;
        }
        isWalkingRight = !isWalkingRight;
        if(isWalkingRight == true)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cystal")
        {
            
            gameManager.increaseScore();
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);
        }
    }
 
}


