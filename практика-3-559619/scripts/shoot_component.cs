using Godot;
using System;

[GlobalClass]
public partial class shoot_component : Node
{
	private CharacterBody3D ParentNode;
	public override void _Ready()
	{
		ParentNode = GetParent<CharacterBody3D>();
	}

	[Export] private vision_component vision_component;
	[Export] private PackedScene BulletScene;
	[Export] public float ShootingCooldown = 1f;
	private float CooldownProgress = 0f;
	public Node3D Target;

	public override void _Process(double delta)
	{
		if (IsInstanceValid(Target))
		{
			if (CooldownProgress <= 0)
			{
				CooldownProgress = ShootingCooldown;
				SpawnBullet();
			}
		}
		else if (vision_component != null)
		{
			Target = vision_component.GetTarget();
		}
		CooldownProgress -= (float)delta;
	}

	private void SpawnBullet()
	{
		Bullet BulletInstance = BulletScene.Instantiate<Bullet>();
		BulletInstance.Position = ParentNode.GlobalPosition;
		bool isAlly = ParentNode.IsInGroup("alias");
		if (isAlly)
		{
			BulletInstance.targetGroup = "enemies";
		}
		else
		{
			BulletInstance.targetGroup = "alias";
		}
		ParentNode.AddSibling(BulletInstance);
		if (IsInstanceValid(Target))
		{
			BulletInstance.LookAt(Target.Position);
		}
	}
}
