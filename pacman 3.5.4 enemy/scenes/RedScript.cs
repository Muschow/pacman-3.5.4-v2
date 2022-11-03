using Godot;
using System;

public class RedScript : GhostScript
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        speed = 100;
        Position = new Vector2(48, 48); //sets his position to be in the maze for testing
        //setspawn on node
        //OR setspawn to random location and set state to patrol, doing patrol stuff.
        //when on node, set state to dijkstras, do dijkstras for like 30 seconds or something and go back to patrol
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        ProcessPatrol();
        Move();
    }
}
