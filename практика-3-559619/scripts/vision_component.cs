using Godot;
using System;

[GlobalClass]
public partial class vision_component : Area3D
{
	private Node3D ParentNode;

	public override void _Ready()
	{
		ParentNode = GetParent<CharacterBody3D>();
	}

	private Node3D currentTarget;
	public Node3D GetTarget()
	{
		foreach(var item in GetOverlappingBodies())
		{
			bool isAlly = ParentNode.IsInGroup("alias");
			bool isEnemy = ParentNode.IsInGroup("enemies");
			// && (!IsInstanceValid(currentTarget)) || item.Position.DistanceTo(ParentNode.Position) < currentTarget.Position.DistanceTo(ParentNode.Position)
			bool differenteTeams = (isAlly && item.IsInGroup("enemies") || isEnemy && item.IsInGroup("alias"));
			if (item != ParentNode && differenteTeams )
			{
				currentTarget = item;
			}
		}
		return currentTarget;
	}
}
