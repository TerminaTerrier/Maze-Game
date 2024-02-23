using Godot;
using System;

public partial class savegame : Node
{
	Node ScoreManager;
	game_manager Main;

	public override void _Ready()
	{
		Main = GetNode<game_manager>("/root/Main");
		ScoreManager = GetNode<Node>("/root/Main/Score_Manager");

		Main.SaveData += OnSaveData;
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