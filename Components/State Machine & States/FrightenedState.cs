using Godot;
using Godot.Collections;
using System;
using System.Reflection.Emit;

public partial class FrightenedState : Node, IState
{
    public StateMachine fsm { get; set; }
    public Vector2 dirEstimate{get; set;}
    [Export]
    CharacterBody2D enemy;
    [Export]
    NavigationAgent2D navAgent;
    float enemySpeed;
    string enemyName;
    Vector2 currentTarget;

    [Export]
    Timer frightenedTimer;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    float timerNum;
    
    string stateKey;
    
    signalbus SignalBus;

    public void Start()
    {
        enemyName = enemy.Name;
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        //SignalBus.StateChange += _on_state_change;
    }

    public void Enter()
    {
        timerNum = rng.RandfRange(10f, 15f);
        frightenedTimer.Start(timerNum);

        currentTarget = navAgent.TargetPosition;
        //GD.Print(currentTarget);
    }

    public void Update(float delta)
    {
        GetTargetPath();
        //GD.Print("I am Frightened");
    }

    private void GetTargetPath()
    {
        
        navAgent.TargetPosition = -currentTarget;
        enemySpeed = 50f;
    }

    public void PhysicsUpdate(float delta)
    {
        Vector2 dir = enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized();

        Vector2 velocity = enemy.Velocity;
        velocity = dir * enemySpeed;
        enemy.Velocity = velocity;

       
        float X = dir.X;  
        float Y = dir.Y;

        float X2 = (float)Math.Round(X, MidpointRounding.AwayFromZero);
        float Y2 = (float)Math.Round(Y, MidpointRounding.AwayFromZero);

        dirEstimate = new Vector2(X2, Y2);

        enemy.MoveAndSlide();

        // GD.Print(navAgent.IsTargetReachable());
        // GD.Print(dir);
    }

    public void _on_area_2d_body_entered(Node2D body)
    {
        var stateKey = fsm.currentStateKey;

        if (body.Name == "Player" && stateKey == "Frightened")
        {    
          
            SignalBus.EmitSignal(signalbus.SignalName.EnemyDefeat);
            fsm.TransitionTo("Retreat");
        }
       
    }

    public void _on_timer_timeout()
    {
        fsm.TransitionTo("Chase");
    }

    //public void _on_state_change(string key)
    //{
        //stateKey = key;
   // }

    public void Exit()
    {
        frightenedTimer.Stop();
    }

}
