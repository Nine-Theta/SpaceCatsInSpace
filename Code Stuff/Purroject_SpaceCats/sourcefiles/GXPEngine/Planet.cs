using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Planet : Sprite
	{
		private Ball _hitball;
		public Planet(Vec2 pPosVec) : base ("colors.png")
		{
			_hitball = new Ball(width / 2, pPosVec);
		}
	}
}
