/* *************************************************************************
 * Club.cs    Original version: Kari Silpiö 20.3.2014 v1.0
                   Modified by: Alexandru Oat 27.11.2014 v1.0
 * -----------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Business Logic Layer
 *  Class:       Business object class describing a single Club
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;

/// <summary>
/// Club - Business object class
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: Alexandru Oat 2014</remarks>
/// </summary>
public class Club
{
    private int clubid;
    private String clubName;
    private String city;
    private String email;

    public Club()
    {
        clubid = -1;
        clubName = "";
        city = "";
        email = "";
    }

    public int Clubid
    {
        get { return clubid; }
        set { clubid = value; }
    }

    public String ClubName
    {
        get { return clubName; }
        set { clubName = value; }
    }
    
    public String City
    {
        get { return city; }
        set { city = value; }
    }
    
    public String Email
    {
        get { return email; }
        set { email = value; }
    }


}
// End