using Godot;
using System;

public partial class player : CharacterBody2D
{
    Direction intendedDirection;
    Direction currentDirection;

    Vector2 directionVec;

    bool isCollisionDetected;

    AnimatedSprite2D animatedSprite;

    RayCast2D rayCast_1;
    RayCast2D rayCast_2;

    [Export]
    public float PlayerSpeed { get; set; } = 80f;

    [Export]
    Area2D area2D;

    signalbus SignalBus;

    public enum Direction
    {
        Still, Left, Right, Up, Down
    }

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        rayCast_1 = GetNode<RayCast2D>("RayCast2D_1");
        rayCast_2 = GetNode<RayCast2D>("RayCast2D_2");
        currentDirection = Direction.Left;

        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        SignalBus.LifeLost += OnLifeLost;
        SignalBus.LivesDepleted += OnLivesDepleted;
        SignalBus.LeftWarp += OnLeftWarp;
        SignalBus.RightWarp += OnRightWarp;
        //SignalBus.StateChange += OnStateChange;
        //GD.Print(Position);
    }

   

    public void OnLivesDepleted()
    {
        //QueueFree();
    }

    public void OnLifeLost()
    {
        Vector2 spawn = new Vector2(568, 313);
        Position = spawn;
    }

    public void OnLeftWarp(Node2D body)
    {
        if (body.Name == "Player")
        {
            GlobalPosition = new Vector2(870, 312);
        }    
    }

    public void OnRightWarp(Node2D body)
    {
        if(body.Name == "Player")
        {
            GlobalPosition = new Vector2(273, 312);
        }
       
    }

    public void GetInput()
    {
       
        
            if (Input.IsActionPressed("WalkLeft"))
            {
                intendedDirection = Direction.Left;

            }

            if (Input.IsActionPressed("WalkRight"))
            {
                intendedDirection = Direction.Right;

            }

            if (Input.IsActionPressed("WalkUp"))
            {
                intendedDirection = Direction.Up;

            }

            if (Input.IsActionPressed("WalkDown"))
            {
                intendedDirection = Direction.Down;
            }

            
       

      
    }

    private void SetRaycastTarget()
    {
       switch (intendedDirection)
        {
            case Direction.Still:
                break;
            case Direction.Left:
                rayCast_1.Position = new Vector2(-6, 3);
                rayCast_2.Position = new Vector2(-6, -5);
                rayCast_1.TargetPosition = new Vector2(-15, 0);
                rayCast_2.TargetPosition = new Vector2(-15, 0);
                rayCast_1.ForceRaycastUpdate();
                rayCast_2.ForceRaycastUpdate();
              //  GD.Print("Left");
                break;
            case Direction.Right:
                rayCast_1.Position = new Vector2(6, -3);
                rayCast_2.Position = new Vector2(6, 5);
                rayCast_1.TargetPosition = new Vector2(15, 0);
                rayCast_2.TargetPosition = new Vector2(15, 0);
                rayCast_1.ForceRaycastUpdate();
                rayCast_2.ForceRaycastUpdate();
               // GD.Print("Right");
                break;
            case Direction.Up:
                rayCast_1.Position = new Vector2(-5, -6);
                rayCast_2.Position = new Vector2(5, -6);
                rayCast_1.TargetPosition = new Vector2(0, -15);
                rayCast_2.TargetPosition = new Vector2(0, -15);
                rayCast_1.ForceRaycastUpdate();
                rayCast_2.ForceRaycastUpdate();
               // GD.Print("Up");
                break;
            case Direction.Down:
                rayCast_1.Position = new Vector2(-5, 4);
                rayCast_2.Position = new Vector2(5, 4);
                rayCast_1.TargetPosition = new Vector2(0, 15);
                rayCast_2.TargetPosition = new Vector2(0, 15);
                rayCast_1.ForceRaycastUpdate();
                rayCast_2.ForceRaycastUpdate();
               // GD.Print("Down");
                break;
        }

        
        SetMovement();
    }
       
    private void SetMovement()
    {
        if (currentDirection != intendedDirection)
        {

            if (rayCast_1.IsColliding() == false && rayCast_2.IsColliding() == false)
            {
                currentDirection = intendedDirection;
                
               // GD.Print("Input changed");
            }
            else
            {
                return;
            }

        }
        
    }

    public override void _PhysicsProcess(double delta)
    {
       SetRaycastTarget();


        GetInput();


        PlayerMovement();
        //GD.Print(rayCast_1.GetCollisionPoint());


       
    }

    private void PlayerMovement()
    {
        
        

        switch (currentDirection)
        {
            case Direction.Left:
                directionVec = new Vector2(-1, 0);
                animatedSprite.Play("MoveCycle");
                break;
            case Direction.Right:
                directionVec = new Vector2(1, 0);
                animatedSprite.Play("MoveCycle");
                break;
            case Direction.Up:
                directionVec = new Vector2(0, -1);
                animatedSprite.Play("MoveCycle");
                break;
            case Direction.Down:
                directionVec = new Vector2(0, 1);
                animatedSprite.Play("MoveCycle");
                break;
        }

        Vector2 velocity = Velocity;
        velocity = directionVec * PlayerSpeed;
        Velocity = velocity;

        MoveAndSlide();
    }

    
    
} 