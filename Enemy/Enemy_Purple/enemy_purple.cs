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
        Position = spawn;
	}
	private void AnimationController()
    {
        var direction = stateMachine.direction;
		GD.Print(direction);
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
}
