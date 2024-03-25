using Godot;
using System;

public partial class sceneloader : Node
{
	game_manager gameManager;
	game_over_screen gameOverScreen;
	start_menu startMenu;
	
	boot Boot;
	public Control failscreenInstance;
	public Node2D mainInstance;
	public Control startscreenInstance;

	playerdata Playerdata;
	savegame Savegame;
	public override void _Ready()
	{
		ProcessMode = Node.ProcessModeEnum.Always;

		Boot = GetNode<boot>("/root/boot");
		Boot.Boot += LoadStart;
		
		Playerdata = GetNode<playerdata>("/root/Playerdata");
		Savegame = GetNode<savegame>("/root/Savegame");


		

		
		
		
	}


	public void LoadStart()
	{
		var scene0 = GD.Load<PackedScene>("res://UI/start_menu.tscn");
		startscreenInstance = (Control)scene0.Instantiate();

		CallDeferred("add_child", startscreenInstance);

		startMenu = (start_menu)startscreenInstance;
		startMenu.GameStart += LoadMain;


	}
	public void LoadMain()
	{
		var scene1 = GD.Load<PackedScene>("res://main.tscn");
		mainInstance = (Node2D)scene1.Instantiate();

		CallDeferred("add_child", mainInstance);
		
		Savegame.OnBoot(mainInstance);
		Playerdata.OnBoot(mainInstance);

		gameManager = (game_manager)mainInstance;
		gameManager.LoadScene += ReloadMain;
		gameManager.GameOver += LoadGameOver;
		
		DeleteScenes("StartMenu");
		
	}

	public void LoadGameOver()
	{
		DeleteScenes("Main");
		
		var scene2 = GD.Load<PackedScene>("res://UI/game_over_screen.tscn");
		failscreenInstance = (Control)scene2.Instantiate();
		
		gameOverScreen = (game_over_screen)failscreenInstance;
		CallDeferred("add_child", failscreenInstance);
		gameOverScreen.Retry += ReloadMain;
	}

	public void ReloadMain(string scene)
	{
		DeleteScenes(scene);
		
		LoadMain();
	}

	private void DeleteScenes(string scene)
	{
		switch(scene)
			{
				case "Main":
				CallDeferred("remove_child", mainInstance);
				break;
				case "GameOverScreen":
				CallDeferred("remove_child", failscreenInstance);
				break;
				case "StartMenu":
				CallDeferred("remove_child", startscreenInstance);
				break;
			}
	}
}
