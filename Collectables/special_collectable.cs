using Godot;
using System;

public partial class special_collectable : Node2D
{	
	[Export]
	Sprite2D sprite;
	[Export]
	Timer timer;
	signalbus SignalBus;
	Texture2D greenSpecial = GD.Load<Texture2D>("res://assets/Art/Custom/greenspecial.png");
	Texture2D blueSpecial = GD.Load<Texture2D>("res://assets/Art/Custom/bluespecial.png");
	Texture2D orangeSpecial = GD.Load<Texture2D>("res://assets/Art/Custom/orangespecial.png");
	RandomNumberGenerator rng = new RandomNumberGenerator();
	int specialTypeNum;
	public override void _Ready()
	{
		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		SignalBus.LifeLost += OnLifeLost;
		timer.Start(15f);
	  	specialTypeNum = rng.RandiRange(1, 3);
		//use switch to select texture
		switch(specialTypeNum)
		{
			case 1:
			sprite.Texture = orangeSpecial;
			break;
			case 2:
			sprite.Texture = greenSpecial;
			break;
			case 3:
			sprite.Texture = blueSpecial;
			break;
		}
	}

	public void OnLifeLost()
	{
		QueueFree();
	}
	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{ 
			GD.Print("Received");
			switch(specialTypeNum)
			{
			case 1:
            SignalBus.EmitSignal(signalbus.SignalName.ItemCollected, "orangeSpecial");
			break;
			case 2:
			SignalBus.EmitSignal(signalbus.SignalName.ItemCollected, "greenSpecial");
			break;
			case 3:
			SignalBus.EmitSignal(signalbus.SignalName.ItemCollected, "blueSpecial");
			break;

			}
			//use switch to pick which kind of signal to send
            QueueFree();
            
		}
	}
	public void _on_timer_timeout()
	{
		QueueFree();
	}

	
}
