using Godot;
using System;

[GlobalClass]
public partial class health_component : Node
{

	private Node3D ParentNode;

	[Export] public float max_health = 100f;
	public float current_health;
	
	public override void _Ready()
	{
		ParentNode = GetParent<CharacterBody3D>();
		current_health = max_health;
	}

	public void takeDamage (float damageAmount)
	{
		current_health -= damageAmount;
		GD.Print("POPIK");
		if (current_health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		ParentNode.QueueFree();
	}

	
}
