using System;
using System.Collections.Generic;

// ---------------- Direction Strategy ---------------- //
public abstract class Direction
{
    public abstract (int, int) Move(int x, int y);
    public abstract Direction Left();
    public abstract Direction Right();
    public abstract string Name();
}

public class North : Direction
{
    public override (int, int) Move(int x, int y) => (x, y + 1);
    public override Direction Left() => new West();
    public override Direction Right() => new East();
    public override string Name() => "N";
}

public class South : Direction
{
    public override (int, int) Move(int x, int y) => (x, y - 1);
    public override Direction Left() => new East();
    public override Direction Right() => new West();
    public override string Name() => "S";
}

public class East : Direction
{
    public override (int, int) Move(int x, int y) => (x + 1, y);
    public override Direction Left() => new North();
    public override Direction Right() => new South();
    public override string Name() => "E";
}

public class West : Direction
{
    public override (int, int) Move(int x, int y) => (x - 1, y);
    public override Direction Left() => new South();
    public override Direction Right() => new North();
    public override string Name() => "W";
}

// ---------------- Composite Pattern for Grid ---------------- //
public abstract class GridComponent
{
    public abstract bool IsObstacle();
}

public class Cell : GridComponent
{
    public override bool IsObstacle() => false;
}

public class Obstacle : GridComponent
{
    public override bool IsObstacle() => true;
}

public class Grid
{
    private readonly int width;
    private readonly int height;
    private readonly Dictionary<(int, int), GridComponent> cells;

    public Grid(int width, int height, List<(int, int)> obstacles = null)
    {
        this.width = width;
        this.height = height;
        cells = new Dictionary<(int, int), GridComponent>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[(x, y)] = new Cell();
            }
        }

        if (obstacles != null)
        {
            foreach (var (ox, oy) in obstacles)
            {
                cells[(ox, oy)] = new Obstacle();
            }
        }
    }

    public bool IsWithinBounds(int x, int y) => (x >= 0 && x < width && y >= 0 && y < height);

    public bool IsObstacle(int x, int y) => cells[(x, y)].IsObstacle();
}

// ---------------- Command Pattern ---------------- //
public abstract class Command
{
    public abstract void Execute(Rover rover);
}

public class MoveCommand : Command
{
    public override void Execute(Rover rover) => rover.Move();
}

public class LeftCommand : Command
{
    public override void Execute(Rover rover) => rover.TurnLeft();
}

public class RightCommand : Command
{
    public override void Execute(Rover rover) => rover.TurnRight();
}

// ---------------- Rover ---------------- //
public class Rover
{
    private int x;
    private int y;
    private Direction direction;
    private readonly Grid grid;

    public Rover(int x, int y, Direction direction, Grid grid)
    {
        this.x = x;
        this.y = y;
        this.direction = direction;
        this.grid = grid;
    }

    public void Move()
    {
        var (newX, newY) = direction.Move(x, y);
        if (!grid.IsWithinBounds(newX, newY))
        {
            Console.WriteLine("Move blocked: Out of bounds!");
            return;
        }
        if (grid.IsObstacle(newX, newY))
        {
            Console.WriteLine($"Move blocked: Obstacle at ({newX}, {newY})!");
            return;
        }
        x = newX; y = newY;
    }

    public void TurnLeft() => direction = direction.Left();
    public void TurnRight() => direction = direction.Right();

    public string Report() => $"Rover is at ({x}, {y}) facing {direction.Name()}.";
}

// ---------------- Command Factory ---------------- //
public static class CommandFactory
{
    private static readonly Dictionary<char, Func<Command>> mapping = new()
    {
        { 'M', () => new MoveCommand() },
        { 'L', () => new LeftCommand() },
        { 'R', () => new RightCommand() }
    };

    public static Command Create(char commandChar) => mapping[commandChar]();
}

// ---------------- Program ---------------- //
class Program
{
    static void Main()
    {
        Console.Write("Enter grid width: ");
        int width = int.Parse(Console.ReadLine());

        Console.Write("Enter grid height: ");
        int height = int.Parse(Console.ReadLine());

        Console.Write("Enter number of obstacles: ");
        int obsCount = int.Parse(Console.ReadLine());

        var obstacles = new List<(int, int)>();
        for (int i = 0; i < obsCount; i++)
        {
            Console.Write($"Enter obstacle {i + 1} position (x y): ");
            var parts = Console.ReadLine().Split();
            obstacles.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        var grid = new Grid(width, height, obstacles);

        Console.Write("Enter starting position (x y): ");
        var startParts = Console.ReadLine().Split();
        int startX = int.Parse(startParts[0]);
        int startY = int.Parse(startParts[1]);

        Console.Write("Enter starting direction (N/E/S/W): ");
        string dirChar = Console.ReadLine().ToUpper();

        Direction direction = dirChar switch
        {
            "N" => new North(),
            "E" => new East(),
            "S" => new South(),
            "W" => new West(),
            _ => throw new ArgumentException("Invalid direction")
        };

        var rover = new Rover(startX, startY, direction, grid);

        Console.Write("Enter commands (M=Move, L=Left, R=Right): ");
        string moves = Console.ReadLine().ToUpper();

        foreach (char c in moves)
        {
            if (CommandFactory.Create(c) is Command cmd)
            {
                cmd.Execute(rover);
            }
            else
            {
                Console.WriteLine($"Invalid command: {c}");
            }
        }

        Console.WriteLine(rover.Report());
    }
}
