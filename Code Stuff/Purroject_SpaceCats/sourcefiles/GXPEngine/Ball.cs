﻿using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ball : Canvas
	{
		public readonly int radius;
		private Vec2 _position;
		private Vec2 _velocity;
		private Vec2 _acceleration;

		private Color _ballColor;

		//private Vec2 _startPosition;
		//private Vec2 _gravity;
		//private float _elasticity = 1.0f;
		//private bool _released;
		//private bool _colliding = false; 

		public Ball(int pRadius, Vec2 pPosition = null, Color? pColor = null) : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			position = pPosition;
			velocity = Vec2.zero;
			_acceleration = Vec2.zero;

			SetOrigin(radius, radius);
			_ballColor = pColor ?? Color.Blue;

			draw();
			x = position.x;
			y = position.y;
			//_startPosition = new Vec2(x, y);
		}

		private void draw()
		{
			graphics.Clear(Color.Empty);
			graphics.FillEllipse(
				new SolidBrush(_ballColor),
				0, 0, 2 * radius, 2 * radius
			);
		}

		public Color ballColor
		{
			get
			{
				return _ballColor;
			}
			set
			{
				_ballColor = value;
				draw();
			}
		}


		public Vec2 position
		{
			set
			{
				_position = value ?? Vec2.zero;
			}
			get
			{
				return _position;
			}
		}

		public Vec2 velocity
		{
			set
			{
				_velocity = value ?? Vec2.zero;
			}
			get
			{
				return _velocity;
			}
		}

		public Vec2 acceleration
		{
			set
			{
				_acceleration = value ?? Vec2.zero;
			}
			get
			{
				return _acceleration;
			}
		}

		public virtual void Step(){
			_velocity.Add(_acceleration);
			_position.Add(_velocity);

			x = _position.x;
			y = _position.y;

			_acceleration = Vec2.zero;

			//float oldX = _position.x;
			//float oldY = _position.y;
			//if (_released)
			//{
			//	ApplyBoundaries();
			//	//_velocity.Subtract(_gravity);
			//	_position.Add(_velocity);
			//	//Console.WriteLine(_velocity);
			//	if (_colliding == true)
			//	{
			//		ballColor = Color.Red;
			//		_colliding = false;
			//	}
			//	else
			//	{
			//		ballColor = Color.Blue;
			//	}
			//}
			//_lineCanvas.graphics.DrawLine(_pen, oldX, oldY, _position.x, _position.y);
			//UpdateArrow();
		}
	}
}

		//public bool released
		//{
		//	get{
		//		return _released;
		//	}
		//	set{
		//		_released = value;
		//	}
		//}

		//public Vec2 startPosition
		//{
		//	get{
		//		return _startPosition;
		//	}
		//}

		//public void ResetBall()
		//{
		//	velocity = Vec2.zero;
		//	SetXY(_startPosition.x, _startPosition.y);
		//	position.SetXY(_startPosition.x, _startPosition.y);
		//	//_lineCanvas.graphics.Clear(Color.Black);

		//}

		//void ApplyBoundaries()
		//{
		
		//	if (position.y - radius < 30)
		//	{
		//		//Top
		//		_velocity.y = -_elasticity * _velocity.y;
		//		_colliding = true;
		//	}
		//	if (position.y + radius > game.height - 30)
		//	{
		//		//Bottom
		//		_velocity.y = -_elasticity * _velocity.y;
		//		_colliding = true;
		//	}
		//	if (position.x - radius < 30)
		//	{
		//		//left
		//		_velocity.x = -_elasticity * _velocity.x;
		//		_colliding = true;
		//	}
		//	if (position.x + radius > game.width - 30)
		//	{
		//		//Right
		//		_velocity.x = -_elasticity * _velocity.x;
		//		_colliding = true;
		//	}
		//}

		//void UpdateArrow()
		//{
		//	_arrow.vector = _velocity;
		//	_arrow.startPoint = position;
		//	_arrow.color = 0xffffffff;
		//}

		//public void SetGravity(string direction)
		//{
		//	switch (direction)
		//	{
		//		case "Left":
		//			_gravity.SetAngleDegrees(180);
		//			break;
		//		case "Up":
		//			_gravity.SetAngleDegrees(90);
		//			break;
		//		case "Right":
		//			_gravity.SetAngleDegrees(0);
		//			break;
		//		case "Down":
		//			_gravity.SetAngleDegrees(270);
		//			break;
		//		case "":
		//			Console.WriteLine("Empty direction given for SetGravity.");
		//			break;
		//		default:
		//			Console.WriteLine("Invalid direction given for SetGravity:");
		//			Console.WriteLine(direction);
		//			break;
					
		//	}
		//}

