using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Digit : AnimSprite
	{
		private int _number = 0;
		public Digit(float pX, int pNumber = 0) : base("Sprites/Numbers.png", 10, 1)
		{
			SetXY(pX, 16);
			_number = pNumber;
			if (_number > 0)
			{
				SetFrame(_number - 1);
			}
			else
			{
				SetFrame(9);
			}
		}

		public void SetNumber(int pNumber)
		{
			_number = pNumber;
			if (pNumber > 0)
			{
				SetFrame(_number - 1);
			}
			else
			{
				SetFrame(9);
			}
		}
	}
}
