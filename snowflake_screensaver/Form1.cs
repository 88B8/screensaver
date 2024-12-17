using System;
using System.Drawing.Drawing2D;
using Timer = System.Timers.Timer;

namespace snowflake_screensaver
{
    public partial class Form1 : Form
    {
        private List<Snowflake> Snowflakes = new List<Snowflake>();
        private Timer timer;
        private int Count = 100;

        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();

            for (int i = 0; i < Count; i++)
            {
                Snowflakes.Add(new Snowflake());
            }

            timer = new Timer();
            timer.Interval = 25;
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var snowflake in Snowflakes)
            {
                snowflake.Y += snowflake.Speed;

                if (snowflake.Y > this.ClientSize.Height)
                {
                    snowflake.SpawnSnowflake();
                }
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var snowflake in Snowflakes)
            {
                g.DrawImage(snowflake.Image, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }
        }

        public class Snowflake
        {
            public Random rand = new Random();
            public int X { get; private set; }
            public float Y { get; set; }
            public float Speed { get; private set; }
            public int Size { get; private set; }
            public Image Image { get; private set; }

            public Snowflake()
            {
                Image = Properties.Resources.snowflake;
                Size = rand.Next(20, 100);
                SpawnSnowflake();
            }

            public void SpawnSnowflake()
            {
                this.X = rand.Next(0, Screen.PrimaryScreen.Bounds.Width);
                this.Y = rand.Next(0, 150);
                this.Speed = (float)rand.Next(1, 4) / 2;
            }
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
