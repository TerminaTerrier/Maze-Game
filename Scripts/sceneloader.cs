using Godot;
using System;

public partial class sceneloader : Node
{
	game_manager gameManager;
	Node Main;
	public override void _Ready()
	{
		gameManager = GetNode<game_manager>("/root/Main");
		gameManager.LoadScene += ReloadMain;

		Main = GetNode<Node>("/root/Main");
	}

	public void ReloadMain()
	{
		Main.QueueFree();

		var scene = GD.Load<PackedScene>("res://main.tscn");
		var mainInstance = (Node2D)scene.Instantiate();

		AddChild(mainInstance);
		Main = mainInstance;

	}
}
