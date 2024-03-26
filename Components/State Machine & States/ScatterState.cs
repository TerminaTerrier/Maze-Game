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
   
    signalbus SignalBus;
   

    public void Start()
    {
        enemyName = enemy.Name;
       
        
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
    }

    public void Enter()
    {
        timerNum = rng.RandfRange(5f, 10f);
        scatterTimer.Start(timerNum);
        SignalBus.ItemCollected += OnItemCollected;
      //  GD.Print("Scatter entered!");
    }

    public void Update(float delta)
    {
        GetScatterTarget();
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

    private void GetScatterTarget()
    {
        switch (enemyName)
        {
            case "Enemy_Green":
                navAgent.TargetPosition = Vector2.Zero;
                enemySpeed = 50f;
                break;
            case "Enemy_Red":
                navAgent.TargetPosition = new Vector2(920, 120);
                enemySpeed = 50f;
                break;
            case "Enemy_Purple":
                navAgent.TargetPosition = new Vector2(540, 225);
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

    public void Exit()
    {
        scatterTimer.Stop();
    }
}
