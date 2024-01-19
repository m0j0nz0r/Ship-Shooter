using Godot;
using System;

public class Enemy : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
	
	// Movement
	[Export]
	public int Speed = 400;
	private void _Move(float delta) {
		var velocityNormalized = -Transform.basis.z;
		velocityNormalized.x = 0;
		MoveAndSlide(velocityNormalized * delta * Speed);		
	}
	
	// Rotation
	private Vector3 _TargetLocation = Vector3.Zero;
	private float _RotationLerp = 0;
	private float _RotationSpeed = 0.01f;
	
	public void SetTargetLocation(Vector3 NewTarget) {
		if (_TargetLocation.IsEqualApprox(NewTarget)) {
			return;
		}
		
		_TargetLocation = NewTarget;
		_RotationLerp = 0;
	}
	private void _Rotate(float delta) {
		
		if (_RotationLerp < 1) {
			_RotationLerp += delta * _RotationSpeed;
		} else if (_RotationLerp > 1) {
			_RotationLerp = 1;
		}
		
		var target = Transform.LookingAt(_TargetLocation, Vector3.Up);
		target = Transform.InterpolateWith(target, _RotationLerp);
		LookAt(GlobalTransform.origin - target.basis.z, Vector3.Up);
		
	}
	public override void _PhysicsProcess(float delta) {
		_Rotate(delta);
		_Move(delta);
	}
	
	
}
