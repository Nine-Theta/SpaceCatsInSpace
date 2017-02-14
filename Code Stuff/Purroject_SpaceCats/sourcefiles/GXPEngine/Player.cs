﻿using System;
using Purroject_SpaceCats;
namespace GXPEngine
{
	//TODO: 
	public class Player : Ball
	{
		//private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;

		private AnimSprite _yarnSprite;
		private LevelManager _levelRef;
		private Arrow _arrow = null;

		private bool _selected;
		private int _animTimer = 5;
		//private float _gravityScale = 0.1f;

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

			_arrow = new Arrow(this);
			AddChild(_arrow);
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
		public Arrow arrow{
			set{
				_arrow = value ?? null;
			}
			get{
				return _arrow;
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
				//foreach (Planet planet in _levelRef.planetList)
				for (int i = 0; i < _levelRef.planetList.Length; i++)
				{
					Planet planet = _levelRef.planetList[i];
					if (planet != null)
					{
						Vec2 deltaVec = _position.Clone().Subtract(planet.posVec);
						if (planet.gravityRadius + radius > deltaVec.Length())
						{
							_acceleration.Subtract(deltaVec.Normalize().Scale(planet.gravityForce));
						}
					}
				}
			}
			if (_levelRef != null && _levelRef.asteroidList != null)
			{
				//foreach (Planet planet in _levelRef.planetList)
				for (int i = 0; i < _levelRef.asteroidList.Length; i++)
				{
					Asteroid asteroid = _levelRef.asteroidList[i];
					if (asteroid != null)
					{
						asteroid.Step();
						Vec2 deltaVec = _position.Clone().Subtract(asteroid.position);
						if ((radius + asteroid.radius) > deltaVec.Length())
						{
							velocity.Scale(0.5f);
							asteroid.AddVelocity(velocity.Clone());
						}
					}
				}
			}
		}

		public void Step()
		{
			_velocity.Add(_acceleration);
			_position.Add(_velocity);

			x = _position.x;
			y = _position.y;

			_acceleration = Vec2.zero;

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
