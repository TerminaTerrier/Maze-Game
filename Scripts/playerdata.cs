using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class playerdata : Node
{
    boot Boot;
    game_manager Main;
    Node Level;
    Node2D Player;
    public static Vector2 playerPosition;


    public override void _Ready()
    {
      //Boot = GetNode<boot>("/root/boot");  
      //Boot.Boot +=  OnBoot;
      //GD.Print(Sceneloader == null);
    }
    
    public void OnBoot(Node2D main)
    {
      //Main = GetNode<game_manager>("/root/Sceneloader/Main");
      //Player = GetNode<CharacterBody2D>("/root/Sceneloader/Main/Level/Player");
      Level = main.GetNode<Node>("Level");
      Player = Level.GetNode<Node2D>("Player");

      //GD.Print(Main == null);  
      //GD.Print(Player == null);
    }
    public override void _Process(double delta)
    {
        playerPosition = Player.GlobalPosition;
       // GD.Print(playerPosition);
    }
}
