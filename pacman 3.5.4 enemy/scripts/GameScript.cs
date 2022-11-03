using Godot;
using System;

public class GameScript : Node2D
{
    private int mazeStartLoc = 0;
    private int mazeHeight;
    private int mazeWidth;
    private int mazesOnTheScreen = 0; //this is literally used for 1 thing to make the walls black on the bottom of the first maze. Probably a better way to do this...
    private TileMap mazeTm;
    private Node2D mazeCon;
    private KinematicBody2D pacman;
    private Node mazeInstance;
    PackedScene mazeScene = GD.Load<PackedScene>("res://scenes/Maze.tscn");

    // Called when the node enters the scene tree for the first time.
    private void InstanceAndRemoveMazes()
    {
        //store all the getnodes, MazeGenerator mazeG = new MazeGenerator() stuff in here instead of in the ready function for better encapsulation


        mazeStartLoc -= (mazeHeight - 1);

        mazeInstance = mazeScene.Instance();
        mazeCon.AddChild(mazeInstance, true);

        mazesOnTheScreen++;
        GD.Print("instanced maze!");

        if (mazeCon.GetChildCount() > 3) //if more than 3 mazes, remove maze
        {
            mazeCon.GetChild(0).QueueFree();
            mazesOnTheScreen--;
            GD.Print("removed maze!");
        }
        //PrintTreePretty();

        //REMEMBER! This prnt tree is literally lying, if you change the pacman camera to 2x9, you can clearly see only 3 mazes are spawned in at a time!
        //I love how godot mono is trash and buggy and a broken mess :)
        //GD.Print("mazesOnTheScreen: " + mazesOnTheScreen); //yeah so this is always on 3 pretty much so idk whats going on lol

    }

    public override void _Ready()
    {
        mazeCon = GetNode<Node2D>("/root/Game/MazeContainer");

        mazeTm = GetNode<TileMap>("/root/Game/MazeContainer/Maze/MazeTilemap"); //put all the maze crap in a mazeInit function

        mazeHeight = (int)mazeTm.Get("height"); //dependancy
        mazeWidth = (int)mazeTm.Get("width"); //dependency


        mazesOnTheScreen++; //remove this if you instance the first maze

        pacman = GetNode<KinematicBody2D>("/root/Game/Pacman"); // res://scenes/Pacman.tscn


        //PrintTreePretty(); //debug, also you cant really trust this for instances anyway so...
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Math.Floor(pacman.Position.y / 32) == mazeStartLoc + mazeHeight - 2)
        {
            InstanceAndRemoveMazes();
        }

    }
}
