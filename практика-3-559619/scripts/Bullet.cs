using Godot;
using System;

public partial class Bullet : Area3D
{
	[Export] public float BulletSpeed = 100f; 
	public string targetGroup = "enemies"; 

	public override void _Process(double delta)
	{
		Position += -Transform.Basis.Z * BulletSpeed * (float)delta;
	}

	public void _on_body_entered(Node3D body)
	{
		if (body.IsInGroup(targetGroup))
		{
			foreach(var child in body.GetChildren())
			{
				if (child is health_component healthComponent)
				{
					healthComponent.takeDamage(30f);
					break;
				}
			}

			QueueFree();
		}
	}
}
