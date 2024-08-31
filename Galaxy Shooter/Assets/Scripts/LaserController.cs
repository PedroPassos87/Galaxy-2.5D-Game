using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    void Update()
    {
        //laser up
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        
        //destroy the object
        if (transform.position.y > 8f)
        {
            //check if this object has a parent
            if (transform.parent != null)
            {
                //destroy the parent to
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
    
    
}
