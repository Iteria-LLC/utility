using UnityEngine;
using static UnityEngine.Mathf;

#if !ITERIA_DENAMESPACE
namespace Iteria
{
#endif
	public static class Maths
	{
		public static float Remap(this float f, float low1, float high1, float low2, float high2)
		{
			return low2 + (f - low1) * (high2 - low2) / (high1 - low1);
		}

		public static float RemapClamped(this float f, float low1, float high1, float low2, float high2)
		{
			float hiClamp = high2;
			float loClamp = low2;
			if(hiClamp < loClamp)
				(loClamp, hiClamp) = (hiClamp, loClamp);

			return Clamp(low2 + (f - low1) * (high2 - low2) / (high1 - low1), loClamp, hiClamp);
		}

		public static int CardinalVector(Vector2 vec)
		{
			float angle = Atan2(vec.y, vec.x);
			return RoundToInt(4 * angle / (2 * PI) + 4) % 4;
		}

		public static int OrdinalVector(Vector2 vec)
		{
			float angle = Atan2(vec.y, vec.x);
			return RoundToInt(8 * angle / (2 * PI) + 8) % 8;
		}

		//Find the point of intersection of a line through a plane
		public static Vector3 LineIntersection(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 lineDir)
		{
			Debug.Assert(Vector3.Dot(planeNormal, lineDir.normalized) != 0);

			float t = (Vector3.Dot(planeNormal, planePoint) - Vector3.Dot(planeNormal, linePoint)) / Vector3.Dot(planeNormal, lineDir.normalized);
			return linePoint + (lineDir.normalized * t);
		}

		//Jacked from astar
		public static Vector3 ClosestPointOnInfiniteLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point) {
			Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
			float dot = Vector3.Dot(point - lineStart, lineDirection);

			return lineStart + lineDirection * dot;
		}

		public static Vector3 ClosestPointOnFiniteLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 direction = lineEnd - lineStart;
			float length = direction.magnitude;
			direction.Normalize();
			float projectedLength = Clamp(Vector3.Dot(point - lineStart, direction), 0f, length);
			return lineStart + direction * projectedLength;
		}

		public static float DeltaLerp(this float from, float to, float speedFactor, float sharpnessFactor = 0.5f)
		{
			return Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));
		}

		public static Quaternion DeltaLerp(this Quaternion from, Quaternion to, float speedFactor, float sharpnessFactor = 0.5f)
		{
			return Quaternion.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));
		}

		public static Color DeltaLerp(this Color from, Color to, float speedFactor, float sharpnessFactor = 0.5f)
		{
			return Color.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));
		}

		public static Vector3 DeltaLerp(this Vector3 from, Vector3 to, float speedFactor, float sharpnessFactor = 0.5f)
		{
			return Vector3.Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * speedFactor));
		}

		public static Vector3 BezierQuadratic(Vector3 a, Vector3 b, Vector3 control, float t)
		{
			return Vector3.Lerp(Vector3.Lerp(a, control, t), Vector3.Lerp(control, b, t), t);
		}

		public static Vector3 BezierQuadraticOffset(Vector3 a, Vector3 b, Vector3 offset, float t)
		{
			var control = ((a + b) / 2f) + offset;
			return BezierQuadratic(a, b, control, t);
		}

		public static float DeltaLerpVariableSpeed(this float from, float to, float speedFactorDown, float speedFactorUp, float sharpnessFactor = 0.5f)
		{
			return Lerp(from, to, 1f - Pow(1f - sharpnessFactor, Time.deltaTime * (from > to ? speedFactorDown : speedFactorUp)));
		}

		//Forward 
		public static Vector3 RandomInsideCone(float radius, float xScale = 1f, float yScale = 1f)
		{
			//(sqrt(1 - z^2) * cosϕ, sqrt(1 - z^2) * sinϕ, z)
			float radradius = radius * PI / 360;
			float z = Random.Range(Cos(radradius), 1);
			float t = Random.Range(0, PI * 2);
			return Vector3.Scale(new Vector3(Sqrt(1 - z * z) * Cos(t), Sqrt(1 - z * z) * Sin(t), z), new Vector3(xScale, yScale, 1));
		}

		public static Vector3 RandomInsideCone(Vector3 inputDirection, Vector3 upDirection, float radius, float xScale = 1f, float yScale = 1f)
		{
			Vector3 cone = RandomInsideCone(radius, xScale, yScale);
			Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.LookRotation(inputDirection, upDirection));
			return matrix.MultiplyVector(cone);
		}

		public static Vector3 RandomOnUnitCircle()
		{
			float angle = Random.Range(0f, PI * 2f);
			return new Vector3(Sin(angle), 0f, Cos(angle));
		}

		public static int RandomSign()
		{
			return Random.value > 0.5f ? 1 : -1;
		}

		//CREDIT Scott Hilbert
		public static float Angle(Quaternion a, Quaternion b)
		{
			Quaternion c = Quaternion.Inverse(a) * b;
			float angle = 360 * Atan2(Sqrt(c.x * c.x + c.y * c.y + c.z * c.z), c.w) / PI;
			if(angle > 180.0f)
				angle = 360.0f - angle;

			return angle;
		}

		//CREDIT Scott Hilbert
		public static Quaternion RotateTowards(Quaternion a, Quaternion b, float maxDegrees, float minAngleBeforeSnapping = 0.001f)
		{
			float angle = Angle(a, b);
			//If angle is super tiny, snap to "b" (target)
			//If maxDegrees is greater than the angle between a and b, just snap to b immediately
			//Otherwise do a slerp
			if(angle >= minAngleBeforeSnapping && maxDegrees < angle)
				return Quaternion.Slerp(a, b, maxDegrees / angle);
		
			 return b;
		}

		//Apparently this is faster than a "smart" implemenation. Sure.
		//https://stackoverflow.com/questions/4483886/how-can-i-get-a-count-of-the-total-number-of-digits-in-a-number
		/// <summary>
		/// Returns the number of digits, including the minus sign as a digit for negative numbers.
		/// </summary>
		public static int Digits(this int n)
		{
			if(n >= 0)
			{
				if(n < 10) return 1;
				if(n < 100) return 2;
				if(n < 1000) return 3;
				if(n < 10000) return 4;
				if(n < 100000) return 5;
				if(n < 1000000) return 6;
				if(n < 10000000) return 7;
				if(n < 100000000) return 8;
				if(n < 1000000000) return 9;
				return 10;
			}
			else
			{
				if(n > -10) return 2;
				if(n > -100) return 3;
				if(n > -1000) return 4;
				if(n > -10000) return 5;
				if(n > -100000) return 6;
				if(n > -1000000) return 7;
				if(n > -10000000) return 8;
				if(n > -100000000) return 9;
				if(n > -1000000000) return 10;
				return 11;
			}
		}
	}
#if !ITERIA_DENAMESPACE
}
#endif
