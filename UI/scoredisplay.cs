using Godot;
using System;

public partial class scoredisplay : Label
{
	score_manager Score_Manager;
	public override void _Ready()
	{
		Score_Manager = GetNode<score_manager>("/root/Sceneloader/Main/Score_Manager");
		Score_Manager.ScoreChange += OnScoreChange;
	}

	public void OnScoreChange(int score)
	{
		Text = "Score: " + score;
	}

	
}
