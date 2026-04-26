using Godot;
using Logos.Input;

namespace TestProject;

public partial class Main : Node2D
{
	private const int TileSize = 64;
	private const double MoveInterval = 0.2;
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
	
	private Vector2 _playerPos;
	private int _width;
	private int _height;
	
	private GodotKeyboardListener _keyboardListener;
	private KeyboardMapper _normalContext;
	private KeyboardMapper _poweredContext;

	private MoveControl _moveUp;
	private MoveControl _moveDown;
	private MoveControl _moveLeft;
	private MoveControl _moveRight;
	private JumpControl _jumpControl;
	
	private bool _isPowered;
	private bool _isJumping;
	
	private double _moveTimer;
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
		
		_moveUp = new MoveControl(new Vector2I(0, -1));
		_moveDown = new MoveControl(new Vector2I(0, 1));
		_moveLeft = new MoveControl(new Vector2I(-1, 0));
		_moveRight = new MoveControl(new Vector2I(1, 0));
		
		// bind wasd for both contexts
		BindMove(_normalContext, KeyCode.W, _moveUp);
		BindMove(_normalContext, KeyCode.S, _moveDown);
		BindMove(_normalContext, KeyCode.A, _moveLeft);
		BindMove(_normalContext, KeyCode.D, _moveRight);

		BindMove(_poweredContext, KeyCode.W, _moveUp);
		BindMove(_poweredContext, KeyCode.S, _moveDown);
		BindMove(_poweredContext, KeyCode.A, _moveLeft);
		BindMove(_poweredContext, KeyCode.D, _moveRight);

		// bind space to jump, only in powered context
		_jumpControl = new JumpControl();
		_poweredContext.Bind(new KeyGesture(KeyCode.Space, KeyAction.Press), _jumpControl);
		_poweredContext.Bind(new KeyGesture(KeyCode.Space, KeyAction.Repeat), _jumpControl);
		_poweredContext.Bind(new KeyGesture(KeyCode.Space, KeyAction.Release), _jumpControl);
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

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is not InputEventKey keyEvent) return;

		var keyCode = keyEvent.Keycode switch
		{
			Key.W => KeyCode.W,
			Key.A => KeyCode.A,
			Key.S => KeyCode.S,
			Key.D => KeyCode.D,
			Key.Space => KeyCode.Space,
			_ => (KeyCode?)null
		};

		if (keyCode is null) return;

		if (keyEvent.Pressed)
		{
			if (keyEvent.Echo) _keyboardListener.Repeat(keyCode.Value);
			else _keyboardListener.Press(keyCode.Value);
		}
		else
		{
			_keyboardListener.Release(keyCode.Value);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_hasWon)
		{
			QueueRedraw();
			return;
		}

		if (_jumpControl.State && !_isJumping)
		{
			_isJumping = true;
			_jumpTimer = JumpWindow;
		}
		
		_moveTimer -= delta;
		var move = _moveUp.State + _moveDown.State + _moveLeft.State + _moveRight.State;

		if (move != Vector2I.Zero && _moveTimer <= 0)
		{
			TryMove(move.X, move.Y, _isJumping);
			_moveTimer = MoveInterval;
		}

		// reset timer if no movement (so you can spam keys to move wicked fast)
		if (move == Vector2I.Zero) _moveTimer = 0;

		if (_isJumping)
		{
			_jumpTimer -= delta;
			if (_jumpTimer <= 0) _isJumping = false;
		}
		
		// Draw screen every frame
		QueueRedraw();
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
	
	private static void BindMove(KeyboardMapper mapper, KeyCode key, IKeyObserver observer)
	{
		mapper.Bind(new KeyGesture(key, KeyAction.Press), observer);
		mapper.Bind(new KeyGesture(key, KeyAction.Repeat), observer);
		mapper.Bind(new KeyGesture(key, KeyAction.Release), observer);
	}

	private sealed class MoveControl : KeyControl<Vector2I>
	{
		public MoveControl(Vector2I direction)
		{
			State = Vector2I.Zero;
			_direction = direction;
		}
		
		private readonly Vector2I _direction;

		public override void OnKeyPressed(object? sender, KeyEventArgs e)
		{
			State = _direction;
		}
		
		public override void OnKeyRepeated(object? sender, KeyEventArgs e)
		{
			State = _direction;
		}
		
		public override void OnKeyReleased(object? sender, KeyEventArgs e)
		{
			State = Vector2I.Zero;
		}
	}

	private sealed class JumpControl : KeyControl<bool>
	{
		public JumpControl()
		{ 
			State = false;
		}
		
		public override void OnKeyPressed(object? sender, KeyEventArgs e)
		{
			State = true;
		}

		public override void OnKeyRepeated(object? sender, KeyEventArgs e)
		{
			State = true;
		}

		public override void OnKeyReleased(object? sender, KeyEventArgs e)
		{
			State = false;
		}
	}
}