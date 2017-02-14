using System;

namespace GXPEngine
{
	public class Planet : Ball
	{
		
		//Hitbox for the planet (don't add as child)
		private Ball _hitball;
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


		public Planet(Vec2 pPosVec, string pFilename, float pRadius, float pGravityForce = 1.0f, int pGravityRange = 0, float pRotationSpeed = 0.0f) : base (1, pPosVec)
		{
			_planetSprite = new Sprite(pFilename);
			_planetSprite.SetOrigin(_planetSprite.width / 2, _planetSprite.height / 2);
			alpha = 0.0f;
			AddChild(_planetSprite);
			_planetSprite.SetScaleXY(0.66f, 0.66f);
			//_planetSprite.alpha = 0.5f;

			_posVec = pPosVec;
			SetXY(_posVec.x, _posVec.y);
			_hitball = new Ball((int)(_planetSprite.width / 2.3), Vec2.zero);
			_hitball.alpha = 0.1f;
			AddChild(_hitball);

			if (pGravityRange != 0)
			{
				_gravityRange = new Ball((int)(pGravityRange), Vec2.zero, System.Drawing.Color.Cyan);
				//Console.WriteLine(pGravityRange);
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

		/// <summary>
		/// Gets the gravity force.
		/// </summary>
		/// <value>the scalar for calculating gravitational force</value>
		public float gravityForce
		{
			get{
				return _gravityForce;
			}
		}

		public int gravityRadius
		{
			get{
				return _gravityRadius;
			}
		}


		void Update()
		{
			rotation += _rotationSpeed;
		}

		public Vec2 posVec
		{
			get
			{
				return _posVec;
			}
		}
		public Ball hitball
		{
			get
			{
				return _hitball;
			}
		}
	}
}
