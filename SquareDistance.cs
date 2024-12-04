using UnityEngine;

#if !ITERIA_DENAMESPACE
namespace Iteria
{
#endif
	public static class Square
	{
		public static readonly SqrDist Zero = new(0);
		public static readonly SqrDist MaxValue = new(float.MaxValue);
		public static readonly SqrDist MinValue = new(float.MinValue);
		public static readonly SqrDist Infinity = new(float.PositiveInfinity);
		public static readonly SqrDist NegativeInfinity = new(float.NegativeInfinity);

		public static SqrDist Distance(Vector3 a, Vector3 b)
		{
			return new SqrDist((a - b).sqrMagnitude, true);
		}

		public readonly struct SqrDist
		{
			public float Root => _root.GetValueOrDefault(Mathf.Sqrt(square));
			readonly float? _root;
			public readonly float square;

			public static bool operator >(SqrDist d, float a) => d.square > a * a;
			public static bool operator <(SqrDist d, float a) => d.square < a * a;
			public static bool operator >=(SqrDist d, float a) => d.square >= a * a;
			public static bool operator <=(SqrDist d, float a) => d.square <= a * a;

			public static float operator /(SqrDist d, float a) => d.square / (a * a);
			public static float operator *(SqrDist d, float a) => d.square * (a * a);

			public static bool operator >(SqrDist d, SqrDist a) => d.square > a.square;
			public static bool operator <(SqrDist d, SqrDist a) => d.square < a.square;
			public static bool operator >=(SqrDist d, SqrDist a) => d.square >= a.square;
			public static bool operator <=(SqrDist d, SqrDist a) => d.square <= a.square;

			public static float operator /(SqrDist d, SqrDist a) => d.square / a.square;
			public static float operator *(SqrDist d, SqrDist a) => d.square * a.square;

			public static SqrDist operator +(SqrDist d, SqrDist a) => new(d.Root + a.Root);

			public SqrDist(float d)
			{
				_root = d;
				square = d * d;
			}

			public SqrDist(float d, bool _)
			{
				_root = null;
				square = d;
			}
		}
	}
#if !ITERIA_DENAMESPACE
}
#endif
