using Godot;
using System;

public partial class savegame : Node
{
	Node ScoreManager;
	game_manager Main;
	boot Boot;

	public override void _Ready()
	{
		
		
	}

	public void OnBoot(Node2D main)
	{
		//Main = GetNode<Nod>("/root/Sceneloader/Main");
		//ScoreManager = GetNode<Node>("/root/Sceneloader/Main/Score_Manager");
		
		Main = main.GetNode<game_manager>(".");
		ScoreManager = main.GetNode<Node>("Score_Manager");
		Main.SaveData += OnSaveData;
	}
	
	public void OnSaveData()
	{
		SaveGame();
	}

	public void SaveGame()
	{
		using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

		GD.Print(FileAccess.GetOpenError());

		var nodeData = ScoreManager.Call("Save");

		var jsonString = Json.Stringify(nodeData);

		saveGame.StoreLine(jsonString);
	}

	
}