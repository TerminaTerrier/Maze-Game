using Godot;
using System;

public partial class playerdata : Node
{
    sceneloader Sceneloader;
    game_manager Main;
    Node2D Player;
    public static Vector2 playerPosition;


    public override void _Ready()
    {
      Sceneloader = GetNode<sceneloader>("/root/Sceneloader");  
      Sceneloader.MainLoad +=  OnMainLoad;
      
    }

    public void OnMainLoad()
    {
      Main = GetNode<game_manager>("/root/Sceneloader/Main");
      Player = GetNode<Node2D>("/root/Sceneloader/Main/Level/Player");

      Main.LoadScene += OnMainReload;
    }
    public void OnMainReload()
    {
        Player = GetNode<Node2D>("/root/Sceneloader/Main/Level/Player");

    }
    public override void _Process(double delta)
    {
        playerPosition = Player.GlobalPosition;
       // GD.Print(playerPosition);
    }
}
