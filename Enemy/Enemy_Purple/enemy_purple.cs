using Godot;
using System;

public partial class enemy_purple : CharacterBody2D
{
	[Export]
    AnimatedSprite2D animatedSprite;
	signalbus SignalBus;
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
		Vector2 spawn = new Vector2(567, 214);
        stateMachine.TransitionTo("Chase");
        Position = spawn;
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
            if(stateMachine.currentStateKey != "Frightened")
            {
               animatedSprite.Play("MoveCycleLeft");
            }
            else if (stateMachine.currentStateKey == "Frightened")
            {
               animatedSprite.Play("FrightenedMoveCycleLeft"); 
            }
            break;
            case Vector2(1,0):
            //right
            if(stateMachine.currentStateKey != "Frightened")
            {
               animatedSprite.Play("MoveCycleRight");
            }
            else if (stateMachine.currentStateKey == "Frightened")
            {
               animatedSprite.Play("FrightenedMoveCycleRight"); 
            }
            break;

        }
    }
    public void OnLeftWarp(Node2D body)
    {
        if (body.Name == "Enemy_Purple")
        {
            GlobalPosition = new Vector2(870, 312);
        }    
    }

    public void OnRightWarp(Node2D body)
    {
        if(body.Name == "Enemy_Purple")
        {
            GlobalPosition = new Vector2(273, 312);
        }
       
    }
}
