using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private float _spedd;
    

    void Update()
    {
        //laser up
        transform.Translate(Vector3.up * Time.deltaTime * _spedd);
        
        //destroy the object
        if (transform.position.y >= 8f)
        {
            Destroy(gameObject);
        }
    }
    
    
}
