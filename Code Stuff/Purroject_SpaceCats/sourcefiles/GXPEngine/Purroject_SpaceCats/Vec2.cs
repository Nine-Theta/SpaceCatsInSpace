using System;

namespace GXPEngine
{
	public class Vec2
	{
		public static Vec2 zero { get { return new Vec2(0, 0); } }
		private static Random rand = new Random();
		public float x = 0;
		public float y = 0;

		public Vec2(float pX = 0, float pY = 0)
		{
			x = pX;
			y = pY;
		}

		public override string ToString()
		{
			return String.Format("X = {0}, Y = {1}, Length = {2}", x, y, Length());
		}

		public Vec2 Add(Vec2 other)
		{
			x += other.x;
			y += other.y;
			return this;
		}

		public Vec2 Add(Core.Vector2 other)
		{
			x += other.x;
			y += other.y;
			return this;
		}

		public Vec2 Add(float pX, float pY) //Overload added to support normal XY values
		{
			x += pX;
			y += pY;
			return this;
		}

		public Vec2 Subtract(Vec2 other)
		{
			x -= other.x;
			y -= other.y;
			return this;
		}

		public Vec2 Subtract(float pX, float pY) //Overload added to support normal XY values
		{
			x -= pX;
			y -= pY;
			return this;
		}

		public Vec2 Scale(float scalar)
		{
			x *= scalar;
			y *= scalar;
			return this;
		}

		public Vec2 Scale(float pScalarX, float pScalarY) //Overload added to support separate XY scaling
		{
			x *= pScalarX;
			y *= pScalarY;
			return this;
		}

		public float Length()
		{
			float tLength;
			//Simple Geometry
			//RYUU GA WAGA TEKI WO KURAU
			tLength = (float)(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
			return tLength;
		}

		public Vec2 Normalize()
		{
			//Temp Length because Length() will change after changing the X
			float tLength = Length();
			if (tLength != 0)
			{
				x = x / tLength;
				y = y / tLength;
			}
			else{
				Console.WriteLine("Normalize failed, 0 in formula");
			}
			return this;
		}

		public Vec2 Clone()
		{
			Vec2 clone = new Vec2(this.x, this.y);
			return clone;
		}

		public Vec2 SetXY(float pX, float pY)
		{
			x = pX;
			y = pY;
			return this;
		}

		public Vec2 SetXY(Vec2 other)
		{
			x = other.x;
			y = other.y;
			return this;
		}

		/// <summary>
		/// Checks if the x and y are equal to the parameters. 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool EqualsTo(float pX, float pY)
		{
			return (this.x == pX && this.y == pY);
		}
		/// <summary>
		/// Checks if current vector's x and y coordinates are equal to the other vector
		/// </summary>
		/// <returns><c>true</c>, if to was equalsed, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool EqualsTo(Vec2 other)
		{
			return (other.x == this.x && other.y == this.y);
		}

		//Assignment 2
		public static float Deg2Rad(float deg)
		{
			float result = deg * (float)(Math.PI) / 180;
			return result;
		}
		public static float Rad2Deg(float rad)
		{
			float result = rad * 180 / (float)(Math.PI);
			return result;
		}
		public static Vec2 GetUnitVectorDegrees(float degrees)
		{
			float rads = Deg2Rad(degrees);
			Vec2 vector = new Vec2((float)(Math.Cos(rads)), (float)(Math.Sin(rads)));
			return vector;
		}
		public static Vec2 GetUnitVectorRadians(float radians)
		{
			Vec2 vector = new Vec2((float)(Math.Cos(radians)), (float)(Math.Sin(radians)));
			return vector;
		}

		public static Vec2 RandomUnitVector()
		{
			int randDeg = rand.Next(0, 360);
			Vec2 vec = GetUnitVectorDegrees(randDeg);
			return vec;
		}

		public void SetAngleDegrees(float degrees)
		{
			float oldLength = Length();
			SetXY(GetUnitVectorDegrees(degrees));
			Scale(oldLength);
		}
		public void SetAngleRadians(float radians)
		{
			float oldLength = Length();
			float degrees = Rad2Deg(radians);
			SetXY(GetUnitVectorDegrees(degrees));
			Scale(oldLength);
		}
		public float GetAngleRadians()
		{
			float radians = (float)Math.Atan2(this.y, this.x);
			return radians;
		}
		public float GetAngleDegrees()
		{
			float radians = (float)Math.Atan2(this.y, this.x);
			float degrees = Rad2Deg(radians);
			return degrees;
		}
		public Vec2 RotateDegrees(float degrees)
		{
			float radians = Deg2Rad(degrees);
			float cosRad = (float)(Math.Cos(radians));
			float sinRad = (float)(Math.Sin(radians));
			return SetXY(this.x * cosRad - this.y * sinRad, this.x * sinRad + this.y * cosRad);
		}
		public Vec2 RotateRadians(float radians)
		{
			float cosRad = (float)(Math.Cos(radians));
			float sinRad = (float)(Math.Sin(radians));
			return SetXY(this.x * cosRad - this.y * sinRad, this.x * sinRad + this.y * cosRad);
		}
		public Vec2 RotateAroundDegrees(Vec2 point, float degrees)
		{
			this.x -= point.x;
			this.y -= point.y;
			this.RotateDegrees(degrees);
			this.x += point.x;
			this.y += point.y;
			return this;
		}
		public Vec2 RotateAroundRadians(Vec2 point, float radians)
		{
			this.x -= point.x;
			this.y -= point.y;
			this.RotateRadians(radians);
			this.x += point.x;
			this.y += point.y;
			return this;
		}

		public Vec2 GetNormal()
		{
			return new Vec2(-this.y, this.x);
		}
	}
}

