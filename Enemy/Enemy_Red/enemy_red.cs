using Godot;
using System;

public partial class enemy_red : CharacterBody2D
{
	signalbus SignalBus;
	[Export]
    AnimatedSprite2D animatedSprite;
	StateMachine stateMachine;
	public override void _Ready()
	{
		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
        SignalBus.LifeLost += OnLifeLost;
        SignalBus.LeftWarp += OnLeftWarp;
        SignalBus.RightWarp += OnRightWarp;
		stateMachine = GetNode<StateMachine>("StateMachine");
		animatedSprite.Play("MoveCycleLeft");

	}

	public override void _PhysicsProcess(double delta)
    {
        AnimationController();
    }


	public void OnLifeLost()
	{
		Vector2 spawn = new Vector2(601, 262);
        Position = spawn;
		stateMachine.TransitionTo("Idle");
	}

	private void AnimationController()
    {
        var direction = stateMachine.direction;
		//GD.Print(direction);
        switch(direction)
        {
            case Vector2(0,-1):
            //up
            animatedSprite.Play(animatedSprite.Animation);
            break;
            case Vector2(0,1):
            //down
            animatedSprite.Play(animatedSprite.Animation);
            break;
            case Vector2(-1,0):
            //left
            animatedSprite.Play("MoveCycleLeft");
            break;
            case Vector2(1,0):
            //right
            animatedSprite.Play("MoveCycleRight");
            break;

        }
        
      
    }
    public void OnLeftWarp(Node2D body)
    {
        if (body.Name == "Enemy_Red")
        {
            GlobalPosition = new Vector2(870, 312);
        }    
    }

    public void OnRightWarp(Node2D body)
    {
        if(body.Name == "Enemy_Red")
        {
            GlobalPosition = new Vector2(273, 312);
        }
       
    }
}
