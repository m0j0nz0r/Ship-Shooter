using Godot;
using System;

public class Player : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public int Speed { get; set; } = 1400;
	
	private Vector3 _targetVelocity = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public override void _PhysicsProcess(float delta) {
		var direction = Vector3.Zero;
		
		direction.z = Input.GetAxis("move_left", "move_right");
		direction.y = Input.GetAxis("move_down", "move_up");
		_targetVelocity = direction * Speed * delta;
		
		
		MoveAndSlide(_targetVelocity);
	}
}
