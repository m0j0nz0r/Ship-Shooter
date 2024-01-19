using Godot;
using System;

public class Bullet : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public int Speed = 3000;
	
	private Vector3 _velocity = Vector3.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void Initialize(Vector3 position, Vector3 target) {
		_velocity = (target - position).Normalized() * Speed;	
		LookAtFromPosition(position, target, Vector3.Up);
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	public override void _PhysicsProcess(float delta) {
		MoveAndSlide(_velocity * delta);
	}

	private void OnVisibilityNotifierScreenExited()
	{
		QueueFree();
	}
}
