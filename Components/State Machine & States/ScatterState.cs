using Godot;
using System;


public partial class ScatterState : Node, IState
{
    public StateMachine fsm { get; set; }
     public Vector2 dirEstimate {get; set;}
    [Export]
    CharacterBody2D enemy;
    [Export]
    NavigationAgent2D navAgent;
    [Export]
    Timer scatterTimer;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    float timerNum;
    string enemyName;
    float enemySpeed;
    Vector2 currentTarget;
    Vector2 greenTarget1 = new Vector2(280, 165);
    Vector2 greenTarget2 = new Vector2(360, 230);
    Vector2 redTarget1 = new Vector2(856, 166);
    Vector2 redTarget2 = new Vector2(775, 231);
    Vector2 purpleTarget1 = new Vector2(567, 169);
    Vector2 purpleTarget2 = new Vector2(567, 455);
    signalbus SignalBus;
   

    public void Start()
    {
        enemyName = enemy.Name;
        
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        SignalBus.ItemCollected += OnItemCollected;
        navAgent.TargetReached += OnTargetReached;
    }

    public void Enter()
    {
        timerNum = rng.RandfRange(7f, 14f);
        scatterTimer.Start(timerNum);
        

      

    
      if(enemyName == "Enemy_Green")
        {
            currentTarget = greenTarget1;
            GetScatterTarget(currentTarget);
        }
      if(enemyName == "Enemy_Red")
        {
            currentTarget = redTarget1;
            GetScatterTarget(currentTarget);
        }  
        if(enemyName == "Enemy_Purple")
        {
            currentTarget = purpleTarget1;
            GetScatterTarget(currentTarget);
        }   
      //  GD.Print("Scatter entered!");
    }

    public void Update(float delta)
    {
        if(enemyName == "Enemy_Purple")
        {
            GD.Print(currentTarget);
            GD.Print(navAgent.IsTargetReachable());
        }
        
      //  GD.Print("I am in Scatter");
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

       // GD.Print(navAgent.IsTargetReachable());
     //   GD.Print(dir);
    }

    private void GetScatterTarget(Vector2 target)
    {
        switch (enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = currentTarget;
                enemySpeed = 50f;
                break;
            case "Enemy_Red":
                navAgent.TargetPosition = currentTarget;
                enemySpeed = 50f;
                break;
            case "Enemy_Purple":
                navAgent.TargetPosition = currentTarget;
                enemySpeed = 50f;
                break;
        }
    }

    public void _on_timer_timeout()
    {
        fsm.TransitionTo("Chase");
    }

    public void OnItemCollected(StringName collectable)
    {
        if (collectable == "power_up" && fsm.currentStateKey == "Scatter")
        {
            fsm.TransitionTo("Frightened");
        }
    }

    public void OnTargetReached()
    {
     
     if(fsm.currentStateKey == "Scatter")
     {
        if(enemyName == "Enemy_Green" && currentTarget == greenTarget1)
        {
            currentTarget = greenTarget2;
            GetScatterTarget(currentTarget);
        }
        else if(enemyName == "Enemy_Green" && currentTarget == greenTarget2)
        {
            currentTarget = greenTarget1;
            GetScatterTarget(currentTarget);
        }
        if(enemyName == "Enemy_Red" && currentTarget == redTarget1)
        {
            currentTarget = redTarget2;
            GetScatterTarget(currentTarget);
        } 
        else if(enemyName == "Enemy_Red" && currentTarget == redTarget2)
        {
            currentTarget = redTarget1;
            GetScatterTarget(currentTarget);
        } 
        if(enemyName == "Enemy_Purple" && currentTarget == purpleTarget1)
        {
            GD.Print("change target1!");
            currentTarget = purpleTarget2;
            GetScatterTarget(currentTarget);
        }   
        else if(enemyName == "Enemy_Purple" && currentTarget == purpleTarget2)
        {
             GD.Print("change target2!");
            currentTarget = purpleTarget1;
            GetScatterTarget(currentTarget);
        }
     }
     else
     {
        return;
     }
    }

    public void Exit()
    {   
        //currentTarget = Vector2.Zero;
        scatterTimer.Stop();
    }
}
