using Godot;
using System;

public class Main : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(float delta) {
		GetNode<Enemy>("Enemy").SetTargetLocation(GetNode<Player>("Player").GlobalTransform.origin);
	}
}
