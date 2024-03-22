using Godot;
using System;

public partial class savegame : Node
{
	Node ScoreManager;
	game_manager Main;
	boot Boot;

	public override void _Ready()
	{
		//Boot = GetNode<boot>("/root/boot");
		
		//Boot.Boot += OnBoot;
	}

	public void OnBoot(Node2D main)
	{
		//Main = GetNode<Nod>("/root/Sceneloader/Main");
		//ScoreManager = GetNode<Node>("/root/Sceneloader/Main/Score_Manager");
		//Main.SaveData += OnSaveData;
		Main = main.GetNode<game_manager>(".");
		ScoreManager = main.GetNode<Node>("Score_Manager");
	}
	
	public void OnSaveData()
	{
		SaveGame();
	}

	public void SaveGame()
	{
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

		var nodeData = ScoreManager.Call("Save");

		var jsonString = Json.Stringify(nodeData);

		saveGame.StoreLine(jsonString);
	}

	
}