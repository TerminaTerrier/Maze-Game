using Godot;
using System;

public partial class RetreatState : Node, IState
{
    public StateMachine fsm { get; set; }
    [Export]
    CharacterBody2D enemy;
    [Export]
    NavigationAgent2D navAgent;
    [Export]
    Area2D area2D;
    float enemySpeed;
    string enemyName;

    [Export]
    Timer retreatTimer;
    float timerNum;

    public void Start()
    {
        enemyName = enemy.Name;

    }

    public void Enter()
    {
        timerNum = 1f;
        retreatTimer.Start(timerNum);
        
        enemySpeed = 100f;

        enemy.SetCollisionMaskValue(2, false);
        area2D.SetCollisionMaskValue(2, false);
    }

    

    private void GetTargetPath()
    {
        var greenSpawn = new Vector2(538f, 262f);

        switch(enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = greenSpawn;
                break;

        }

    }

    public void Update(float delta)
    {
        if(navAgent.IsTargetReached() == false)
        {
            GetTargetPath();
        }
    }

    public void PhysicsUpdate(float delta)
    {
        var dir = enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized();

        Vector2 velocity = enemy.Velocity;
        velocity = dir * enemySpeed;
        enemy.Velocity = velocity;

        enemy.MoveAndSlide();

        GD.Print(dir);
        GD.Print(navAgent.IsTargetReachable());
        GD.Print(navAgent.IsTargetReached());
        GD.Print(navAgent.TargetPosition);
        GD.Print(navAgent.GetFinalPosition());
    }

    public void _on_timer_timeout()
    {
        if(navAgent.IsTargetReached())
        {
            fsm.TransitionTo("Idle");
        }
        timerNum = 1f;
    }

    public void Exit()
    {
        retreatTimer.Stop();
        enemy.SetCollisionMaskValue(2, true);
        area2D.SetCollisionMaskValue(2, true);
    }
}
