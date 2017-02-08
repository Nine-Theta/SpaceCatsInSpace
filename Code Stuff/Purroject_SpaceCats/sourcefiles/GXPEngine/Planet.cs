using System;

namespace GXPEngine
{
	public class Planet : Sprite
	{
		//Planets for Player access
		//private static Planet[] _planetList;

		//Hitbox for the planet (don't add as child)
		private Ball _hitball;
		//Range for the gravity of this planet (don't add as child)
		private Ball _gravityRange;

		//the radius of this planet's gravity
		private int _gravityRadius;
		//gravity force multiplier
		private float _gravityForce;
		//speed with which this planet rotates in degrees per frame
		private float _rotationSpeed;

		private Vec2 _posVec;


		public Planet(Vec2 pPosVec, string pFilename, float pRadius, float pGravityForce = 1.0f, int pGravityRange = 0, float pRotationSpeed = 0.0f) : base (pFilename)
		{
			//Potential future changes:
			//width = pRadius * 2;
			//height = pRadius * 2;
			SetOrigin(width / 2, height / 2);

			_gravityRadius = pGravityRange;
			_posVec = pPosVec;
			SetXY(_posVec.x, _posVec.y);
			_hitball = new Ball(width / 2, Vec2.zero);

			if (pGravityRange != 0)
			{
				_gravityRange = new Ball(_gravityRadius, Vec2.zero);
			}
			else{
				//If gravity range is not specified, set range of gravity to twice the size of the hitbox
				_gravityRange = new Ball(width, Vec2.zero);
			}
			_rotationSpeed = pRotationSpeed;
			_gravityForce = pGravityForce;

			//AddChild(_gravityRange);
			//AddPlanetList(this);
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

		/// <summary>
		/// Returns a boolean if in range of gravitational pull
		/// Accepts separate x and y floats but also a vec2
		/// </summary>
		public bool InRange(float pX, float pY, int pRadius)
		{
			Vec2 deltaVec = new Vec2(pX, pY);
			deltaVec.Subtract(_posVec);
			if (pRadius + _gravityRange.radius < deltaVec.Length()){
				//_gravityRange.color = 0xFF0000;
				return true;
			}
			else{
				//_gravityRange.color = 0x0000FF;
				return false;
			}
		}
		/// <summary>
		/// Returns a boolean if in range of gravitational pull
		/// /// </summary>
		public bool InRange(Vec2 pVec, int pRadius)
		{
			Vec2 deltaVec = pVec.Clone();
			deltaVec.Subtract(_posVec);
			if (pRadius + _gravityRange.radius < deltaVec.Length()){
				return true;
			}
			else{
				return false;
			}
		}

		public Vec2 posVec
		{
			get
			{
				return _posVec;
			}
		}

		//TODO: SOMETHING WITH THIS MAYBE
		////PlanetList setup for accessibility
		//public static void AddPlanetList(Planet pPlanet)
		//{
		//	int index = _planetList.Length;
		//	_planetList[index] = pPlanet;
		//}

		//public static void ClearPlanetList()
		//{
		//	int index = 0;
		//	foreach (Planet planet in _planetList)
		//	{
		//		planet.Destroy();
		//		_planetList[index] = null;
		//		index++;
		//	}
		//}

		////ReadOnly, add through AddPlanetList. Only clear when changing levels
		//public static Planet[] planetList
		//{
		//	get
		//	{
		//		return _planetList;
		//	}
		//}
	}
}
