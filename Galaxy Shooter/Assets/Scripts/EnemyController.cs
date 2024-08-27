using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float _speed;
    void Start()
    {
        
    }

    void Update()
    {
        //move down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX,7,0);  //spawna inimigo no intervalo de x
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  //inimigo colidiu com a nave
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            
            if (player != null) //verifica se o player existe
            {
                player.Damage(); //tira um de vida
            }
            
            Destroy(this.gameObject);  //destroi inimigo
        }

        if (other.CompareTag("Laser"))  //inimigo tomou o laser
        {
            Destroy(other.gameObject);   //destroi o inimigo
            Destroy(this.gameObject);    //destroi o laser
        }
    }
}
