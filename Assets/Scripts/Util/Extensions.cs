using UnityEngine;
using System.Collections;

public class Extensions {
}

public static class Vector3Extension {
	public static Vector3 withX(this Vector3 parent, float x)
	{
		return new Vector3(x, parent.y, parent.z);
	}
	
	public static Vector3 withY(this Vector3 parent, float y)
	{
		return new Vector3(parent.x, y, parent.z);
	}
	
	public static Vector3 withZ(this Vector3 parent, float z)
	{
		return new Vector3(parent.x, parent.y, z);
	}
	
	public static Vector3 addX(this Vector3 parent, float x)
	{
		return new Vector3(parent.x + x, parent.y, parent.z);
	}
	
	public static Vector3 addY(this Vector3 parent, float y)
	{
		return new Vector3(parent.x, parent.y + y, parent.z);
	}
	
	public static Vector3 addZ(this Vector3 parent, float z)
	{
		return new Vector3(parent.x, parent.y, parent.z + z);
	}
	
	public static Vector2 v2(this Vector3 parent)
	{
		return new Vector2(parent.x, parent.y);
	}
}
