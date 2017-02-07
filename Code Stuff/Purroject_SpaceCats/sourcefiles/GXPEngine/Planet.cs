using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Planet : Sprite
	{
		//Hitbox for the planet (don't add as child)
		private Ball _hitball;
		//Range for the gravity of this planet (don't add as child)
		private Ball _gravityRange;
		//gravity force multiplier
		private float _gravityForce;
		//speed with which this planet rotates in degrees per frame
		private float _rotationSpeed;
		private Vec2 posVec;


		public Planet(Vec2 pPosVec, string pFilename, float pGravityForce = 1.0f, int pGravityRange = 0, float pRotationSpeed = 0.0f) : base (pFilename)
		{
			SetOrigin(width / 2, height / 2);
			posVec = pPosVec.Clone();
			SetXY(posVec.x, posVec.y);
			_hitball = new Ball(width / 2, pPosVec);
			if (pGravityRange != 0)
			{
				_gravityRange = new Ball(pGravityRange, Vec2.zero);
			}
			else
			{
				//If gravity range is not specified, set range of gravity to twice the size of the hitbox
				_gravityRange = new Ball(width, Vec2.zero);
			}
			_rotationSpeed = pRotationSpeed;
			_gravityForce = pGravityForce;

			//AddChild(_gravityRange);
		}

		/// <summary>
		/// Gets the gravity force.
		/// </summary>
		/// <value>the scalar for calculating gravitational force</value>
		public float gravityForce
		{
			get
			{
				return _gravityForce;
			}
		}

		void Update()
		{
			rotation += _rotationSpeed;
		}


		/// <summary>
		/// Returns a boolean if in range of gravitational pull
		/// Accepts separate x and y floats but also a vec2
		/// </summary>
		public bool InRange(float pX, float pY, int pRadius)
		{
			Vec2 deltaVec = new Vec2(pX, pY);
			deltaVec.Subtract(posVec);
			if (pRadius + _gravityRange.radius < deltaVec.Length())
			{
				//_gravityRange.color = 0xFF0000;
				return true;
			}
			else
			{
				//_gravityRange.color = 0x0000FF;
				return false;
			}
		}
		/// <summary>
		/// Returns a boolean if in range of gravitational pull
		/// /// </summary>
		public bool InRange(Vec2 pVec, int pRadius)
		{
			Vec2 deltaVec = pVec.Clone();
			deltaVec.Subtract(posVec);
			if (pRadius + _gravityRange.radius < deltaVec.Length())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
