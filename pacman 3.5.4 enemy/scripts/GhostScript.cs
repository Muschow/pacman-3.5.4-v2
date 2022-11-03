using Godot;
using System;

public class GhostScript : CharacterScript
{
    protected AnimatedSprite ghostEyes;

    protected enum states
    {
        patrol,
        chase,
        scatter
    }
    protected states ghostState = states.patrol; //initialise ghost state to patrol mode
    //in the ready function put EnterState(patrol)
    protected void EnterState(states passState)
    {
        if (ghostState != passState)
        {
            LeaveState(ghostState);
            ghostState = passState;
        }
    }

    protected void ProcessStates()
    {
        if (ghostState == states.patrol)
        {
            ProcessPatrol();
        }
        else if (ghostState == states.chase)
        {
            ProcessChase();
        }
        else if (ghostState == states.scatter)
        {
            ProcessScatter();
        }

    }

    protected void LeaveState(states passState)
    {
        //use this to reset any values
    }

    protected void ProcessChase()
    {

    }

    protected void ProcessPatrol()
    {
        //if on a node
        //choose a random direction that is not a wall
        //go that way infinitely until you reach another node
        const int wall = 1;
        Vector2[] directions = new Vector2[] { Vector2.Down, Vector2.Left, Vector2.Right, Vector2.Up };
        TileMap mazeTm = GetNode<TileMap>("/root/Game/MazeContainer/Maze/MazeTilemap");
        int[,] adjMatrix = (int[,])mazeTm.Get("adjMatrix");


        // if (IfOnNode())
        // {
        //     int randindex = (int)GD.RandRange(0, directions.Length);

        //     while (mazeTm.GetCellv(Position + (directions[randindex] * 32)) == wall)
        //     {
        //         randindex = (int)GD.RandRange(0, directions.Length);
        //     }
        //     moveDir = directions[randindex];


        // }

    }

    protected void ProcessScatter()
    {

    }
    protected override void MoveAnimManager(Vector2 masVector)
    {
        //animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite"); //not sure whether to put it in here for readabillity or in each ready so theres less calls
        //ghostEyes = GetNode<AnimatedSprite>("GhostEyes"); //not sure whether to put it in here for readabillity or in each ready so theres less calls
        masVector = masVector.Normalized();
        if (masVector == Vector2.Up)
        {
            ghostEyes.Play("up");
        }
        else if (masVector == Vector2.Down)
        {
            ghostEyes.Play("down");
        }
        else if (masVector == Vector2.Right)
        {
            ghostEyes.Play("right");
        }
        else if (masVector == Vector2.Left)
        {
            ghostEyes.Play("left");
        }
    }

    protected virtual bool IfOnNode()
    {
        const int node = 0; //this should really be global or something idk
        TileMap nodeTm = GetNode<TileMap>("/root/Game/MazeContainer/Maze/NodeTilemap");
        if (nodeTm.GetCellv(Position) == node)
        {
            return true;
        }
        else
        {
            return false;
        }
    }






    //As GhostScript is a base class, it will not be in the scene tree so ready and process are not needed
    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {

    // }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
