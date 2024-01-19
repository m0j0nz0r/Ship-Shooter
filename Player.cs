using Godot;
using System;

public class Player : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public int Speed { get; set; } = 1400;
	
	[Export]
	public PackedScene BulletScene;
	
	[Export]
	public ulong ShootingCooldown = 150;
	private ulong _lastShotTime = 0;
	
	private Vector3 _targetVelocity = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lastShotTime = Time.GetTicksMsec();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void _ProcessMovement(float delta) {
		var direction = Vector3.Zero;
		
		direction.z = Input.GetAxis("move_left", "move_right");
		direction.y = Input.GetAxis("move_down", "move_up");
		_targetVelocity = direction * Speed * delta;
		
		MoveAndSlide(_targetVelocity);
	}
	
	private void _Shoot() {
		Bullet bullet = (Bullet)BulletScene.Instance();
		Vector3 spawnPos = GetNode<Spatial>("Pivot/BulletSpawn").GlobalTransform.origin;
		bullet.Initialize(spawnPos, spawnPos + Vector3.Back);
		GetParent().AddChild(bullet);
	}
	private void _ProcessShooting() {
		ulong currentTime = Time.GetTicksMsec();
		ulong deltaTime = currentTime - _lastShotTime;
		
		if (deltaTime < ShootingCooldown) {
			return;
		}
		
		if (Input.GetActionStrength("fire") > 0) {
			_lastShotTime = currentTime;
			_Shoot();
		}
	}

	public override void _PhysicsProcess(float delta) {
		_ProcessMovement(delta);
		_ProcessShooting();
	}
}
