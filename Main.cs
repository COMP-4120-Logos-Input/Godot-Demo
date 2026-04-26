using System;
using System.Collections.Generic;
using Godot;
using Logos.Input;

namespace TestProject;

public partial class Main : Node2D
{
	private const int TileSize = 64;
	private const double RepeatRate = 0.2;
	private const double JumpWindow = 0.5;
	
	private static readonly char[][] Level =
	[
		"############".ToCharArray(),
		"#       O  #".ToCharArray(),
		"#       O  #".ToCharArray(),
		"# P  *  O G#".ToCharArray(),
		"#       O  #".ToCharArray(),
		"#       O  #".ToCharArray(),
		"############".ToCharArray()
	];

	private readonly Dictionary<string, double> _holdTimers = new();
	private Vector2 _playerPos;
	private int _width;
	private int _height;
	
	private GodotKeyboardListener _keyboardListener;
	private KeyboardMapper _normalContext;
	private KeyboardMapper _poweredContext;
	
	private bool _isPowered;
	private bool _isJumping;
	private double _jumpTimer;

	private bool _hasWon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// find and set playerloc
		for (var y = 0; y < Level.Length; y++)
		{
			for (var x = 0; x < Level[y].Length; x++)
			{
				var tile = Level[y][x];
					
				if (tile != 'P') continue;
				_playerPos = new Vector2(x, y);
				Level[y][x] = ' '; // remove player from level
				GD.Print($"Player at ({x}, {y})");
			}
		}
			
		// Set window size
		_width = Level[0].Length;
		_height = Level.Length;
			
		GetWindow().Size = new Vector2I(
			_width * TileSize,
			_height * TileSize
		);

		_keyboardListener = new GodotKeyboardListener();
		_normalContext = new KeyboardMapper(_keyboardListener, true);
		_poweredContext = new KeyboardMapper(_keyboardListener, false);
		
		BindMove(_normalContext, KeyCode.W, () => TryMove(0, -1, false));
		BindMove(_normalContext, KeyCode.S, () => TryMove(0, 1, false));
		BindMove(_normalContext, KeyCode.A, () => TryMove(-1, 0, false));
		BindMove(_normalContext, KeyCode.D, () => TryMove(1, 0, false));
		
		BindMove(_poweredContext, KeyCode.W, () => TryMove(0, -1, _isJumping));
		BindMove(_poweredContext, KeyCode.S, () => TryMove(0, 1, _isJumping));
		BindMove(_poweredContext, KeyCode.A, () => TryMove(-1, 0, _isJumping));
		BindMove(_poweredContext, KeyCode.D, () => TryMove(1, 0, _isJumping));

		_poweredContext.Bind(
			new KeyGesture(KeyCode.Space, KeyAction.Press),
			new MoveObserver(() =>
			{
				_isJumping = true;
				_jumpTimer = JumpWindow;
			})
		);
	}

	public override void _Draw()
	{
		for (var y = 0; y < Level.Length; y++)
		{
			for (var x = 0; x < Level[y].Length; x++)
			{
				var tile = Level[y][x];
				var rect = new Rect2(
					x * TileSize,
					y * TileSize,
					TileSize,
					TileSize
				);

				DrawRect(rect, tile == '#' ? Colors.Brown : Colors.DimGray);

				switch (tile)
				{
					case 'G':
						DrawRect(rect.Grow(-16), Colors.LimeGreen);
						break;
					case '*':
						DrawCircle(rect.GetCenter(), 14, Colors.Yellow);
						break;
					case 'O':
						DrawCircle(rect.GetCenter(), 30f, Colors.Black);
						break;
				}
			}
		}

		var playerRect = new Rect2(
			_playerPos.X * TileSize,
			_playerPos.Y * TileSize,
			TileSize,
			TileSize
		);

		var playerColor = Colors.Blue;

		if (_isPowered) playerColor = Colors.Cyan;
		if (_isJumping) playerColor = Colors.CornflowerBlue;
		if (_hasWon) playerColor = Colors.Yellow;
		
		DrawRect(playerRect, playerColor);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_hasWon)
		{
			QueueRedraw();
			return;
		}
		
		HandleKey("move_up", KeyCode.W, delta);
		HandleKey("move_down", KeyCode.S, delta);
		HandleKey("move_left", KeyCode.A, delta);
		HandleKey("move_right", KeyCode.D, delta);

		if (_isPowered && !_isJumping && Input.IsActionPressed("jump")) _keyboardListener.Press(KeyCode.Space);
		
		if (_isJumping)
		{
			_jumpTimer -= delta;
			if (_jumpTimer <= 0) _isJumping = false;
		}
		
		// Draw screen every frame
		QueueRedraw();
	}

	private void HandleKey(string action, KeyCode key, double delta)
	{
		if (Input.IsActionJustPressed(action))
		{
			_keyboardListener.Press(key);
			_holdTimers[action] = 0;
			return;
		}

		if (Input.IsActionPressed(action))
		{
			_holdTimers.TryAdd(action, 0);
			
			_holdTimers[action] += delta;

			if (!(_holdTimers[action] >= RepeatRate)) return;
			_keyboardListener.Repeat(key);
			_holdTimers[action] = 0;
		}
		else
		{
			_holdTimers.Remove(action);
		}
	}
	
	private void TryMove(int dx, int dy, bool canJump)
	{
		// cancel movement if u won already
		if (_hasWon) return;
		
		var newX = (int)_playerPos.X + dx;
		var newY = (int)_playerPos.Y + dy;

		// prevent out of bounds (important)
		if (newY < 0 || newY >= Level.Length)
			return;
		if (newX < 0 || newX >= Level[0].Length)
			return;

		var target = Level[newY][newX];

		switch (target)
		{
			case '#':
			case 'O' when !canJump:
				return;
			case 'O':
			{
				var jumpX = newX + dx;
				var jumpY = newY + dy;
			
				if (jumpY < 0 || jumpY >= Level.Length) return;
				if (jumpX < 0 || jumpX >= Level[0].Length) return;
			
				if (Level[jumpY][jumpX] == '#' || Level[jumpY][jumpX] == 'O') return;
			
				_playerPos = new Vector2(jumpX, jumpY);
				_isJumping = false;
				GD.Print("Jumped over a hole!");
				return;
			}
		}

		_playerPos = new Vector2(newX, newY);

		if (Level[(int)_playerPos.Y][(int)_playerPos.X] == 'G')
		{
			_hasWon = true;
			GD.Print(_isJumping ? "You reached the goal (while jumping!)" : "You reached the goal!");
		}
		
		if (target == '*' && !_isPowered)
		{
			Level[newY][newX] = ' ';
			EnablePoweredContext();
		}
		
		_isJumping = false;
	}

	private void EnablePoweredContext()
	{
		_isPowered = true;
		_normalContext.IsEnabled = false;
		_poweredContext.IsEnabled = true;
		GD.Print("Powered up! Press space to jump.");
	}
	
	private static void BindMove(KeyboardMapper mapper, KeyCode key, Action action)
	{
		mapper.Bind(new KeyGesture(key, KeyAction.Press), new MoveObserver(action));
		mapper.Bind(new KeyGesture(key, KeyAction.Repeat), new MoveObserver(action));
	}
	
	private sealed class MoveObserver(Action action) : IKeyObserver
	{
		public void OnKeyPressed(object? sender, KeyEventArgs e)
		{
			action();
		}

		public void OnKeyRepeated(object? sender, KeyEventArgs e)
		{
			action();
		}

		public void OnKeyReleased(object? sender, KeyEventArgs e)
		{
		}
	}
}