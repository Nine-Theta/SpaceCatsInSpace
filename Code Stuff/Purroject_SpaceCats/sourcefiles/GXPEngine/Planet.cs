using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Planet : Sprite
	{
		//Hitbox for the planet (don't add as child)
		private Ball _hitball;
		//gravity force multiplier
		private float _gravityForce;


		public Planet(Vec2 pPosVec) : base ("colors.png")
		{
			_hitball = new Ball(width / 2, pPosVec);
		}

		public float gravityForce
		{
			get
			{
				return _gravityForce;
			}
		}
	}
}
