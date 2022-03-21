using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_DVA222
{
    internal class Engine
    {
        public int AmountOfPlayers { get; private set; }
        private int _currentAmountOfPlayers;
        public int Height { get; private set; }
        public int Width { get; private set; }
        private int snakeStartLength = 5;
        public int GameObjectSize { get; private set; }

        // *INDIVIDUAL ASSIGNMENT*
        // Made the list of snakes gettable.
        public List<Snake> _snakes { get; private set; } = new List<Snake>();
        List<Food> _food = new List<Food>();

        MainForm _form;
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        Movement _movement = new Movement();

        // *INDIVIDUAL ASSIGNMENT*
        private int _moveCount = 1;

        public Engine()
        {

            _form = new MainForm(this);

        }

        public void Run()
        {
            _form.KeyDown += KeyEventHandler;
            _form.Paint += Draw;
            _form.Resize += OnResize;
            SetGameSizes();

            _timer.Tick += TimerEventHandler;

            // *INDIVIDUAL ASSIGNMENT* 
            // Went to 60 fps for the movement.
            _timer.Interval = 1000 / 60;

            Application.Run(_form);
        }

        private void SetGameSizes()
        {
            Height = _form.Height - _form.Height / 16;
            Width = _form.Width - _form.Width / 40;
            GameObjectSize = Width / 80;
        }

        private void OnResize(object? sender, EventArgs e) => SetGameSizes();

        public void Add(Snake snake) => _snakes.Add(snake);
        public void Add(Food food) => _food.Add(food);
        public void Remove(Snake snake)
        {
            _snakes.Remove(snake);
            _currentAmountOfPlayers--;
        }
        public void Remove(Food food) => _food.Remove(food);

        private void Draw(object? sender, PaintEventArgs e)
        {
            var snakes = new List<Snake>(_snakes);
            var foods = new List<IFood>(_food);

            foreach (var snake in snakes)
                snake.Draw(e.Graphics);
            foreach (var food in foods)
                food.Draw(e.Graphics);
        }

        private void TimerEventHandler(object? sender, EventArgs e)
        {
            _form.ScoreDisplay.Text = GetScoreString();
            _form.Refresh();
            Collide();
            Move();
            TrySpawnFood();
            if (_snakes.Count < 1) GameOver();
        }

        public void StartGame(int amountOfPlayers)
        {
            _timer.Start();

            _currentAmountOfPlayers = AmountOfPlayers = amountOfPlayers;
            Coordinate snakeCoordinate;
            for (int i = 0; i < amountOfPlayers; i++)
            {
                if (i % 2 == 0)
                {
                    // "10 * GameObjectSize" is how far away it should be from the center.
                    // i * 4 * GameObjectSize is far away it should be from snakes spawning on the same side.
                    snakeCoordinate = new Coordinate(Width / 2 - 10 * GameObjectSize - i * 4 * GameObjectSize, Height / 2 + GameObjectSize * 20);
                }
                else
                {
                    // *INDIVIDUAL ASSIGNMENT*
                    // Changed y to make snakes spawn at the bottom.
                    snakeCoordinate = new Coordinate(Width / 2 + 9 * GameObjectSize + i * 4 * GameObjectSize, Height / 2 + GameObjectSize * 20);
                }
                Add(new Snake(snakeStartLength, snakeCoordinate, i + 1, this));
            }
        }

        // This is triggered every time a key is pressed down after the game has been started.
        private void KeyEventHandler(object? sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            foreach (var snake in _snakes)
                _movement.Move(e, snake);
        }

        private void Collide()
        {
            // Try doing this without the temp lists if collisions do not trigger things as they should.
            var snakes = new List<Snake>(_snakes);
            var foodList = new List<Food>(_food);

            foreach (var food in foodList)
                foreach (var snake in snakes)
                    if(food.intersect(snake))
                    food.TryHit(snake);
            foreach (var snake in snakes)
                foreach (var otherSnake in snakes)
                    snake.SnakeCollide(otherSnake);
        }

        private void Move()
        {
            var snakes = new List<Snake>(_snakes);

            // *INDIVIDUAL ASSIGNMENT*
            foreach (var snake in snakes)
            {
                if (snake.Speed % _moveCount == 0) snake.Move(Width, Height);
            }

            _moveCount++;
            if (_moveCount > 10) _moveCount = 1;
        }

        private void TrySpawnFood()
        {
            // This can be changed depending on how much food should be spawned.
            if (_food.Count >= _currentAmountOfPlayers) return;

            var snakes = new List<Snake>(_snakes);
            List<Coordinate> snakeCoords = new List<Coordinate>();
            foreach (var snake in snakes) snakeCoords.AddRange(snake.GetBodyCords());

            Coordinate foodCoordinate;
            Random rand = new Random();
            do
            {
                // This randomization should use the min and max x-y values of the game area.
                foodCoordinate = new Coordinate(rand.Next(0, Width - GameObjectSize), rand.Next(0, Height - GameObjectSize));
            } while (snakeCoords.Contains(foodCoordinate));

          
            Add(new Food(foodCoordinate.X, foodCoordinate.Y, this));
        }

        private void GameOver()
        {
            _snakes.Clear();
            _food.Clear();
            _timer.Stop();
            _form.RestartMenu();
        }

        public string GetScoreString()
        {
            var snakes = new List<Snake>(_snakes);
            string scoreString = "";
            foreach (var snake in snakes)
                scoreString += $"Player {snake.ID}: {snake.Points}\r\n";
            if (_currentAmountOfPlayers == 1 && AmountOfPlayers > 1)
                scoreString += "WINNER!";
            return scoreString;
        }
    }
}