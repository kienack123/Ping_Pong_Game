using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    [SerializeField, Min(0f)] private float
        startXSpeed = 8f, maxStartXSpeed = 2f;

    [SerializeField, Min(0f)] private float
        maxXSpeed = 20f,
        constantYSpeed = 10f ,extents = 0.5f;

    [SerializeField] private ParticleSystem ballEffect;
    
    public Vector2 Velocity => velocity;
    
    Vector2 position, velocity;
    
    
    public float Extents => extents;
	
    public Vector2 Position => position;

    private bool isStartgame = false;


    private void Awake()
    {
        gameObject.SetActive(false);
    }


    public void UpdateVisualization ()
    {
        transform.localPosition = new Vector3(position.x, 0f, position.y);
    }

    public void Move ()
    {
        position += velocity * Time.deltaTime;
        if (!isStartgame)
        {
            ballEffect.Play();
            isStartgame = true;
        }
    }


    public void StartNewGame ()
    {
        position = Vector2.zero;
        UpdateVisualization();
        velocity.x = UnityEngine.Random.Range(-maxStartXSpeed, maxStartXSpeed);
        velocity.y = -constantYSpeed;
        gameObject.SetActive(true);
    }
    
    
    public void EndGame ()
    {
        position.x = 0f;
        gameObject.SetActive(false);
        ballEffect.Stop();
        isStartgame = false;
    }
    
    
    public void BounceX (float boundary)
    {
        position.x = 2f * boundary - position.x;
        velocity.x = -velocity.x;
    }

    public void BounceY (float boundary)
    {
        position.y = 2f * boundary - position.y;
        velocity.y = -velocity.y;
    }
    
    public void SetXPositionAndSpeed (float start, float speedFactor, float deltaTime)
    {
        velocity.x = maxXSpeed * speedFactor;
        position.x = start + velocity.x * deltaTime;
    }
    
}
