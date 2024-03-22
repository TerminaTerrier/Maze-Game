using Godot;
using System;

public partial class highscoredisplay : Label
{
	score_manager Score_Manager;
	public override void _Ready()
	{
		Score_Manager = GetNode<score_manager>("/root/Sceneloader/Main/Score_Manager");
		Score_Manager.HighScoreChange += OnHighScoreChange;
	}

	public void OnHighScoreChange(int highScore)
	{
		Text = "High Score: " + highScore;
	}
}
