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
				_gravityRange = new Ball(pGravityRange, pPosVec);
			}
			else
			{
				//If gravity range is not specified, set range of gravity to twice the size of the hitbox
				_gravityRange = new Ball(width, pPosVec);
			}
			_rotationSpeed = pRotationSpeed;
			_gravityForce = pGravityForce;
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
	}
}
