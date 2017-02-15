using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Pickup : Ball
	{
		private Sprite _sprite;
		public Pickup(int pRadius, Vec2 pPosVec, string pSource) : base(pRadius, pPosVec)
		{
			alpha = 0.0f;
			_sprite = new Sprite(pSource);
			_sprite.SetOrigin(_sprite.width / 2, _sprite.height / 2);
			_sprite.width = pRadius * 2;
			_sprite.height = pRadius * 2;
			AddChild(_sprite);
		}

		//TODO: Communicate what their purposes should be. Just score?
		public void Pick()
		{
			switch (_sprite.name)
			{
				case "Milk.png":
					//Milk stuffs
					break;
				case "Fish.png":
					//Fish stuffs
					break;
				case "Pickup.png":
					//Pickup stuffs (the paw bubble one)
					break;
				default:
					Console.WriteLine("Error on Pickup source name" + _sprite.name );
					break;
			}
			this.Destroy();
		}
	}
}
