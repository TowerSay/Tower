using UnityEngine;
using System.Collections;

/// <summary>通用点结构</summary>
public struct Point
{
	public int x,y;
	public Point(int x=0,int y=0)
	{
		this.x=x;
		this.y=y;
	}
	
	public string ToString()
	{
		return "("+x+","+y+")";
	}
	
	public static bool operator ==(Point a, Point b)
	{
		return (a.x==b.x && a.y==b.y);
	}
	
	public static bool operator !=(Point a, Point b)
	{
		return (a.x!=b.x || a.y!=b.y);
	}
}