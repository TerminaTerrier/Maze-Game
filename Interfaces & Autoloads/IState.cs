using Godot;
using System;

public interface IState
{
	public StateMachine fsm { get; set;} 
	
	public Vector2 dirEstimate { get; set; }

	public void Enter();
	public void Exit();
	public void Start();
	public void Update(float delta);
	public void PhysicsUpdate(float delta);
}
