using Godot;
using System;

public partial class sceneloader : Node
{
	game_manager gameManager;
	game_over_screen gameOverScreen;
	Node2D Main;
	boot Boot;
	public Control failscreenInstance;
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
		gameManager.GameOver += OnGameOver;
		Main = mainInstance;
		
	}

	public void ReloadMain()
	{

		failscreenInstance.QueueFree();

		LoadMain();
	}

	public void OnGameOver()
	{
		Main.QueueFree();

		var scene = GD.Load<PackedScene>("res://UI/game_over_screen.tscn");
		failscreenInstance = (Control)scene.Instantiate();
		gameOverScreen = (game_over_screen)failscreenInstance;
		CallDeferred("add_child", failscreenInstance);
		gameOverScreen.Retry += ReloadMain;
	}

	private void DeleteScenes(string scene)
	{
		switch(scene)
			{
				case "Main":
				Main.QueueFree();
				break;
			}
	}
}
