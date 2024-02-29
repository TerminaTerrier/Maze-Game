using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export]
	public NodePath initialState;
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	private IState currentState;
	public string currentStateKey{get; set;}

	signalbus SignalBus;
	public override void _Ready()
	{
        SignalBus = GetNode<signalbus>("/root/Main/SignalBus");

        foreach (Node node in GetChildren())
		{
			if (node is IState s)
			{
				states[node.Name] = s;

				s.fsm = this;

				s.Start();
				s.Exit();
			}

		}
		currentState = GetNode<IState>(initialState);
		currentState.Enter();


        SignalBus.EmitSignal(signalbus.SignalName.StateChange, "Idle");

        //GD.Print(states.Keys);
    }

	public override void _Process(double delta)
    {
		currentState.Update((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
		currentState.PhysicsUpdate((float)delta);
	}

	public void TransitionTo(string key)
	{
		if (!states.ContainsKey(key) || currentState == states[key])
		{
			return;
		}
		currentState.Exit();
		currentState = states[key];
		currentState.Enter();

		currentStateKey = key;
		SignalBus.EmitSignal(signalbus.SignalName.StateChange, key);
		GD.Print(key);

	}
}
