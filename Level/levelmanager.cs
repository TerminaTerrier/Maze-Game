using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Godot.Collections;


public partial class levelmanager : Node
{
	static int pelletCount;
	[Export]
	TileMap tilemap;
	//using Godot's arrays here because C# arrrays and list were not compatible with the tilemap methods

	Array<Vector2I> pelletTiles;
	Array<Vector2> pelletTilesV2 = new Array<Vector2> { };
	Array<Vector2I> powerTiles;
	Array<Vector2> powerTilesV2 = new Array<Vector2> { };
	Vector2I specialTile;
	PackedScene pellet = GD.Load<PackedScene>("res://Collectables/pellet.tscn");
	PackedScene powerUp = GD.Load<PackedScene>("res://Collectables/power_up.tscn");
	PackedScene special = GD.Load<PackedScene>("res://Collectables/special_collectable.tscn");
	int pelletBookmark = 395;
	signalbus SignalBus;


	//I can probably refactor a lot of this code using return methods to make it cleaner
	public override void _Ready()
	{
		Vector2I atlasCoordPellets = new Vector2I(3, 1);
		Vector2I atlasCoordPowerUp = new Vector2I(0, 3);
		pelletTiles = tilemap.GetUsedCellsById(0, 1, atlasCoordPellets);
		powerTiles = tilemap.GetUsedCellsById(0, 1, atlasCoordPowerUp);

		specialTile = new Vector2I(568, 311);

		

		ConvertArray();
		CreatePellets();
		CreatePowerUp();

		SignalBus = GetNode<signalbus>("/root/Sceneloader/Main/SignalBus");
		SignalBus.ItemCollected += OnItemCollected;
	}

	private void ConvertArray()
	{
		foreach (var tile in pelletTiles)
		{
			pelletTilesV2.Add(tilemap.MapToLocal(tile));
		}

		foreach (var tile in powerTiles)
		{
			powerTilesV2.Add(tilemap.MapToLocal(tile));
		}
		//GD.Print(backgroundTilesV2);
	}

	//instances not being added to group?
	//need to prevent pellet from being instanced where the player spawns
	private void CreatePellets()
	{
		foreach (var tile in pelletTilesV2)
		{
			//GD.Print("hi!");
			Node2D pelletInstance = (Node2D)pellet.Instantiate();
			pelletInstance.Position = tile;
			AddChild(pelletInstance);
			pelletInstance.AddToGroup("Collectables");
			pelletCount++;


		}
		GD.Print(pelletCount);
		
	}

	private void CreatePowerUp()
	{
		foreach (var tile in powerTilesV2)
		{
			Node2D powerUpInstance = (Node2D)powerUp.Instantiate();
			powerUpInstance.Position = tile;
			AddChild(powerUpInstance);
			powerUpInstance.AddToGroup("Collectables");

			var checkGroup = GetTree().GetNodesInGroup("Collectables");
			if (checkGroup != null)
			{
				//GD.Print("nodes are in group!");
				GD.Print(checkGroup.Count);
			}
		}


	}
	
	
	
	public void OnItemCollected(StringName collectable)
	{
		if(collectable == "pellet")
		{
			pelletCount--;
			SpecialTracker();
			LevelClearTracker();
		}
		GD.Print(pelletCount);
	}

	private void SpecialTracker()
	{
		if(pelletCount <= pelletBookmark - 70)
		{
			Node2D specialInstance = (Node2D)special.Instantiate();
			specialInstance.Position = specialTile;
			CallDeferred("add_child", specialInstance);
			specialInstance.AddToGroup("Collectables");

			pelletBookmark = pelletBookmark - 70;
		}
	}
	public void LevelClearTracker()
	{
		if(pelletCount == 0)
		{
			SignalBus.EmitSignal(signalbus.SignalName.LevelClear);
		}
	}

	public void _on_level_warp_left_body_entered(Node2D body)
	{
		SignalBus.EmitSignal(signalbus.SignalName.LeftWarp, body);
	}

	public void _on_level_warp_right_body_entered(Node2D body)
	{
		SignalBus.EmitSignal(signalbus.SignalName.RightWarp, body);
	}

}
