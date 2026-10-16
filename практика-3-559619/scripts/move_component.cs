using Godot;
using System;

[GlobalClass]
public partial class move_component : Node
{
	private CharacterBody3D ParentNode;
	public override void _Ready()
	{
		ParentNode = GetParent<CharacterBody3D>();
	}

	public override void _Process(double delta)
	{
		Move(delta);
	}

	[Export] public float MoveSpeed = 100f;
	[Export] public float AttackDistance = 1f;
	[Export] private vision_component vision_component;
	public Node3D Target;

	public void Move(double delta)
	{
		Vector3 targetPosition = ParentNode.Position;

		if (IsInstanceValid(Target))
		{
			targetPosition = new Vector3(Target.Position.X, ParentNode.Position.Y, Target.Position.Z);
		}
		else if (vision_component != null)
		{
			Target = vision_component.GetTarget();
			targetPosition = new Vector3(0, Target.Position.Y, 0);
		}
		else 
		{
			targetPosition = new Vector3(0, Target.Position.Y, 0);
		}
		ParentNode.Velocity = (targetPosition - ParentNode.Position).Normalized() * MoveSpeed * (float)delta;
		if (ParentNode.Position.DistanceTo(targetPosition) > AttackDistance)
		{
			ParentNode.MoveAndSlide();
		}
	}
}
