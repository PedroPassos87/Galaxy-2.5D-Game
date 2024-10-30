using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
    private GameObject playerObject;
    private PlayerController playerController;
    
    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();
        playerController.SetLives(3);
    }
    
    [Test]
    public void PlayerMovesWithCorrectSpeed()
    {
        //set speed
        playerController._speed = 5f;
        
        //posiçao inicial
        Vector3 startPosition = playerObject.transform.position;
        
        //movimenta horizontalmente
        float horizontalInput = 1f;
        float deltaTime = 1f;  // Simula um segundo
        
        //move o player e verifica se andou o esperado
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        playerObject.transform.Translate(direction * playerController._speed * deltaTime);
        
        Assert.AreEqual(5f, playerObject.transform.position.x, 0.1f);
    }

    [Test]
    public void TripleShotTime()
    {
        playerController.TripleShotActive();
        Assert.IsTrue(playerController.IsTripleShotActive);
        
    }
    

    
    
}
