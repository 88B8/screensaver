using snowflake_screensaver.Properties;
using Timer = System.Timers.Timer;

namespace snowflake_screensaver
{
    /// <summary>
    /// Модель главной формы
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly List<Snowflake> snowflakes = new List<Snowflake>();
        private readonly Timer timer;
        private readonly Bitmap snowflakeBmp;
        private readonly Bitmap background;
        private readonly Bitmap scene;
        private readonly Graphics sceneGraphics;
        private readonly Graphics graphics;
        private Size windowSize;
        private const int SnowflakeCount = 50;
        private const int SnowflakeMaxSpeed = 50;
        private const int SnowflakeSizeStep = 10;

        /// <summary>
        /// ctor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();
            windowSize = WindowState == FormWindowState.Maximized
            ? Screen.PrimaryScreen?.Bounds.Size ?? ClientSize
            : ClientSize;
            GenerateSnowflakes();
            snowflakeBmp = new Bitmap(Resources.snowflake);
            background = new Bitmap(Resources.winter_village);
            scene = new Bitmap(windowSize.Width, windowSize.Height);
            sceneGraphics = Graphics.FromImage(scene);
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += TimerElapsed;
            timer.Start();
            graphics = CreateGraphics();
        }

        private void GenerateSnowflakes()
        {
            for (var i = 0; i < SnowflakeCount; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = Random.Shared.Next(windowSize.Width),
                    Y = Random.Shared.Next(windowSize.Height),
                    Size = Random.Shared.Next(2, SnowflakeMaxSpeed / SnowflakeSizeStep) * SnowflakeSizeStep
                });
            }
        }

        private void MoveSnowflakes()
        {
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += SnowflakeMaxSpeed - snowflake.Size + SnowflakeSizeStep;

                if (snowflake.Y >= windowSize.Height)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = Random.Shared.Next(windowSize.Width);
                }
            }
        }

        private void TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            MoveSnowflakes();
            DrawSnowflakes();
        }

        private void DrawSnowflakes()
        {
            sceneGraphics.DrawImage(background, new Rectangle(0, 0, windowSize.Width, windowSize.Height));
            foreach (var snowflake in snowflakes)
            {
                sceneGraphics.DrawImage(snowflakeBmp, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }

            graphics.DrawImage(scene, new Point(0, 0));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
