using System;

/// <summary>
/// Summary description for Brevet
/// </summary>
public class Brevet
{
    private int brevetid;
    private int distance;
    private DateTime brevetDate;
    private String location;
    private int climbing;

    public Brevet()
    {
    }

    public int Brevetid
    {
        get { return brevetid; }
        set { brevetid = value; }
    }
    public int Distance
    {
        get { return distance; }
        set { distance = value; }
    }
    public DateTime BrevetDate
    {
        get { return brevetDate; }
        set { brevetDate = value; }
    }

    public String Location
    {
        get { return location; }
        set { location = value; }
    }
    public int Climbing
    {
        get { return climbing; }
        set { climbing = value; }
    }
}