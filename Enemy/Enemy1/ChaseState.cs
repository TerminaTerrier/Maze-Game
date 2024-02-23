using Godot;
using System;
using System.Diagnostics;

public partial class ChaseState : Node, IState
{
    
    public StateMachine fsm { get; set; }
    Vector2 playerPos;
    [Export]
    CharacterBody2D enemy;
    [Export]
    NavigationAgent2D navAgent;
    [Export]
    Timer chaseTimer;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    float timerNum;
    string enemyName;
    float enemySpeed;
    signalbus SignalBus;

    public void Start()
    {
        enemyName = enemy.Name;
        
        SignalBus = GetNode<signalbus>("/root/Main/SignalBus");
        //GD.Print(enemyName);
    }

    public void Enter()
    {
        timerNum = rng.RandfRange(15f, 35f);
        chaseTimer.Start(timerNum);


    }

    public void Update(float delta)
    {
        //GD.Print(playerdata.playerPosition);
        GetTargetPath();
    }

    private void GetTargetPath()
    {
        switch(enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = playerdata.playerPosition;
                enemySpeed = 50f;
                break;
        }
    }

    

    public void PhysicsUpdate(float delta)
    {
        var dir = enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized();

        Vector2 velocity = enemy.Velocity;
        velocity = dir * enemySpeed;
        enemy.Velocity = velocity;

        enemy.MoveAndSlide();
         
        //GD.Print(navAgent.IsTargetReachable());
        //GD.Print(dir);
       // GD.Print(enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized());
        // GD.Print(navAgent.IsTargetReachable());
        // GD.Print(navAgent.IsNavigationFinished());
    }

    public void _on_timer_timeout()
    {
        fsm.TransitionTo("Scatter");
    }

    public void Exit()
    {
        chaseTimer.Stop();
    }
}
