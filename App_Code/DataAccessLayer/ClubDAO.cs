/* *************************************************************************
 * ClubDAO.cs   Original version: Kari Silpiö 20.3.2014 v1.0
 *                    Modified by     : -
 * ------------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Data Access Layer
 *  Class:       SQL Server specific DAO class for Club entity objects
 * -------------------------------------------------------------------------
 * NOTICE: This is an over-simplified example for an introductory course. 
 * - Error processing is not robust (some error are not handled)
 * - No multi-user considerations, no transaction programming 
 * - No protection for attacks of type 'SQL injection'
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;
using System.Collections.Generic;

using System.Data;
using Karis.DatabaseLibrary;

/// <summary>
/// ClubDAO - Data Access Layer interface class. Accesses the data storage.
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: -</remarks>
/// </summary>
public class ClubDAO
{
    private Database myDatabase;
    private String myConnectionString;

    public ClubDAO()
    {
        myConnectionString = ModelCaseConnectionString.Text;
        myDatabase = new Database();
    }

    public int GetNextAvailableId()
    {
        int nextAvailableId = 0;
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT MAX (clubId) AS 'max_id'
                  FROM Club";

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                nextAvailableId = (int)resultSet["max_id"];
            }

            resultSet.Close();

            return nextAvailableId + 1;
        }
        catch (Exception)
        {
            return 0; // An error occured
        }
        finally
        {
            myDatabase.Close();
        }
    }
    
    /// <summary>
    /// Deletes a single Club row by ClubId from the database.
    /// </summary>
    /// <param name="ClubId"></param>
    /// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>    
    public int DeleteClub(int clubid)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (RiderExistsForClub(clubid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"DELETE FROM Club
                 WHERE clubId = {0}", clubid);

            myDatabase.ExecuteUpdate(sqlText);

            return 0;   // OK
        }
        catch (Exception)
        {
            return -1; // An error occurred
        }
        finally
        {
            myDatabase.Close();
        }
    }

    /// <summary>
    /// Retrieves all Club rows in alphabetical order by Club name from the database.
    /// </summary>
    /// <returns>A List of Clubs</returns>
    public List<Club> GetAllClubsOrderedByName()
    {
        List<Club> ClubList = new List<Club>();
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = 
              @"SELECT clubId, clubName, city, email
                  FROM Club 
              ORDER BY clubName";

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                Club Club = new Club();

                Club.Clubid = (int)resultSet["clubId"];
                Club.ClubName = (String)resultSet["clubName"];
                Club.City = (String)resultSet["city"];
                Club.Email = (String)resultSet["email"];

                ClubList.Add(Club);
            }

            resultSet.Close();

            return ClubList;
        }
        catch (Exception)
        {
            return null; // An error occured
        }
        finally
        {
            myDatabase.Close();
        }
    }

    /// <summary>
    /// Retrieves a single Club row by ClubId from the database.
    /// </summary>
    /// <param name="ClubId"></param>
    /// <returns>A single Club object</returns>
    public Club GetClubByClubId(int clubid)
    {
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT clubId, clubName, city, email  
                  FROM Club 
                 WHERE clubId = {0}", clubid);

            resultSet = myDatabase.ExecuteQuery(sqlText);

            if (resultSet.Read() == true)
            {
                Club Club = new Club();

                Club.Clubid = (int)resultSet["clubId"];
                Club.ClubName = (String)resultSet["clubName"];
                Club.City = (String)resultSet["city"];
                Club.Email = (String)resultSet["email"];
                resultSet.Close();

                return Club;
            }
            else
            {
                return null; // Not found
            }
        }
        catch (Exception)
        {
            return null;  // An error occurred
        }
        finally
        {
            myDatabase.Close();
        }
    }

    /// <summary>
    /// Inserts a single Club row into the database.
    /// </summary>
    /// <param name="Club"></param>
    /// <returns>0 = OK, 1 = insert not allowed, -1 = error</returns>
    public int InsertClub(Club Club)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (ClubExists(Club.Clubid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"INSERT INTO Club (clubId, clubName, city, email)
                VALUES ({0}, '{1}', '{2}', '{3}')",
                Club.Clubid,
                Club.ClubName, 
                Club.City,
                Club.Email);

            myDatabase.ExecuteUpdate(sqlText);

            return 0;  // OK
        }
        catch (Exception)
        {
            return -1; // An error occurred
        }
        finally
        {
            myDatabase.Close();
        }
    }

    /// <summary>
    /// Updates an existing Club row in the database.
    /// </summary>
    /// <param name="Club"></param>
    /// <returns>0 = OK, -1 = error</returns>
    public int UpdateClub(Club Club)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"UPDATE Club
                SET clubName  = '{0}', 
                    city      = '{1}',
                    email     = '{2}'
                WHERE clubId  =  {3}",
                Club.ClubName,
                Club.City,
                Club.Email,
                Club.Clubid);

            myDatabase.ExecuteUpdate(sqlText);

            return 0;  // OK
        }
        catch (Exception)
        {
            return -1; // An error occurred
        }
        finally
        {
            myDatabase.Close();
        }
    }

    /// <summary>
    /// Checks if a Club row with the given Club id exists in the database.
    /// </summary>
    /// <param name="clubId"></param>
    /// <returns>true = row exists, otherwise false</returns>
    private bool ClubExists(int clubid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT clubId 
              FROM Club 
             WHERE clubId = {0}", clubid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

    /// <summary>
    /// Checks if the Club row is being referenced by another row. 
    /// </summary>
    /// <param name="clubId"></param>
    /// <returns>true = a child row exists, otherwise false</returns>
    private bool RiderExistsForClub(int clubid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT riderId
              FROM Rider
             WHERE clubId = {0}", clubid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }
}
// End
