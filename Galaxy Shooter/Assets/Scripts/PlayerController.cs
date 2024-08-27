using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float _speed;

    [Header("Skills")]
    [SerializeField]private GameObject _laserPrefab;
    [SerializeField]private float _fireRate;
    private float _canFire = -1f;
    
    [Header("Player stats")]
    [SerializeField]private int _lives;
    
    private SpawnManager _spawnManager;
    
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        _spawnManager =
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); //find the object and get component

        if (_spawnManager == null)
        {
            Debug.LogError("No SpawnManager found");
        }
    }

    void Update()
    {
        PlayerMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
          FireLaser(); 
        }
        
    }

    public void Damage()
    {
        _lives --;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    //spawn laser
    private void FireLaser()
    {
        
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0,0.95f,0), Quaternion.identity);    
            
        
    }

    //all the movements and border limits
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");   //get horizontal input
        float verticalInput = Input.GetAxis("Vertical");    //get vertical input
        
        //mais otimizado
        Vector3 direction = new Vector3(horizontalInput,verticalInput,  0);
        transform.Translate(direction * _speed * Time.deltaTime);
       
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


