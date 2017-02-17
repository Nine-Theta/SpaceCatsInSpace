using System;
using GXPEngine;
namespace Purroject_SpaceCats
{
	public class BlackHole : Planet
	{
		public BlackHole(Vec2 pPosVec, int pRadius, int pGravityRange) : base(pPosVec, "Sprites/Black Hole.png", pRadius, 1.0f, pGravityRange, 20)
		{
			_planetSprite.SetScaleXY(1, 1);
		}
	}
}
