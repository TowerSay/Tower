using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;

#if UNITY_ANDROID
using Mono.Data.Sqlite;
#else
using Mono.Data.Sqlite;
#endif

public static class DbAccess 
{
	private static string connection;
	private static IDbConnection dbcon;
	private static IDbCommand dbcmd;
	private static IDataReader reader;
	private static StringBuilder builder;

	private static bool was_open=false;

	/// <summary>
	/// 打开数据库
	/// </summary>
	public static void OpenDB(string path)
	{
		string filepath=Game.DataPath("Access/"+path,true);

		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);

		try
		{
			dbcon = new SqliteConnection(connection);
			dbcon.Open();
			Debug.Log("Succeed!");
		}
		catch (Exception e)
		{
			return ;
			Debug.Log("Failure!:"+e.Message);
		}


		was_open=true;
	}
	
	/// <summary>
	/// 关闭数据库
	/// </summary>
	public static void CloseDB()
	{
		if(reader!=null){reader.Close(); reader = null;}
		if(dbcmd!=null){dbcmd.Dispose();dbcmd = null;}
		if(dbcon!=null){dbcon.Close();dbcon = null;}
		was_open=false;
	}

	/// <summary>开始事务</summary>
	public static IDbTransaction BeginTransaction()
	{
		return dbcon.BeginTransaction();
	}

	/// <summary>提交事务</summary>
	public static void Commit(IDbTransaction trans)
	{
		try
		{
			trans.Commit();
			Debug.Log("Both records are written to database.");
		}
		catch (Exception ex)
		{
			Debug.Log("Commit Exception Type: {0}\n"+ ex.GetType());
			Debug.Log("  Message: {0}\n"+ex.Message);
			try
			{
				trans.Rollback();
			}
			catch (Exception ex2)
			{
				Debug.Log("Rollback Exception Type: {0}" + ex2.GetType());
				Debug.Log("  Message: {0}" + ex2.Message);
			}
		}
	}

	/// <summary>基本查询</summary>
	public static IDataReader SQL(string query)
	{
		if(!was_open)return null;
		dbcmd = dbcon.CreateCommand();	// create empty command
		dbcmd.CommandText = query;		// fill the command
		reader = dbcmd.ExecuteReader();	// execute command which returns a reader
		return reader;
	}

	/// <summary>创建表</summary>
	public static bool CreateTable(string name,string[] col, string[] colType)
	{ 						// Create a table, name, column array, column type array
		string query;
		query  = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
		for(int i=1; i< col.Length; i++)
		{
			query += ", " + col[i] + " " + colType[i];
		}
		query += ")";
		try
		{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e)
		{
			Debug.Log(e);
			return false;
		}
		return true;
	}

	/// <summary>插入一条记录</summary>
	public static int InsertIntoSingle(string tableName, string colName , string value )
	{
		string query;
		query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
		try
		{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e)
		{
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public static int InsertIntoSpecific(string tableName, string[] col, string[] values)
	{ // Specific insert with col and values
		string query;

		query = "INSERT INTO " + tableName + "(" + col[0];
		for(int i=1; i< col.Length; i++)
		{
			query += ", " + col[i];
		}
		query += ") VALUES (" + values[0];

		for(int i=1; i< values.Length; i++)
		{
			query += ", " + values[i];
		}
		query += ")";

		Debug.Log(query);

		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e)
		{
			Debug.Log(e);
			return 0;
		}

		return 1;
	}
	
	public static int InsertInto(string tableName , string[] values )
	{ // basic Insert with just values
		string query;
		query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for(int i=1; i< values.Length; i++)
		{
			query += ", " + values[i];
		}
		query += ")";
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e)
		{
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public static ArrayList SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue)
	{ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		while(reader.Read())
		{
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				j++;
			}
			readArray.Add(row);
		}
		return readArray; // return matches
	}


}