using Godot;
using System;

public partial class signalbus : Node
{
    [Signal]
    public delegate void LifeLostEventHandler();
    [Signal]
    public delegate void LivesDepletedEventHandler();
    [Signal]
    public delegate void ItemCollectedEventHandler(StringName collectable);
    [Signal]
    public delegate void StateChangeEventHandler(string state);
    [Signal]
    public delegate void LeftWarpEventHandler(Node2D node);
    [Signal]
    public delegate void RightWarpEventHandler(Node2D node);

}
