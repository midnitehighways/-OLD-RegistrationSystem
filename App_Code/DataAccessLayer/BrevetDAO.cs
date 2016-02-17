using System;
using System.Collections.Generic;
using System.Data;
using Karis.DatabaseLibrary;
/// <summary>
/// Summary description for BrevetDAO
/// </summary>
public class BrevetDAO
{
    private Database myDatabase;
    private String myConnectionString;

    public BrevetDAO()
    {
        myConnectionString = ModelCaseConnectionString.Text;
        myDatabase = new Database();
    }

    public List<Brevet> GetAllBrevetsOrderedById()
    {
        List<Brevet> brevetList = new List<Brevet>();
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT * 
                  FROM Brevet 
              ORDER BY distance, location";

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                Brevet Brevet = new Brevet();

                Brevet.Brevetid = (int)resultSet["brevetId"];
                Brevet.Distance = (int)resultSet["distance"];
                Brevet.BrevetDate = (DateTime)resultSet["brevetDate"];
                Brevet.Location = (String)resultSet["location"];
                Brevet.Climbing = (int)resultSet["climbing"];

                brevetList.Add(Brevet);
            }

            resultSet.Close();

            return brevetList;
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

    public int GetNextAvailableId()
    {
        int nextAvailableId = 0;
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT MAX (brevetId) AS 'max_id'
                  FROM Brevet";

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
    /// Deletes a single Brevet row by BrevetId from the database.
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>
    public int DeleteBrevet(int brevetid)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (RiderExistsForBrevet(brevetid) == true)
            {
                return 1;
            }

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
    public int InsertBrevet(Brevet brevet)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (BrevetExists(brevet.Brevetid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"INSERT INTO Brevet (brevetId, distance, brevetDate, location, climbing)
                VALUES ({0}, {1}, '{2}', '{3}', {4})",
                brevet.Brevetid,
                brevet.Distance,
                brevet.BrevetDate,
                brevet.Location,
                brevet.Climbing);

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
    /// Updates an existing Brevet row in the database.
    /// </summary>
    /// <param name="Brevet"></param>
    /// <returns>0 = OK, -1 = error</returns>
    public int UpdateBrevet(Brevet brevet)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"UPDATE Brevet
                SET distance        = {0},
                    brevetDate      = '{1}',
                    location        = '{2}',
                    climbing        = {3} 
                WHERE brevetId      = {4}",
                brevet.Distance,
                brevet.BrevetDate,
                brevet.Location,
                brevet.Climbing,
                brevet.Brevetid);

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
    /// Checks if a Brevet row with the given Brevet id exists in the database.
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>true = row exists, otherwise false</returns>
    private bool BrevetExists(int brevetid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT brevetId 
              FROM Brevet 
             WHERE brevetId = {0}", brevetid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

    /// <summary>
    /// Checks if the Brevet row is being referenced by another row. 
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>true = a child row exists, otherwise false</returns>
    private bool RiderExistsForBrevet(int brevetid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT riderId
              FROM Brevet_Rider
             WHERE brevetId = {0}", brevetid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

}