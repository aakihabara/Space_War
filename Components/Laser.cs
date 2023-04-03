using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class Laser : Component
    {
        private SpriteRenderer spriteRenderer;

        private float speed;

        private string laserSprite;

        private Vector2 direction;

        private Vector2 startPosition;

        public Laser(string laserSprite, Vector2 direction, Vector2 startPosition)
        {
            this.laserSprite = laserSprite;
            this.direction = direction;
            this.startPosition = startPosition;
        }

        public override void Awake()//Метод, который вызывается при создании лазера
        {
            GameObject.Tag = "Laser";
            speed = 500;
            GameObject.Transform.Position = startPosition;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.SetSprite(laserSprite);
            spriteRenderer.ScaleFactor = 0.5f;
        }

        /// <summary>
        /// Каждый тик лазер будет выполнять метод Move
        /// и если он выйдет за пределы видимости формы, 
        /// то будет уничтожен
        /// </summary>
        public override void Update()
        {
            Move();
            if (spriteRenderer.OnBecameInvisible())
            {
                GameObject.Destroy();
            }
        }

        public void Move()//То, в какое направлние летит и с какой скоростью
        {
            GameObject.Transform.Translate(direction * speed * OptTime.DeltaTime);
        }
    }
}
