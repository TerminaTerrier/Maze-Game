using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

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
        navAgent.TargetReached += OnTargetReached;
        //SignalBus.StateChange += _on_state_change;
    }

    public void Enter()
    {
        timerNum = rng.RandfRange(10f, 15f);
        frightenedTimer.Start(timerNum);

       FindIntersect();
       

    }

    public void Update(float delta)
    {
       
        //GD.Print("I am Frightened");
    }

    private void GetTargetPath(Vector2 position)

    {
        switch(position)
        {
            
            case Vector2(1,0):
            navAgent.TargetPosition = new Vector2(895, 312);
            enemySpeed = 50f;
            break;
            case Vector2(1,1):
            //bottom right
            navAgent.TargetPosition = new Vector2(241, 312);
            enemySpeed = 50f;
            break;
            


        }
        
      
    }
    
    public void FindIntersect()
    {
         var spaceState = enemy.GetWorld2D().DirectSpaceState;
        var query = PhysicsRayQueryParameters2D.Create(enemy.GlobalPosition, playerdata.playerPosition);
        var result = spaceState.IntersectRay(query);

        var intersectPosition = (Vector2)result["position"];
        var intersectPositionNormal = intersectPosition.Normalized();

        float X = intersectPositionNormal.X;  
        float Y = intersectPositionNormal.Y;

        float X2 = (float)Math.Round(X, MidpointRounding.AwayFromZero);
        float Y2 = (float)Math.Round(Y, MidpointRounding.AwayFromZero);

        var intersectEstimate = new Vector2(X2, Y2);
        GetTargetPath(intersectEstimate);
        GD.Print(intersectPosition);
        GD.Print(intersectPositionNormal);
        GD.Print(intersectEstimate);
    }
    public void PhysicsUpdate(float delta)
    {
        


        Vector2 dir = enemy.ToLocal(navAgent.GetNextPathPosition()).Normalized();

        Vector2 velocity = enemy.Velocity;
        velocity = dir * enemySpeed;
        enemy.Velocity = velocity;

        enemy.MoveAndSlide(); 

        float X = dir.X;  
        float Y = dir.Y;

        float X2 = (float)Math.Round(X, MidpointRounding.AwayFromZero);
        float Y2 = (float)Math.Round(Y, MidpointRounding.AwayFromZero);

        dirEstimate = new Vector2(X2, Y2);

        

        //GD.Print(navAgent.IsTargetReachable());
        //GD.Print(dir);
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
    public void OnTargetReached()
    {
        if(fsm.currentStateKey == "Frightened")
        {
        fsm.TransitionTo("Chase");
        }
    }
    public void Exit()
    {
        frightenedTimer.Stop();
    }

}
