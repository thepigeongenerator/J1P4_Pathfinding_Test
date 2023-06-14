﻿using PathfindingDemo.Gameplay.Enviroment;
using PathfindingDemo.Gameplay.Util;

namespace PathfindingDemo.Gameplay.Entities;

internal abstract class Entity : GameBehaviour
{
    public Position Position { get; set; }
    public ConsoleColor Color { get; protected init; }
    protected World World { get; }

    /// <summary>
    /// create an entity object with the start position
    /// </summary>
    public Entity(World _world, Position _position)
    {
        Color = ConsoleColor.Gray; //set the default entity colour to gray

        //set the starting variables
        World = _world;
        Position = _position;
    }

    /// <summary>
    /// sets <see cref="Position"/> to that plus <paramref name="_direction"/>,<br/>
    /// if this path encounters a wall the position is set to the space before
    /// </summary>
    protected void Move(Position _direction)
    {
        float m;
        int x1, x2, y1, y2;
        Position newPos, endPos;

        //calculate the new position
        newPos = Position + _direction;

        //set the end position to the current position
        endPos = Position;

        //set all point variables
        x1 = Position.X;
        y1 = Position.Y;
        x2 = newPos.X;
        y2 = newPos.Y;

        //get m (slope)
        m = (y2 - y1) / (x2 - x1);

        //loop through x
        for (int x = x1; x < x2; x++)
        {
            int y = (int)Math.Floor(m * (x - x1));

            //if (x, y) is a wall
            if (World.IsWall((x, y)))
                break; //break the loop as we have our answer

            endPos = new Position(x, y);
        }

        //set the position to the final position
        Position = endPos;
    }
}
