using Godot;
using System;
using System.Diagnostics;



public partial class IdleState : Node, IState
{
    public StateMachine fsm { get; set; }
    public Vector2 dirEstimate{ get; set;}
    [Export]
    public Timer timer;
    [Export]
    public CharacterBody2D enemy;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    float timerNum;
    Vector2 move = new Vector2(0, 1);
    string enemyName;
    
    signalbus SignalBus;

    public void Start()
    {
       enemyName = enemy.Name;
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
    }

    public void Enter()
    {
        enemy.SetCollisionMaskValue(7, true);
        timerNum = rng.RandfRange(10f, 15f);
        timer.Start(timerNum);
        SignalBus.ItemCollected += OnItemCollected;
        SignalBus.EmitSignal(signalbus.SignalName.StateChange, "Idle");

        if(enemyName != "Enemy_Purple")
        {
        fsm.currentStateKey = "Idle";
        }
        
    }

    public void Update(float delta)
    {
        // GD.Print(GetTimeLeft);
        //GD.Print("I am Idle");
    }

    public void PhysicsUpdate(float delta)
    {
        //GD.Print(enemy.Position);
        var collision = enemy.MoveAndCollide(move * 0.5f);

        if(collision == null)
        {
            enemy.MoveAndCollide(move * 0.5f);
        }

        if (collision != null)
        {
            if (enemy.GlobalPosition.Y > 262f)
            {
                move = new Vector2(0, -1);
                enemy.MoveAndCollide(move * 0.5f);
            }
            else if (enemy.GlobalPosition.Y < 262f)
            {
                move = new Vector2(0, 1);
                enemy.MoveAndCollide(move * 0.5f);
            }
           
        }

       // GD.Print(move);
    }

    public void _on_timer_timeout() 
    {
        if(enemyName != "Enemy_Blue")
        {
            fsm.TransitionTo("Chase");
        }
        else
        {
            return;
        }
    }

    public void OnItemCollected(StringName collectable)
    {
        if (collectable == "power_up" && fsm.currentStateKey == "Idle" && enemyName != "Enemy_Blue")
        {
            fsm.TransitionTo("Frightened");
        }
        else if(collectable == "power_up" && fsm.currentStateKey == "Idle" && enemyName == "Enemy_Blue")
        {
            fsm.TransitionTo("Chase");
        }
    }

    public void Exit()
    {
        enemy.SetCollisionMaskValue(7, false);

        timer.Stop();

    }
}
