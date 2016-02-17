using System;

/// <summary>
/// Summary description for Brevet_Rider
/// </summary>
public class Brevet_Rider
{
    private int brevetid;
    private int riderid;
    private String isCompleted;
    private String finishingTime;

    public Brevet_Rider()
    {
    }

    public int Brevetid
    {
        get { return brevetid; }
        set { brevetid = value; }
    }
    public int Riderid
    {
        get { return riderid; }
        set { riderid = value; }
    }
    public String IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    public String FinishingTime
    {
        get { return finishingTime; }
        set { finishingTime = value; }
    }
}