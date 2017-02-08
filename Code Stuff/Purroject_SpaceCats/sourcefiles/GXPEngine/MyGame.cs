using System;
using System.Drawing;
using GXPEngine;
using Purroject_SpaceCats;

//TODO Implement Nyarn cat (sic)
//TODO Shank a b*tch for this comment

public class MyGame : Game
{
	private MouseHandler _catHandler = null; //playerhandler won the vote over "ballhandler" & "ballfondler" //Renamed playerhandler to cathandler
	private Player _player = null;
	private Cat _cat = null;
	private Planet _planet = null;
	private Canvas _background = null;
	private Planet _planetTest = null;

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;
	private Vec2 _playerLastPosition = null;
	private Vec2 _playerBouncePos = null;
	private Vec2 _playerPOI = null;


	private float _accelerationValue = 0.0f;
	//private int _catCounter = 5;
	private float _leftBoundary, _rightBoundary, _topBoundary, _bottomBoundary;
	private float _bounceXPos, _bounceYPos;

	private int _shankCounter = 10; //counts the amount of times b*tches will get shanked, hypothetically that is. (for legal reasons).

	public MyGame() : base(1200, 900, false, false)
	{
		targetFps = 60;

		_background = new Canvas(width, height);
		AddChild(_background);
		_background.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, width, height));

		_player = new Player(30, new Vec2(width / 2, 200));
		AddChild(_player);

		_playerStartPosition = new Vec2(_player.x, _player.y);
		_playerLastPosition = new Vec2(_player.x, _player.y);
		_playerBouncePos = Vec2.zero;
		_playerPOI = Vec2.zero;

		_cat = new Cat(_player, 30);
		AddChild(_cat);

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_planet = new Planet(new Vec2(300, 200), "circle.png", 5, 1.5f, 150);
		AddChild(_planet);

		_planetTest = new Planet(new Vec2(500, 500), "circle.png", 5, 0.8f, 120);
		AddChild(_planetTest);

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);

		float border = 50;
		_leftBoundary = border;
		_rightBoundary = width - border;
		_topBoundary = border;
		_bottomBoundary = height - border;


		_shankCounter += 1;
		_shankCounter++;
	}

	private void onCatMouseDown(GameObject target, MouseEventType type)
	{

		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;
		_catHandler.OnMouseRightDown += onCatRightMouseDown;
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));

		_accelerationValue = _mouseDelta.Length() / 10;
		Console.WriteLine(_accelerationValue);
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;

		_cat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
	}

	private void onCatRightMouseDown(GameObject target, MouseEventType type)
	{
		_player.velocity = Vec2.zero;
		_cat.velocity = Vec2.zero;
		_player.position.SetXY(Input.mouseX, Input.mouseY);
		_cat.position.SetXY(_player.position.Clone().Add(_player.position.Clone().Normalize().Scale(_player.radius)));
	}

	private void OnMouseEvent(GameObject target, MouseEventType type)
	{
		Console.WriteLine("Eventtype: " + type + " triggered on " + target);
	}

	private void BasicCollisionCheck(Planet other) //Very Basic
	{
		Vec2 deltaVec = _player.position.Clone().Subtract(other.posVec);

		if ((_player.radius + other.gravityRadius) > deltaVec.Length())
		{
			//Console.WriteLine("Collision = true");
			_player.acceleration.Subtract((_planet.gravityForce * Mathf.Cos(deltaVec.GetAngleRadians())), (_planet.gravityForce * Mathf.Sin(deltaVec.GetAngleRadians())));
		}
		else {
			//Console.WriteLine("Collision = false");
		}
	}


	private void checkBoundaryCollisions()
	{
		bool leftHit = (_player.x - _player.radius) < _leftBoundary;
		bool rightHit = (_player.x + _player.radius) > _rightBoundary;
		bool topHit = (_player.y - _player.radius) < _topBoundary;
		bool bottomHit = (_player.y + _player.radius) > _bottomBoundary;

		if (leftHit || rightHit || topHit || bottomHit)
		{
			_playerBouncePos.x = _player.x;
			_playerBouncePos.y = _player.y;
			_player.ballColor = Color.Maroon;

			_playerPOI.SetXY(_playerBouncePos.Clone().Subtract(_playerLastPosition));

			if (leftHit)
			{
				_playerPOI.Scale(_leftBoundary - _playerBouncePos.x);
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (rightHit)
			{
				_playerPOI.Scale(_playerBouncePos.x - _rightBoundary);
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(-1, 1);
			}
			if (topHit)
			{
				_playerPOI.Scale(_topBoundary - _playerBouncePos.y);
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(1, -1);
			}
			if (bottomHit)
			{
				_playerPOI.Scale(_playerBouncePos.y - _bottomBoundary);
				_player.position.Add(_playerPOI);
				_player.velocity.Scale(1, -1);
			}
		}
		else {
			_player.ballColor = Color.Pink;
		}
	}

	private void Debug()
	{
		if (Input.GetKeyDown(Key.ONE))
		{
			targetFps = 60;
		}
		if (Input.GetKeyDown(Key.TWO))
		{
			targetFps = 12;
		}
		if (Input.GetKeyDown(Key.THREE))
		{
			targetFps = 2;
		}
		if (Input.GetKeyDown(Key.FOUR))
		{
			targetFps = 240;
		}
		if (Input.GetKeyDown(Key.FIVE))
		{
			targetFps = 999999999;
		}
	}

	void Update()
	{
		Debug();

		_player.Step();

		_cat.Step();

		_mouseDelta.SetXY(Input.mouseX - _player.position.x, Input.mouseY - _player.position.y);

		BasicCollisionCheck(_planet);
		BasicCollisionCheck(_planetTest);

		_background.graphics.DrawLine(new Pen(Color.White), _playerLastPosition.x, _playerLastPosition.y, _player.x, _player.y);

		_playerLastPosition.x = _player.x;
		_playerLastPosition.y = _player.y;

		checkBoundaryCollisions();
	}

	static void Main()
	{
		new MyGame().Start();
	}

	/// <summary>
	/// Ollieses the private empty void.
	/// </summary>
	private void OlliesPrivateEmptyVoid()
	{

	}

	/// <summary>
	/// Ollieses the private empty void.
	/// Again.
	/// </summary>
	private void OlliesPrivateEmptyVoid2_EletricBoogalo()
	{

	}
}
