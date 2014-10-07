using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public static Point operator +(Point a, Point b)
	{
		return new Point(a.x+b.x,a.y+b.y);
	}

	public static Point operator -(Point a, Point b)
	{
		return new Point(a.x-b.x,a.y-b.y);
	}

	public static Point operator *(Point a, Point b)
	{
		return new Point(a.x*b.x,a.y*b.y);
	}

	public static Point operator /(Point a, Point b)
	{
		return new Point((int)((float)a.x/(float)b.x),(int)((float)a.y/(float)b.y));
	}
}


/// <summary>精灵包</summary>
public class SpriteClipPack
{
	public Dictionary<Point,Sprite> spts=new Dictionary<Point, Sprite>();
	public SpriteClipPack(string name,Point size)
	{
		spts=Game.ClipFromMainTexture(Game.Tex(name),size);
	}
}