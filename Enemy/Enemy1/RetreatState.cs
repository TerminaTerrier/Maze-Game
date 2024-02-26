using Godot;
using System;

public partial class RetreatState : Node, IState
{
    public StateMachine fsm { get; set; }
    [Export]
    CharacterBody2D enemy;
    [Export]
    NavigationAgent2D navAgent;
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
    }

    private void GetTargetPath()
    {
        var greenSpawn = new Vector2(538f, 262f);

        switch(enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = greenSpawn;
                enemySpeed = 50f;
                break;

        }

    }

    public void Update(float delta)
    {
        GetTargetPath();
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
    }

    public void _on_timer_timeout()
    {
        if(navAgent.IsTargetReached())
        {
            fsm.TransitionTo("Idle");
        }
    }

    public void Exit()
    {
        retreatTimer.Stop();
    }
}
