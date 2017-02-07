using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class SpaceStation : AnimSprite
	{
		public SpaceStation(float pX, float pY, int pFrame) : base ("checkers.png", 2, 1)
		{
			SetXY(pX, pY);
			SetOrigin(width / 2, height / 2);
			SetFrame(pFrame);
		}
	}
}
