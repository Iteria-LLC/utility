using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class Extensions
{
	#region Enum Extensions
	public static string Format<T>(this T enu) where T : System.Enum
	{
		return System.Text.RegularExpressions.Regex.Replace(System.Enum.GetName(typeof(T), enu), "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
	}

	public static T Increment<T>(this T enu) where T : System.Enum
	{
		var enumerations = System.Enum.GetValues(typeof(T));
		int curr = System.Array.IndexOf(enumerations, enu);
		int next = curr < enumerations.Length - 1 ? curr + 1 : 0;

		return (T)enumerations.GetValue(next);
	}

	public static T Decrement<T>(this T enu) where T : System.Enum
	{
		var enumerations = System.Enum.GetValues(typeof(T));
		int curr = System.Array.IndexOf(enumerations, enu);
		int prev = curr > 0 ? curr - 1 : enumerations.Length - 1;

		return (T)enumerations.GetValue(prev);
	}
	#endregion

	#region Index Wrapping
		#region Array
	/// <summary>
	/// Returns index, wrapped to the bounds of the given collection.
	/// </summary>
	public static int WrapIndex<T>(this T[] array, int index)
	{
		return ((index % array.Length) + array.Length) % array.Length;
	}

	/// <summary>
	/// Increments index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void IncrementIndex<T>(this ref int index, T[] array, bool wrap = true)
	{
		if(wrap)
			index = index < array.Length - 1 ? index + 1 : 0;
		else
			index = Clamp(index + 1, 0, array.Length - 1);
	}

	/// <summary>
	/// Decrements index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void DecrementIndex<T>(this ref int index, T[] array, bool wrap = true)
	{
		if(wrap)
			index = index > 0 ? index - 1 : array.Length - 1;
		else
			index = Clamp(index - 1, 0, array.Length - 1);
	}

	/// <summary>
	/// Modifies index by reference, adding add, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void ModIndex<T>(this ref int index, int add, T[] array, bool wrap = true)
	{
		if(wrap)
			index = ((index + add % array.Length) + array.Length) % array.Length;
		else
			index = Clamp(index + add, 0, array.Length - 1);
	}

	public static T RandomElement<T>(this T[] array)
	{
		return array[Random.Range(0, array.Length)];
	}
	#endregion
		#region List
	/// <summary>
	/// Returns index, wrapped to the bounds of the given collection.
	/// </summary>
	public static int WrapIndex<T>(this List<T> list, int index)
	{
		return ((index % list.Count) + list.Count) % list.Count;
	}

	/// <summary>
	/// Increments index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void IncrementIndex<T>(this ref int index, List<T> list, bool wrap = true)
	{
		if(wrap)
			index = index < list.Count - 1 ? index + 1 : 0;
		else
			index = Clamp(index + 1, 0, list.Count - 1);
	}

	/// <summary>
	/// Decrements index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void DecrementIndex<T>(this ref int index, List<T> list, bool wrap = true)
	{
		if(wrap)
			index = index > 0 ? index - 1 : list.Count - 1;
		else
			index = Clamp(index - 1, 0, list.Count - 1);
	}

	/// <summary>
	/// Modifies index by reference, adding add, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void ModIndex<T>(this ref int index, int add, List<T> list, bool wrap = true)
	{
		if(wrap)
			index = ((index + add % list.Count) + list.Count) % list.Count;
		else
			index = Clamp(index + add, 0, list.Count - 1);
	}

	public static T RandomElement<T>(this List<T> list)
	{
		return list[Random.Range(0, list.Count)];
	}
	#endregion
		#region Dictionary
	/// <summary>
	/// Returns index, wrapped to the bounds of the given collection.
	/// </summary>
	public static int WrapIndex<K, V>(this Dictionary<K, V> dict, int index)
	{
		return ((index % dict.Count) + dict.Count) % dict.Count;
	}

	/// <summary>
	/// Increments index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void IncrementIndex<K, V>(this ref int index, Dictionary<K, V> dict, bool wrap = true)
	{
		if(wrap)
			index = index < dict.Count - 1 ? index + 1 : 0;
		else
			index = Clamp(index + 1, 0, dict.Count - 1);
	}

	/// <summary>
	/// Decrements index by reference, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void DecrementIndex<K, V>(this ref int index, Dictionary<K, V> dict, bool wrap = true)
	{
		if(wrap)
			index = index > 0 ? index - 1 : dict.Count - 1;
		else
			index = Clamp(index - 1, 0, dict.Count - 1);
	}

	/// <summary>
	/// Modifies index by reference, adding add, by default wrapped to the bounds of the given collection.
	/// </summary>
	public static void ModIndex<K, V>(this ref int index, int add, Dictionary<K, V> dict, bool wrap = true)
	{
		if(wrap)
			index = ((index + add % dict.Count) + dict.Count) % dict.Count;
		else
			index = Clamp(index + add, 0, dict.Count - 1);
	}
	#endregion
	#endregion

	#region With(...) Extensions
	//Idea from somewhere else, but forgot where.
	public static Vector4 With(this Vector4 vec, float? x = null, float? y = null, float? z = null, float? w = null)
		=> new(
			x.GetValueOrDefault(vec.x),
			y.GetValueOrDefault(vec.y),
			z.GetValueOrDefault(vec.z),
			w.GetValueOrDefault(vec.w));

	public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null)
		=> new(
			x.GetValueOrDefault(vec.x),
			y.GetValueOrDefault(vec.y),
			z.GetValueOrDefault(vec.z));

	public static Vector2 With(this Vector2 vec, float? x = null, float? y = null)
		=> new(
			x.GetValueOrDefault(vec.x),
			y.GetValueOrDefault(vec.y));

	public static Vector3Int With(this Vector3Int vec, int? x = null, int? y = null, int? z = null)
		=> new(
			x.GetValueOrDefault(vec.x),
			y.GetValueOrDefault(vec.y),
			z.GetValueOrDefault(vec.z));

	public static Vector2Int With(this Vector2Int vec, int? x = null, int? y = null)
		=> new(
			x.GetValueOrDefault(vec.x),
			y.GetValueOrDefault(vec.y));

	public static Color With(this Color col, float? r = null, float? g = null, float? b = null, float? a = null)
		=> new(
			r.GetValueOrDefault(col.r),
			g.GetValueOrDefault(col.g),
			b.GetValueOrDefault(col.b),
			a.GetValueOrDefault(col.a));

	public static Color32 With(this Color32 col, byte? r = null, byte? g = null, byte? b = null, byte? a = null)
		=> new(
			r.GetValueOrDefault(col.r),
			g.GetValueOrDefault(col.g),
			b.GetValueOrDefault(col.b),
			a.GetValueOrDefault(col.a));
	#endregion

	#region Delta Lerps
	public static float DeltaLerp(this float from, float to, float speedFactor, float sharpnessFactor = 0.5f)
		=> Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));

	public static Quaternion DeltaLerp(this Quaternion from, Quaternion to, float speedFactor, float sharpnessFactor = 0.5f)
		=> Quaternion.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));

	public static Color DeltaLerp(this Color from, Color to, float speedFactor, float sharpnessFactor = 0.5f)
		=> Color.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));

	public static Vector3 DeltaLerp(this Vector3 from, Vector3 to, float speedFactor, float sharpnessFactor = 0.5f)
		=> Vector3.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));
	#endregion

	public static Vector3 GetDirectionTo(this Vector3 from, Vector3 to)
		=> (to - from).normalized;

	public static Vector3 GetXZDirectionTo(this Vector3 from, Vector3 to)
		=> (to.With(y: 0) - from.With(y: 0)).normalized;

	public static float GetYDifference(this Vector3 a, Vector3 b)
		=> Abs(a.y - b.y);

	/// <summary>
	/// Does this contain the other bounds entirely?
	/// </summary>
	public static bool Contains(this Bounds b, Bounds bounds)
	{
		return b.Contains(bounds.center - bounds.extents) && b.Contains(bounds.center + bounds.extents);
	}

	public static bool Match(this AnimatorStateInfo info, int hash)
	{
		return hash == info.fullPathHash || hash == info.shortNameHash;
	}

	public static bool Match(this AnimatorStateInfo info, params int[] animationHashes)
	{
		for(int i = 0; i < animationHashes.Length; i++)
		{
			if(info.Match(animationHashes[i]))
				return true;
		}

		return false;
	}

	public static bool PointInFrustum(this Camera camera, Vector3 point)
	{
		var v = camera.WorldToViewportPoint(point);
		return v.x >= 0f && v.y >= 0f &&
			v.x <= 1f && v.y <= 1f &&
			v.z >= 0;
	}
}