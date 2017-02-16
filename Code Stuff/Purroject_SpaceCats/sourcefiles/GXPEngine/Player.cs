using System;
using Purroject_SpaceCats;
namespace GXPEngine
{
	//TODO: Everything
	public class Player : Ball
	{
		//private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;

		private AnimSprite _yarnSprite;
		private LevelManager _levelRef;
		private Cat _cat = null;
		private Arrow _arrow = null;

		private bool _selected;
		private int _animTimer = 5;
		//Very bad fix: Shank me
		//Amount of frames of "invulnerability" to planet bumping
		//Doesn't fix the problem at hand but decreases the symptoms 
		private int _bouncedOffPlanetTimer = 3;
		private float _speedLimit = 20.0f;


		public Player(int pRadius, Vec2 pPosition = null) : base(pRadius, pPosition)
		{
			position = pPosition;
			_velocity = Vec2.zero;
			_acceleration = Vec2.zero;

			_yarnSprite = new AnimSprite("Sprites/Spritesheet.png", 4, 2);
			_yarnSprite.SetOrigin(_yarnSprite.width / 2, _yarnSprite.height / 2);
			_yarnSprite.SetScaleXY(0.3f, 0.3f);
			alpha = 0.0f;
			AddChild(_yarnSprite);

			_cat = new Cat(this);
			AddChild(_cat);

			_arrow = new Arrow(this);
			AddChild(_arrow);
			_arrow.alpha = 0.0f;
		}

		public Vec2 velocity{
			set{
				_velocity = value ?? Vec2.zero;
			}
			get{
				return _velocity;
			}
		}
		public Vec2 acceleration{
			set{
				_acceleration = value ?? Vec2.zero;
			}
			get{
				return _acceleration;
			}
		}
		public LevelManager levelRef
		{
			set{
				_levelRef = value;
			}
		}
		public bool selected{
			set{
				_selected = value;
			}
		}

		public Cat cat{
			set{
				_cat = value ?? null;
			}
			get{
				return _cat;
				}
		}
		public Arrow arrow{
			set{
				_arrow = value ?? null;
			}
			get{
				return _arrow;
			}
		}
		public AnimSprite yarnSprite
		{
			get
			{
				return _yarnSprite;
			}
		}

		private void AnimationCycle(){
			int tFrame = 0;
			if (_selected){
				tFrame = 4;
				tFrame += _yarnSprite.currentFrame % 4;
				tFrame++;
				if (tFrame >= 8){
					tFrame = 4;
				}
			}
			else{
				tFrame += _yarnSprite.currentFrame % 4;
				tFrame++;
				if (tFrame >= 4){
					tFrame = 0;
				}
			}
			_yarnSprite.SetFrame(tFrame);
		}

		private void PlanetGravity(){
			if (_levelRef != null && _levelRef.planetList != null)
			{
				_bouncedOffPlanetTimer--;
				for (int i = 0; i < _levelRef.planetList.Length; i++)
				{
					Planet planet = _levelRef.planetList[i];
					if (planet != null)
					{
						Vec2 deltaVec = position.Clone().Subtract(planet.posVec);
						if (planet.hitball.radius + radius > deltaVec.Length() && _bouncedOffPlanetTimer < 0)
						{
							///Iteration 1, reflects velocity. Bounces back where it came from
							//Console.WriteLine(_acceleration);
							//_acceleration.Invert();
							//_velocity.Invert();
							///Iteration 2, Sets degrees to match the delta vector's, effectively sends them away from the core. 
							//float degrees = deltaVec.GetAngleDegrees();
							//_acceleration.SetAngleDegrees(degrees);
							//_velocity.SetAngleDegrees(degrees);
							///Iteration 3: Dot Product. Should reflect with bounciness of 1. Fails miserably. Adding probably not the solution
							//Note: This can be used as a normal for the dot product as we are dealing with a circle, and the delta is not the line of reflection
							//Vec2 normalDelta = deltaVec.Clone().Normalize();
							//Console.WriteLine(normalDelta);
							//Vec2 projectedVec = _velocity.Clone().Normalize().Scale(_velocity.Dot(normalDelta));
							//_velocity.Add(projectedVec).Add(projectedVec);
							///Iteration 4: Dot Product angle forcing? Was this supposed to be used this way? WORKS! Kinda... See: Iteration 5
							Vec2 normalDelta = deltaVec.Clone().Normalize();
							//Console.WriteLine(normalDelta);
							Vec2 projectedVec = _velocity.Clone().Normalize().Scale(deltaVec.Dot(normalDelta));
							projectedVec.RotateDegrees(180);
							//_velocity.RotateRadians(projectedVec.GetAngleRadians()).RotateRadians(projectedVec.GetAngleRadians()).Scale(0.8f);
							//_acceleration.RotateRadians(projectedVec.GetAngleRadians()).RotateRadians(projectedVec.GetAngleRadians()).Scale(0.8f);
							///Iteration 5: To be used with Iteration 4. Fixes some of Iteration 4's issues. 
							//_position.SetXY(deltaVec.Clone().Normalize().Scale(planet.hitball.radius + radius).Add(planet.position));

							//Iteration 6:
							_velocity.Reflect(normalDelta, 1).Scale(0.8f);

							//Console.WriteLine(projectedVec.GetAngleDegrees());
							_bouncedOffPlanetTimer = 3;
						}
						else if (planet.gravityRadius + radius > deltaVec.Length())
						{
							_acceleration.Subtract(deltaVec.Normalize().Scale(planet.gravityForce));
						}
					}
				}
			}
			if (_levelRef != null && _levelRef.asteroidList != null)
			{
				for (int i = 0; i < _levelRef.asteroidList.Length; i++)
				{
					Asteroid asteroid = _levelRef.asteroidList[i];
					if (asteroid != null)
					{
						asteroid.Step();
						Vec2 deltaVec = position.Clone().Subtract(asteroid.position);
						if ((radius + asteroid.radius) > deltaVec.Length())
						{
							if (_velocity.Length() > 10.0f && !asteroid.crushed){
								asteroid.Crush();
							}
							_velocity.Scale(0.5f);
							_acceleration.Subtract(asteroid.velocity.Clone().Scale(0.7f));
							asteroid.acceleration.Add(_velocity.Clone().Scale(0.9f));
						}
					}
				}
			}
			if (_levelRef != null && _levelRef.pickupList != null)
			{
				for (int i = 0; i < _levelRef.pickupList.Length; i++)
				{
					Pickup pickup = _levelRef.pickupList[i];
					if (pickup != null)
					{
						Vec2 deltaVec = position.Clone().Subtract(pickup.position);
						if ((radius + pickup.radius) > deltaVec.Length())
						{
							//TODO: Make this do stuff
							pickup.Pick();
						}
					}
				}
			}
		}

		public void Step()
		{
			_velocity.Add(_acceleration);
			if (_velocity.Length() > _speedLimit){
				_velocity.Normalize().Scale(_speedLimit);
			}
			position.Add(_velocity);

			x = position.x;
			y = position.y;

			_acceleration = Vec2.zero;

			_cat.Step();

			_arrow.Step();

			_animTimer--;
			if (_animTimer < 0){
				AnimationCycle();
				_animTimer = 4;
			}

			PlanetGravity();
		}
	}
}
