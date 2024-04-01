using Godot;
using System;

public partial class enemy_blue : CharacterBody2D
{
    [Export]
    AnimatedSprite2D animatedSprite;
	StateMachine stateMachine;
    signalbus SignalBus;

    public override void _Ready()
    {
        SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");

        SignalBus.LifeLost += OnLifeLost;
        stateMachine = GetNode<StateMachine>("StateMachine");
        GD.Print(Position);
        animatedSprite.Play("MoveCycleLeft");
      
    }
    public override void _PhysicsProcess(double delta)
    {
        AnimationController();
    }
    public void OnLifeLost()
    {
        Vector2 spawn = new Vector2(568, 264);
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
            if(stateMachine.currentStateKey != "Chase")
            {
               animatedSprite.Play("MoveCycleLeft");
            }
            else if (stateMachine.currentStateKey == "Chase")
            {
               animatedSprite.Play("ChaseMoveCycleLeft"); 
            }
            break;
            case Vector2(1,0):
            //right
            if(stateMachine.currentStateKey != "Chase")
            {
               animatedSprite.Play("MoveCycleRight");
            }
            else if (stateMachine.currentStateKey == "Chase")
            {
               animatedSprite.Play("ChaseMoveCycleRight"); 
            }
            break;

        }
        
      
    }
   
}
