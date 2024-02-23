using Godot;
using System;

public partial class playerdata : Node
{
    Node main;
    Node level;
    Node2D player;
    public static Vector2 playerPosition;


    public override void _Ready()
    {
        main = GetTree().GetFirstNodeInGroup("main");
        player = (Node2D)main.GetTree().GetFirstNodeInGroup("player");
    }

    public override void _Process(double delta)
    {
  
        playerPosition = player.GlobalPosition;
       // GD.Print(playerPosition);
    }
}
