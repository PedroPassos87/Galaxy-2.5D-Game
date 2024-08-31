using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _speed;
    
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
                player.TripleShotActivate();
            }
            Destroy(this.gameObject);
        }
        
    }
    
}
