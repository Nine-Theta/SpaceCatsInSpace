using System;
using GXPEngine; 

namespace Purroject_SpaceCats
{
	public class HUD : Canvas
	{
		private Sprite _hudSprite;
		public HUD(int pWidth, int pHeight) : base(pWidth, pHeight)
		{
			_hudSprite = new Sprite("Sprites/Hud.png");
		}
	}
}
