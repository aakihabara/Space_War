using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class Background : Component
    {
        protected SpriteRenderer spriteRenderer;

        private Vector2 startPosition;

        private Image sprite;

        protected float speed;

        private Transform sibling;


        /// <summary>
        /// //Задаём стартовую позицию и выбираем спрайт фона
        /// </summary>
        public Background(string spriteName, Vector2 position, float speed, Transform sibling)
        {
            this.sibling = sibling;
            this.speed = speed;
            this.startPosition = position;
            sprite = Image.FromFile($@"Sprites/{spriteName}.png");
        }

        private void Move()//Движение поля
        {
            GameObject.Transform.Translate(new Vector2(0, 1) * speed * OptTime.DeltaTime);
            if(GameObject.Transform.Position.Y > GameWorld.WorldSize.Height)
            {
                Reset();
            }
        }

        public override void Update()//Обновление поля каждый тик
        {
            Move();
        }

        public virtual void Reset()//Сброс поля (Позиция нашего элемента будет выше указанного sibling)
        {
            GameObject.Transform.Position = new Vector2(0, sibling.Position.Y - spriteRenderer.Sprite.Height);
        }

        public override void Awake()//Устанавливаем нашему объекту стартовую позицию и выбранный спрайт
        {
            this.spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            this.spriteRenderer.Sprite = sprite;
            this.GameObject.Transform.Position = startPosition;
        }
    }
}
