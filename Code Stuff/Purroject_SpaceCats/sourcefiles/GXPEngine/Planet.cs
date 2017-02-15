using System;

namespace GXPEngine
{
	public class Planet : Ball
	{
		protected enum PlanetType { BLUE, PURPLE, GREEN, RED, BLACKHOLE }

		//Hitbox for the planet (don't add as child)
		private Ball _hitball;
		private Ball _hitball2;
		//Range for the gravity of this planet (don't add as child)
		private Ball _gravityRange;
		//Fix to resize without fucking up everything
		protected Sprite _planetSprite;

		//the radius of this planet's gravity
		private int _gravityRadius;
		//gravity force multiplier
		private float _gravityForce;
		//speed with which this planet rotates in degrees per frame
		private float _rotationSpeed;

		private Vec2 _posVec;

		private PlanetType _planetType;

		public Planet(Vec2 pPosVec, string pFilename, float pRadius, float pGravityForce = 1.0f, int pGravityRange = 0, float pRotationSpeed = 1.0f) : base (1, pPosVec)
		{
			_posVec = pPosVec;
			SetXY(_posVec.x, _posVec.y);

			_planetSprite = new Sprite(pFilename);
			_planetSprite.SetOrigin(_planetSprite.width / 2, _planetSprite.height / 2);
			alpha = 0.0f;
			AddChild(_planetSprite);
			//_planetSprite.SetScaleXY(0.66f, 0.66f);
			//_planetSprite.alpha = 0.5f;

			//Console.WriteLine(pFilename);

			//Are these types really necessary? You can access the filename through _planetSprite.name too, so you can cut out the middle man here 
			if (pFilename == "Sprites/Planet 1.png")
				_planetType = PlanetType.BLUE;
			if (pFilename == "Sprites/Planet 2.png")
				_planetType = PlanetType.PURPLE;
			if (pFilename == "Sprites/Planet 3.png")
				_planetType = PlanetType.GREEN;
			if (pFilename == "Sprites/Planet 4.png")
				_planetType = PlanetType.RED;

			SetHitball();

			if (pGravityRange != 0){
				_gravityRange = new Ball((int)(pGravityRange), Vec2.zero, System.Drawing.Color.Cyan);
			}
			else{
				//If gravity range is not specified, set range of gravity to twice the size of the hitbox
				_gravityRange = new Ball((int)(width * 3), Vec2.zero);
				//Console.WriteLine(_gravityRange.radius);
			}

			_gravityRadius = _gravityRange.radius;
			_rotationSpeed = pRotationSpeed;
			_gravityForce = pGravityForce;

			AddChild(_gravityRange);
			_gravityRange.alpha = 0.125f;
		}

		private void SetHitball()
		{
			if (_planetType == PlanetType.BLUE)
				_hitball = new Ball((int)(_planetSprite.width / 2.5f), new Vec2(20,-6));
			if (_planetType == PlanetType.PURPLE)
				_hitball = new Ball((int)(_planetSprite.width / 2.3f), Vec2.zero);
			if (_planetType == PlanetType.GREEN)
				_hitball = new Ball((int)(_planetSprite.width / 3.6f), new Vec2(-2,-12));
			if (_planetType == PlanetType.RED){
				_hitball = new Ball((int)(_planetSprite.width / 3.0f), new Vec2(-8, -7));
				_hitball2 = new Ball((int)(_planetSprite.width / 10.0f), new Vec2(165, 123));
				AddChild(_hitball2);
				_hitball2.alpha = 0.5f;
			}
			
			_hitball.alpha = 0.5f;
			AddChild(_hitball);
			//Console.WriteLine(_planetType);
		}

		/// <summary>
		/// Gets the gravity force.
		/// </summary>
		/// <value>the scalar for calculating gravitational force</value>
		public float gravityForce{
			get{
				return _gravityForce;
			}
		}

		public int gravityRadius{
			get{
				return _gravityRadius;
			}
		}


		void Update(){
			rotation += _rotationSpeed;
		}

		public Vec2 posVec{
			get{
				return _posVec;
			}
		}
		public Ball hitball{
			get{
				return _hitball;
			}
		}
	}
}
