using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float _speed;
    
    
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        playerMovement();
        sceneLimits();
        
    }

    private void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");   //get horizontal input
        float verticalInput = Input.GetAxis("Vertical");    //get vertical input
        
        /*vector * input * speed * real time
        transform.Translate(Vector3.right* horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up* verticalInput * _speed * Time.deltaTime);*/

        //mais otimizado
        Vector3 direction = new Vector3(horizontalInput,verticalInput,  0);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void sceneLimits()
    {
       /* if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.77f)
        {
            transform.position = new Vector3(transform.position.x, -3.77f, 0);
        }
        */
       
       //mais eficaz
       transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.77f,0), 0);
       
       //o x nao utilizo o clamp pois quero que, ao passar do limite, a nave apareça do outro lado da tela
       if (transform.position.x <= -11.4f)
       {
           transform.position = new Vector3(11.44f, transform.position.y, 0);
       }
       else if (transform.position.x >= 11.44f)
       {
           transform.position = new Vector3(-11.4f, transform.position.y, 0);
       }

    }
}


