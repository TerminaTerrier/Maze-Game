using Godot;
using System;
using System.Reflection.Metadata;

public partial class health : Node2D
{

    public const int maxLives = 5;
    public static int lives;
    signalbus SignalBus;
    string enemyState;

    public override void _Ready()
    {
        lives = 2;
        SignalBus = GetNode<signalbus>("/root/Main/SignalBus");
        SignalBus.StateChange += OnStateChange;
    }


    public void _on_area_2d_body_entered(Node2D body)
    {
        if (body.IsInGroup("Enemies") == true)
        {
            if (lives >= 0 && lives <= maxLives && enemyState != "Frightened" && enemyState != "Retreat")
            {
                lives--;
                SignalBus.EmitSignal(signalbus.SignalName.LifeLost);
                GD.Print(lives);
            }
            else if (lives < 0)
            {
                SignalBus.EmitSignal(signalbus.SignalName.LivesDepleted);
            }
        }
    }

    public void _on_timer_timeout()
    {
        int score = score_manager.score;

        if (score == 5000 && lives <= maxLives && lives >= 0)
        {
            lives++;
        }
    }

    public void OnStateChange(string state)
    {
        enemyState = state;
    }
}
