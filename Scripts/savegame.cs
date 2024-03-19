using Godot;
using System;

public partial class savegame : Node
{
	Node ScoreManager;
	game_manager Main;
	sceneloader Sceneloader;

	public override void _Ready()
	{
		GetNode<sceneloader>("root/Sceneloader");
		
		Sceneloader.MainLoad += OnMainLoad;
	}

	public void OnMainLoad()
	{
		Main = GetNode<game_manager>("/root/Sceneloader/Main");
		ScoreManager = GetNode<Node>("/root/Sceneloader/Main/Score_Manager");
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