using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    
    [SerializeField]
    Ball ball;

    [SerializeField]
    Paddle bottomPaddle, topPaddle;
    
    [SerializeField, Min(0f)]
    Vector2 arenaExtents = new Vector2(10f, 10f);
    
    
    [SerializeField, Min(2)]
    int pointsToWin = 3;
    
    
    
    [SerializeField]
    TextMeshPro countdownText;
	
    [SerializeField, Min(1f)]
    float newGameDelay = 3f;

    float countdownUntilNewGame;
    
    
    
    [SerializeField]
    LivelyCamera livelyCamera;
    
    
    
    
    void Awake ()
    {
        StartNewGame();
        
        countdownUntilNewGame = newGameDelay;
    }
    
    

    void Update ()
    {
        
        // ball.StartNewGame();
        // bottomPaddle.StartNewGame();
        // topPaddle.StartNewGame();
        
        bottomPaddle.Move(ball.Position.x, arenaExtents.x);
        topPaddle.Move(ball.Position.x, arenaExtents.x);
        
        
        if (countdownUntilNewGame <= 0f)
        {
            UpdateGame();
        }
        else
        {
            UpdateCountdown();
        }
        
        
    }
    
    
    void UpdateGame ()
    {
        ball.Move();
        BounceYIfNeeded();
        BounceXIfNeeded(ball.Position.x);
        ball.UpdateVisualization();
    }
    
    
    void UpdateCountdown ()
    {
        countdownUntilNewGame -= Time.deltaTime;
        
        if (countdownUntilNewGame <= 0f)
        {
            countdownText.gameObject.SetActive(false);
            StartNewGame();
            countdownText.SetText("3");
        }
        else
        {
            float displayValue = Mathf.Ceil(countdownUntilNewGame);

            int number = (int)displayValue;
            
            if (number < newGameDelay)
            {
                countdownText.SetText("{0}", number);
            }
        }
        
    }
    
    void StartNewGame ()
    {
        ball.StartNewGame();
        bottomPaddle.StartNewGame();
        topPaddle.StartNewGame();
    }
    
    
    void BounceYIfNeeded ()
    {
        float yExtents = arenaExtents.y - ball.Extents;
        if (ball.Position.y < -yExtents)
        {
            livelyCamera.PushXZ(ball.Velocity);
            BounceY(-yExtents, bottomPaddle, topPaddle);
        }
        else if (ball.Position.y > yExtents)
        {
            livelyCamera.PushXZ(ball.Velocity);
            BounceY(yExtents, topPaddle, bottomPaddle);
        }
    }
    
    void BounceXIfNeeded (float x)
    {
        float xExtents = arenaExtents.x - ball.Extents;
        if (x < -xExtents)
        {
            ball.BounceX(-xExtents);
        }
        else if (x > xExtents)
        {
            ball.BounceX(xExtents);
        }
    }
    
    
    
    void BounceY (float boundary, Paddle defender , Paddle attacker)
    {
        float durationAfterBounce = (ball.Position.y - boundary) / ball.Velocity.y;
        
        float bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;
        
        BounceXIfNeeded(bounceX);
        bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;
        livelyCamera.PushXZ(ball.Velocity);
        
        ball.BounceY(boundary);
        
        defender.MissEffect();
        
        if (defender.HitBall(bounceX, ball.Extents, out float hitFactor))
        {
            ball.SetXPositionAndSpeed(bounceX, hitFactor, durationAfterBounce);
            defender.HitEffect();
        }
        else if (attacker.ScorePoint(pointsToWin))
        {
            EndGame();
        }
    }
    
    void EndGame ()
    {
        countdownUntilNewGame = newGameDelay;
        countdownText.SetText("GAME OVER");
        countdownText.gameObject.SetActive(true);
        ball.EndGame();
    }
    
}
