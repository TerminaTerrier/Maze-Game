using Godot;
using System;
using System.Reflection.Metadata;

public partial class health : Node2D
{
    score_manager Score_Manager;
    public const int maxLives = 5;
    public static int lives;
    signalbus SignalBus;
    string enemyState;

    int scoreBase;
    static int scoreIncrement = 1;

    public override void _Ready()
    {
        lives = 2;
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        SignalBus.StateChange += OnStateChange;

        Score_Manager = GetNode<score_manager>("/root/Sceneloader/Main/Score_Manager");
        Score_Manager.ScoreChange += OnScoreChange; 

        scoreBase = 1000;
    }


    public void _on_area_2d_body_entered(Node2D body)
    {
        var enemyName = body.Name;
        GD.Print(enemyName);
        if (body.IsInGroup("Enemies") == true)
        {
             
             if (lives <= 0)
            {
                lives--;
                SignalBus.EmitSignal(signalbus.SignalName.LivesDepleted);
                scoreBase = 1000;
            }

            if (lives > 0 && lives <= maxLives && enemyState != "Frightened" && enemyState != "Retreat" && enemyName != "Enemy_Blue")
            {
                lives--;
                SignalBus.EmitSignal(signalbus.SignalName.LifeLost);
                GD.Print(lives);
            }
            
           
            
            if(lives > 0 && lives <= maxLives && enemyName == "Enemy_Blue")
            {
                lives--;
                SignalBus.EmitSignal(signalbus.SignalName.LifeLost);
                GD.Print(lives);
            }
           
            
        }
    }


    public void OnStateChange(string state)
    {
        enemyState = state;
    }

    public void OnScoreChange(int score)
    {  
        
        if(score >= scoreBase * scoreIncrement && lives < maxLives && lives >= 0 )
        {
            scoreIncrement++;
            lives++;
            SignalBus.EmitSignal(signalbus.SignalName.LifeGain);
            GD.Print(lives);
        }
    }
}
