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

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;

	private float _accelerationValue = 0.0f;
	//private int _catCounter = 5;
	private int _shankCounter; //counts the amount of times b*tches will get shanked

	public MyGame () : base(800, 600, false)
	{
		_player = new Player(30, new Vec2(width / 2, 200));
		AddChild(_player);

		_playerStartPosition = new Vec2(_player.x, _player.y);

		_cat = new Cat(_player, 30);
		AddChild(_cat);

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_planet = new Planet(new Vec2(100, 100), "circle.png", 1);
		AddChild(_planet);

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);
	}

	private void onCatMouseDown(GameObject target, MouseEventType type){

		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;
	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));

		_accelerationValue = _mouseDelta.Length()/10;
		Console.WriteLine(_accelerationValue);
	}

	private void onCatMouseUp(GameObject target, MouseEventType type)
	{
		_catHandler.OnMouseMove -= onCatMouseMove;
		_catHandler.OnMouseUp -= onCatMouseUp;

		_cat.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(_accelerationValue));
		_player.acceleration.Add(_mouseDelta.Clone().Normalize().Scale(-_accelerationValue));
	}

	private void OnMouseEvent(GameObject target, MouseEventType type)
	{
		Console.WriteLine("Eventtype: " + type + " triggered on " + target);
	}

	private void CheckCollisions(GameObject other)
	{
		//if (_player.position.Clone().Subtract(_planet.
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

	void Update()
	{
		_player.Step();

		_cat.Step();

		_mouseDelta.SetXY(Input.mouseX - _player.position.x, Input.mouseY - _player.position.y);
	}

	static void Main() 
	{
		new MyGame().Start();
	}
}
