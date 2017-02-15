using System;
using GXPEngine;

namespace Purroject_SpaceCats
{
	public class Pickup : Ball
	{
		private Sprite _sprite;
		private MyGame _gameRef;
		public Pickup(int pRadius, Vec2 pPosVec, string pSource, MyGame pGameRef) : base(pRadius, pPosVec)
		{
			alpha = 0.0f;
			_sprite = new Sprite(pSource);
			_sprite.SetOrigin(_sprite.width / 2, _sprite.height / 2);
			_sprite.width = pRadius * 2;
			_sprite.height = pRadius * 2;
			AddChild(_sprite);
			_gameRef = pGameRef;
		}

		//TODO: Communicate what their purposes should be. Just score?
		public void Pick()
		{
			switch (_sprite.name)
			{
				case "Sprites/Milk.png":
					//Milk stuffs
					break;
				case "Sprites/Fish.png":
					//Fish stuffs
					break;
				case "Sprites/Pick Up.png":
					//Pickup stuffs (the paw bubble one)
					_gameRef.AddScore(1);
					break;
				default:
					Console.WriteLine("Error on Pickup source name" + _sprite.name );
					break;
			}
			this.Destroy();
		}
	}
}
