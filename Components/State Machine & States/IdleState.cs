using Godot;
using System;
using System.Diagnostics;



public partial class IdleState : Node, IState
{
    public StateMachine fsm { get; set; }
    [Export]
    public Timer timer;
    [Export]
    public CharacterBody2D enemy;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    float timerNum;
    Vector2 move = new Vector2(0, 1);
    signalbus SignalBus;

    public void Start()
    {
        timerNum = rng.RandfRange(10f, 15f);
        SignalBus = GetNode<signalbus>("/root/Main/SignalBus");
    }

    public void Enter()
    {
        timer.Start(timerNum);
        SignalBus.ItemCollected += OnItemCollected;
        SignalBus.EmitSignal(signalbus.SignalName.StateChange, "Idle");
    }

    public void Update(float delta)
    {
        // GD.Print(GetTimeLeft);
        
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
      fsm.TransitionTo("Chase");
    }

    public void OnItemCollected(StringName collectable)
    {
        if (collectable == "power_up")
        {
            fsm.TransitionTo("Frightened");
        }
    }

    public void Exit()
    {
        

        

    }
}
