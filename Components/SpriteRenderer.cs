using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Components
{
    class SpriteRenderer : Component
    {
        private Graphics graphics;

        private Image sprite;

        public float ScaleFactor { get; set; } = 1;

        private bool isVisible;

        public int SortOrder { get; set; }

        public RectangleF Rectangle //Установка стандартной позиции и размера объекта
        {
            get
            {
                return new RectangleF(GameObject.Transform.Position.X, GameObject.Transform.Position.Y, sprite.Width * ScaleFactor, sprite.Height * ScaleFactor);
            }
        }

        public Image Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public SpriteRenderer()
        {
            this.graphics = GameWorld.Graphics;
            this.SortOrder = 0;
        }

        public SpriteRenderer(int SortOrder)
        {
            this.graphics = GameWorld.Graphics;
            this.SortOrder = 0;
            this.SortOrder = SortOrder;
        }

        public void SetSprite(string spriteName)//Установка объекту его спрайта
        {
            this.sprite = Image.FromFile($@"sprites/{spriteName}.png");
        }

        public override void Update()
        {
            graphics.DrawImage(sprite, Rectangle);
            ///Если включён дебаггер, то рисует вокруг объекта красный квадрат 
            ///для отображения его размеров на форме
            if (GameWorld.Debug)
            {
                graphics.DrawRectangle(new Pen(Color.Red, 0.5f), new Rectangle((int)Rectangle.X, (int)Rectangle.Y, (int)Rectangle.Width, (int)Rectangle.Height));
            }
        }

        public bool OnBecameInvisible()//Проверяет, если объект на форме, то его статус - Видимый
        {
            if (isVisible)
            {
                if (   GameObject.Transform.Position.Y < -Rectangle.Height
                    || GameObject.Transform.Position.Y > GameWorld.WorldSize.Height
                    || GameObject.Transform.Position.X < -Rectangle.Width
                    || GameObject.Transform.Position.X > GameWorld.WorldSize.Width
                   )
                {
                    isVisible = false;
                    return true;
                }
            }

            isVisible = true;
            return false;
        }

        public override string ToString()
        {
            return "SpriteRenderer";
        }
    }
}
