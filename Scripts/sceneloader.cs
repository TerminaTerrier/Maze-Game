using Godot;
using System;

public partial class sceneloader : Node
{
	game_manager gameManager;
	Node2D Main;
	boot Boot;

	public Node2D mainInstance;

	playerdata Playerdata;
	savegame Savegame;
	public override void _Ready()
	{
		Boot = GetNode<boot>("/root/boot");
		Boot.Boot += LoadMain;
		
		Playerdata = GetNode<playerdata>("/root/Playerdata");
		Savegame = GetNode<savegame>("/root/Savegame");
		//can simplify these as get child
		
		
	}

	public void LoadMain()
	{
		var scene = GD.Load<PackedScene>("res://main.tscn");
		mainInstance = (Node2D)scene.Instantiate();

		CallDeferred("add_child", mainInstance);
		
		Savegame.OnBoot(mainInstance);
		Playerdata.OnBoot(mainInstance);

		gameManager = (game_manager)mainInstance;
		gameManager.LoadScene += ReloadMain;
		Main = mainInstance;
	}

	public void ReloadMain()
	{

		Main.QueueFree();

		LoadMain();
	}
}
