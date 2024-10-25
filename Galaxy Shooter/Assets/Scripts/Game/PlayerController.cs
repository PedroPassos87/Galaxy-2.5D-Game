using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float _speed;

    [Header("Skills")]
    [SerializeField]private float _fireRate;
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private int _speedMultiplier = 2;
    private bool _isShieldActive = false;
    
    [Header("Player stats")]
    [SerializeField]private int _lives;
    private int _score;
    
    [Header("Variable references")]
    [SerializeField]private GameObject _laserPrefab;
    [SerializeField]private GameObject _tripleShotPrefab;
    [SerializeField]private GameObject _shieldVisualizer;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); //find the object and get component
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();   

        if (_spawnManager == null)
        {
            Debug.LogError("No SpawnManager found");
        }

        if (_uiManager == null)
        {
            Debug.LogError("No UIManager found");

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

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);

    }
    

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    public void TripleShotActive()
    {
        //tripleshot becomes true
        _isTripleShotActive = true;
        //start the power down coroutine for triple shot
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        _lives--; //diminui vida
        
        _uiManager.UpdateLives(_lives);  //update da ui
        
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

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0,0.95f,0), Quaternion.identity);    
        }
          
        
    }

    //all the movements and border limits
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");   //get horizontal input
        float verticalInput = Input.GetAxis("Vertical");    //get vertical input
        
        //mais otimizado
        Vector3 direction = new Vector3(horizontalInput,verticalInput,  0);
        transform.Translate(direction * _speed* Time.deltaTime);
        
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


