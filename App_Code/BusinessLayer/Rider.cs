/* *************************************************************************
 * Rider.cs    Original version: Kari Silpiö 20.3.2014 v1.0
                   Modified by: Alexandru Oat 27.11.2014 v1.0
 * -----------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Business Logic Layer
 *  Class:       Business object class describing a single Rider
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;

/// <summary>
/// Rider - Business object class
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: Alexandru Oat</remarks>
/// </summary>
public class Rider
{
    private int riderid;
    private String familyName;
    private String givenName;
    private String gender;
    private String phone;
    private String email;
    private int clubid;
    private String username;
    private String password;
    private String role;

    public Rider()
    {
    }

    public int Riderid
    {
        get { return riderid; }
        set { riderid = value; }
    }

    public String FamilyName
    {
        get { return familyName; }
        set { familyName = value; }
    }

    public String GivenName
    {
        get { return givenName; }
        set { givenName = value; }
    }

    public String Gender
    {
        get { return gender; }
        set { gender = value; }
    }

    public String Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public String Email
    {
        get { return email; }
        set { email = value; }
    }

    public int Clubid
    {
        get { return clubid; }
        set { clubid = value; }
    }

    public String Username
    {
        get { return username; }
        set { username = value; }
    }

    public String Password
    {
        get { return password; }
        set { password = value; }
    }

    public String Role
    {
        get { return role; }
        set { role = value; }
    }
}
// End