using Godot;
using Godot.Collections;
using System;
using System.Reflection.Emit;

public partial class FrightenedState : Node, IState
{
    public StateMachine fsm { get; set; }
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

    public void Start()
    {
        enemyName = enemy.Name;
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
    }

    private void GetTargetPath()
    {
        
        navAgent.TargetPosition = -currentTarget;
        enemySpeed = 50f;
    }

    public void PhysicsUpdate(float delta)
    {
        var dir = enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized();

        Vector2 velocity = enemy.Velocity;
        velocity = dir * enemySpeed;
        enemy.Velocity = velocity;

        enemy.MoveAndSlide();

       // GD.Print(navAgent.IsTargetReachable());
       // GD.Print(dir);
    }

    public void _on_timer_timeout()
    {
        fsm.TransitionTo("Chase");
    }

    public void Exit()
    {
        frightenedTimer.Stop();
    }

}
