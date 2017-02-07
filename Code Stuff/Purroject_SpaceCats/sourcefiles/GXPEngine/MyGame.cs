using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{	private MouseHandler _catHandler = null; //playerhandler won the vote over "ballhandler" & "ballfondler"
	private Player _player = null;
	private Cat _cat = null;

	private Vec2 _playerStartPosition = null;
	private Vec2 _mouseDelta = null;
	private Vec2 _mouseDeltaElastic = null;

	private float _accelerationValue = 0.0f;

	public MyGame () : base(800, 600, false)
	{
		_player = new Player(30, new Vec2(width / 2, 200));
		AddChild(_player);

		_playerStartPosition = new Vec2(_player.x, _player.y);

		_cat = new Cat(_player, 30);
		AddChild(_cat);

		_catHandler = new MouseHandler(_cat);
		_catHandler.OnMouseDownOnTarget += onCatMouseDown;

		_mouseDelta = new Vec2(Input.mouseX, Input.mouseY);
		_mouseDeltaElastic = new Vec2(0, 0);
	}

	private void onCatMouseDown(GameObject target, MouseEventType type){

		_catHandler.OnMouseMove += onCatMouseMove;
		_catHandler.OnMouseUp += onCatMouseUp;

		//_player.position.SetXY(_playerStartPosition);

	}

	private void onCatMouseMove(GameObject target, MouseEventType type)
	{
		_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));

		_accelerationValue = _mouseDelta.Length()/10;
		Console.WriteLine(_accelerationValue);
		//_mouseDeltaElastic.SetXY(_mouseDelta.Clone().Normalize().Scale(Mathf.Sqrt(_mouseDelta.Length())).Scale(10.0f).Add(_playerStartPosition));

		//_player.position.SetXY(_mouseDeltaElastic.Add(_playerHandler.offsetToTarget));

		//Console.WriteLine("test");
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

	void Update ()
	{
		_player.Step();

		//_cat.position.SetXY(_player.position.Clone().Add(_mouseDelta.Clone().Normalize().Scale(_player.radius)));
		_cat.Step();

		_mouseDelta.SetXY(Input.mouseX - _player.position.x, Input.mouseY - _player.position.y);
	}

	static void Main() 
	{
		new MyGame().Start();
	}
}
