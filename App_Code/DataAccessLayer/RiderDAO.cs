using System;
using System.Collections.Generic;
using System.Data;
using Karis.DatabaseLibrary;
/// <summary>
/// Contains database access operations regarding the Rider
/// </summary>
public class RiderDAO
{
    private Database myDatabase;
    private String myConnectionString;

    public RiderDAO()
    {
        myConnectionString = ModelCaseConnectionString.Text;
        myDatabase = new Database();
    }

    /// <summary>
    /// Retrieves all Rider rows in alphabetical order by Rider name from the database.
    /// </summary>
    /// <returns>A List of Riders</returns>
    public List<Rider> GetAllRidersOrderedByName()
    {
        List<Rider> riderList = new List<Rider>();
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT riderId, familyName, givenName
                  FROM Rider 
              ORDER BY familyName, givenName";

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {
                Rider rider = new Rider();

                rider.Riderid = (int)resultSet["riderId"];
                rider.FamilyName = (String)resultSet["familyName"];
                rider.GivenName = (String)resultSet["givenName"];
                //rider.Gender = (String)resultSet["gender"];
                //rider.Phone = (String)resultSet["phone"];
                //rider.Email = (String)resultSet["email"];
                //rider.Clubid = (int)resultSet["clubId"];
                //rider.Username = (String)resultSet["username"];
                //rider.Password = (String)resultSet["password"];
                //rider.Role = (String)resultSet["role"];

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

    public int GetNextAvailableId()
    {
        int nextAvailableId = 0;
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT MAX (riderId) AS 'max_id'
                  FROM Rider";

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
    /// Deletes a single Rider row by RiderId from the database.
    /// </summary>
    /// <param name="RiderId"></param>
    /// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>
    public int DeleteRider(int riderid)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (brevetExistsForRider(riderid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"DELETE FROM Rider
                WHERE riderId = {0}", riderid);

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
    /// Retrieves a single Rider row by RiderId from the database.
    /// </summary>
    /// <param name="RiderId"></param>
    /// <returns>A single Rider object</returns>
    public Rider GetRiderByRiderId(int riderid)
    {
        IDataReader resultSet;

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT *
                  FROM Rider 
                 WHERE riderId = {0}", riderid);

            resultSet = myDatabase.ExecuteQuery(sqlText);

            if (resultSet.Read() == true)
            {
                Rider rider = new Rider();

                rider.Riderid = (int)resultSet["riderId"];
                rider.FamilyName = (String)resultSet["familyName"];
                rider.GivenName = (String)resultSet["givenName"];
                rider.Gender = (String)resultSet["gender"];
                rider.Phone = (String)resultSet["phone"];
                rider.Email = (String)resultSet["email"];
                rider.Clubid = (int)resultSet["clubId"];
                rider.Username = (String)resultSet["username"];
                rider.Password = (String)resultSet["password"];
                rider.Role = (String)resultSet["role"];
                
                resultSet.Close();

                return rider;
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
    /// Inserts a single Rider row into the database.
    /// </summary>
    /// <param name="Rider"></param>
    /// <returns>0 = OK, 1 = insert not allowed, -1 = error</returns>
    public int InsertRider(Rider rider)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (RiderExists(rider.Riderid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"INSERT INTO Rider (riderId, familyName, givenName, gender, phone, email, clubId, username, password, role)
                VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}')",

                rider.Riderid,
                rider.FamilyName,
                rider.GivenName,
                rider.Gender,
                rider.Phone,
                rider.Email,
                rider.Clubid,
                rider.Username,
                rider.Password,
                rider.Role);

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
    /// Updates an existing Rider row in the database.
    /// </summary>
    /// <param name="Rider"></param>
    /// <returns>0 = OK, -1 = error</returns>
    public int UpdateRider(Rider rider)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"UPDATE Rider
                SET familyName      = '{0}',
                    givenName       = '{1}',
                    gender          = '{2}',
                    phone           = '{3}',
                    email           = '{4}',
                    clubId          = {5},
                    username        = '{6}',
                    password        = '{7}',
                    role            = '{8}' 
                WHERE riderId       =  {9}",
                rider.FamilyName,
                rider.GivenName,
                rider.Gender,
                rider.Phone,
                rider.Email,
                rider.Clubid,
                rider.Username,
                rider.Password,
                rider.Role,
                rider.Riderid);

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
    /// Checks if a Rider row with the given Rider id exists in the database.
    /// </summary>
    /// <param name="RiderId"></param>
    /// <returns>true = row exists, otherwise false</returns>
    private bool RiderExists(int riderid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT riderId 
              FROM Rider 
             WHERE riderId = {0}", riderid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

    /// <summary>
    /// Checks if the Rider row is being referenced by another row. 
    /// </summary>
    /// <param name="RiderId"></param>
    /// <returns>true = a child row exists, otherwise false</returns>
    private bool brevetExistsForRider(int riderid)
    {
        IDataReader resultSet;
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT brevetId
              FROM Brevet_Rider
             WHERE riderId = {0}", riderid);

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }
}
