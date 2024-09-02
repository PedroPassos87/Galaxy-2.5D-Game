using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _speed;
    //ID for powerups
    [SerializeField]private int _powerupID; //0 = triple shot  1 = speed 2 = shield
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        
        //destroy when leave the screen
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    
    //ontriggercolision only collectable by the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //communicate with the player script
            //handle to the component
            //assign the handle to component
            PlayerController player = other.transform.GetComponent<PlayerController>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Console.WriteLine("default value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
        
    }
    
}
