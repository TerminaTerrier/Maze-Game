using Godot;
using System;
using System.Collections;


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
    int targetNum;
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

         targetNum = rng.RandiRange(1, 5);

        SignalBus.ItemCollected += OnItemCollected;
        SignalBus.EmitSignal(signalbus.SignalName.StateChange, "Chase");

        if(enemyName == "Enemy_Purple")
        {
            fsm.currentStateKey = "Chase";
        }
    }

    public void Update(float delta)
    {
        //GD.Print(playerdata.playerPosition);
        GetTargetPath();
        //GD.Print("I am in Chase");
    }

    private void GetTargetPath()
    {
        

        switch(enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = playerdata.playerPosition;
                enemySpeed = 50f;
                break;
            case "Enemy_Red":
                navAgent.TargetPosition = playerdata.playerPosition + new Vector2(1,1);
                enemySpeed = 50f;
                break;
            case "Enemy_Purple":
                
                navAgent.TargetPosition = RandomTarget();
                enemySpeed = 50f;
                break;
        }
    }

    private Vector2 RandomTarget()
    {
        Vector2 randTargetVector = new Vector2();
        //GD.Print(targetNum);

        switch(targetNum)
        {
            case 1:
            randTargetVector = new Vector2(282f, 166f);
            break;
            case 2:
            randTargetVector = new Vector2(854f, 168f);
            break;
            case 3:
            randTargetVector = new Vector2(281f, 455f);
            break;
            case 4:
            randTargetVector = new Vector2(856f, 457f);
            break;
            case 5:
            randTargetVector = new Vector2(568f, 313f);
            break;
        }

        if(enemyName == "Enemy_Purple" && navAgent.IsTargetReached())
        {
          targetNum = rng.RandiRange(1, 5);
        }
         
        return randTargetVector;
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

    public void OnItemCollected(StringName collectable)
    {
        if(collectable == "power_up" && fsm.currentStateKey == "Chase")
        {
            fsm.TransitionTo("Frightened");
        }
    }

    public void Exit()
    {
        chaseTimer.Stop();
    }
}
