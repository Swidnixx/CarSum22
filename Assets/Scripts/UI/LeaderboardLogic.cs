using System;
using System.Collections.Generic;
using System.Linq;

public struct Car
{
    public string name;
    public int position;

    public Car(string name, int position)
    {
        this.name = name;
        this.position = position;
    }
}
public class LeaderboardLogic
{
    static int carsRegistered = -1;
    static Dictionary<int, Car> board = new Dictionary<int, Car>();

    public static void Reset()
    {
        board.Clear();
        carsRegistered = -1;
    }

    public static void SetPosition(int id, int lap, int checkpoint)
    {
        int position = lap * 1000 + checkpoint;
        board[id] = new Car(board[id].name, position);
    }

    public static int Register(string playerName)
    {
        carsRegistered++;
        board.Add(carsRegistered, new Car(playerName, 0));
        return carsRegistered;
    }

    public static List<string> GetPlaces()
    {
        List<string> places = new List<string>();
        var leaderboard = board.OrderByDescending(entry => entry.Value.position);
        foreach(var entry in leaderboard)
        {
            places.Add(entry.Value.name);
        }
        return places;
    }
}