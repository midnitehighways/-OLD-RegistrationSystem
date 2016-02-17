using System;
using System.Collections.Generic;
using System.Data;
using Karis.DatabaseLibrary;
/// <summary>
/// Summary description for Brevet_RiderDAO
/// </summary>
public class Brevet_RiderDAO
{
    private Database myDatabase;
    private String myConnectionString;

    public Brevet_RiderDAO()
    {
        myConnectionString = ModelCaseConnectionString.Text;
        myDatabase = new Database();
    }

    public List<Rider> GetAllBrevetParticipants(int brevetid)
    {
        List<Rider> riderList = new List<Rider>();
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT familyName, givenName, Club.clubId
                FROM Rider
                JOIN Club ON (Club.clubId = Rider.clubId)
                JOIN Brevet_Rider ON (Brevet_Rider.riderId = Rider.riderId)
                WHERE brevetId = {0}
                ORDER BY familyName, givenName", brevetid);

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                Rider rider = new Rider();

                rider.FamilyName = (String)resultSet["familyName"];
                rider.GivenName = (String)resultSet["givenName"];
                rider.Clubid = (int)resultSet["clubId"];

                riderList.Add(rider);
            }

            resultSet.Close();

            return riderList;
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
    /// Deletes a single Brevet row by BrevetId from the database.
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>
    public int DeleteBrevet(int brevetid)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            /*if (employeeExistsForBrevet(Brevetid) == true)
            {
                return 1;
            }*/

            String sqlText = String.Format(
              @"DELETE FROM Brevet
                WHERE brevetId = {0}", brevetid);

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
    /// Retrieves a single Brevet row by BrevetId from the database.
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>A single Brevet object</returns>
    public Brevet GetBrevetByBrevetId(int brevetid)
    {
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT *
                  FROM Brevet 
                 WHERE brevetId = {0}", brevetid);

            resultSet = myDatabase.ExecuteQuery(sqlText);

            if (resultSet.Read() == true)
            {
                Brevet brevet = new Brevet();

                brevet.Brevetid = (int)resultSet["brevetId"];
                brevet.Distance = (int)resultSet["distance"];
                brevet.BrevetDate = (DateTime)resultSet["brevetDate"];
                brevet.Location = (String)resultSet["location"];
                brevet.Climbing = (int)resultSet["climbing"];

                resultSet.Close();

                return brevet;
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
    /// Inserts a single Brevet row into the database.
    /// </summary>
    /// <param name="Brevet"></param>
    /// <returns>0 = OK, 1 = insert not allowed, -1 = error</returns>
    public int InsertBrevetRegistration(String username, int brevetid)
    {
        int riderid = 0;
        IDataReader resultSet;
        
        try
        {
            myDatabase.Open(myConnectionString);

            /*if (BrevetExists(brevet.Brevetid) == true)
            {
                return 1;
            }*/
            String sqlText = String.Format(
              @"SELECT riderId 
                  FROM Rider 
                 WHERE username = '{0}'", username);

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                riderid = (int)resultSet["riderId"];
                
            }
            resultSet.Close();
            myDatabase.Close();
            myDatabase.Open(myConnectionString);

            String sqlText2 = String.Format(
              @"INSERT INTO Brevet_Rider (brevetId, riderId)
                VALUES ({0}, {1})",
                brevetid,
                riderid);

            myDatabase.ExecuteUpdate(sqlText2);

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
}