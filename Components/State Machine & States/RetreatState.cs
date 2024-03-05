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

        enemy.SetCollisionMaskValue(1, false);
        area2D.SetCollisionMaskValue(1, false);

        enemy.SetCollisionMaskValue(2, false);
        area2D.SetCollisionMaskValue(2, false);

        switch(enemyName)
        {
        case "Enemy_Green":
        enemy.SetCollisionMaskValue(3, false);
        area2D.SetCollisionMaskValue(3, false);
        break;
        case "Enemy_Red":
        enemy.SetCollisionMaskValue(4, false);
        enemy.SetCollisionMaskValue(4, false);
        break;
        case "Enemy_Purple":
        enemy.SetCollisionMaskValue(5, false);
        enemy.SetCollisionMaskValue(5, false);
        break;
        }
       
    }

    

    private void GetTargetPath()
    {
        var greenSpawn = new Vector2(538f, 262f);
        var redSpawn = new Vector2(601f, 262f);
        var purpleSpawn = new Vector2(567f, 214f);

        switch(enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = greenSpawn;
                break;
            case "Enemy_Red":
                navAgent.TargetPosition = redSpawn;
                break;
            case "Enemy_Purple":
                navAgent.TargetPosition = purpleSpawn;
                break;
        }

    }

    public void Update(float delta)
    {
        if(navAgent.IsTargetReached() == false)
        {
            GetTargetPath();
        }

        GD.Print("I am in Retreat");
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
        else
        {
            return;
        }
        
        timerNum = 1f;
    }

    public void Exit()
    {
        retreatTimer.Stop();

        enemy.SetCollisionMaskValue(1, true);
        area2D.SetCollisionMaskValue(1, true);

        enemy.SetCollisionMaskValue(2, true);
        area2D.SetCollisionMaskValue(2, true);

        switch(enemyName)
        {
        case "Enemy_Green":
        enemy.SetCollisionMaskValue(3, true);
        area2D.SetCollisionMaskValue(3, true);
        break;
        case "Enemy_Red":
        enemy.SetCollisionMaskValue(4, true);
        enemy.SetCollisionMaskValue(4, true);
        break;
        case "Enemy_Purple":
        enemy.SetCollisionMaskValue(5, true);
        enemy.SetCollisionMaskValue(5, true);
        break;
        }
    }
}
