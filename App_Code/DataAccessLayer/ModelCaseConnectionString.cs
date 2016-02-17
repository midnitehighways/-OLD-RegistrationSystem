/* *************************************************************************
 * ModelCaseConnectionString.cs  
 * 
 * Original version: Kari Silpiö 20.3.2014 v1.0
 *                   Modified by: -
 * ------------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Data Access Layer
 *  Class:       Connection string for the database
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
/// <summary>
/// Connection string for the database
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: -</remarks>
/// </summary>
public class ModelCaseConnectionString
{
    private static string text =
        @"Data Source=(LocalDB)\v11.0;
        AttachDbFilename=|DataDirectory|\RandonneurDatabase.mdf;
        Integrated Security=True;Connect Timeout=30";

    public static string Text
    {
       get { return text; }
    }
}
// End