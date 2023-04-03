using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class Enemy : Component
    {
        private SpriteRenderer spriteRenderer;

        private float speed;

        private static Random random = new Random();

        private Collider collider;
        
        private Random rnd = new Random();

        ///<summary>
        /// Примечание
        /// Я не использую аниматор для врага т.к. в моей
        /// версии курса нет нужных спрайтов для анимации
        ///</summary>

        //private Animator animator;

        public override void Awake()//Метод, который вызывается при создании врага
        {
            GameObject.Tag = "Enemy";
            speed = 100;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            collider = (Collider)GameObject.GetComponent("Collider");
            collider.CollisionHandler += Collision;
            spriteRenderer.SetSprite("enemy_01");
            spriteRenderer.ScaleFactor = 0.7f;
            //animator = (Animator)GameObject.GetComponent("Animator");
            //animator.AddAnimation(new Animation("EnemyFly", 10));
            //animator.PlayAnimation("EnemyFly");
            GameObject.Transform.Position = new Vector2(random.Next(0, GameWorld.WorldSize.Width - (int)spriteRenderer.Rectangle.Width), -spriteRenderer.Rectangle.Height);
        }


        /// <summary>
        /// Каждый тик враг будет двигаться по заданным правилам
        /// и если он выйдет за пределы формы, то будер перерождаться
        /// </summary>
        public override void Update()
        {
            Move();
            ScreenBounds();
        }
         
        private void Move()//Передвижение врага по полю
        {
            GameObject.Transform.Translate(new Vector2(0, 1) * speed * OptTime.DeltaTime);
        }

        private void ScreenBounds()//Когда враг выходит за границы, он респавнится в случайной точке по X
        {
            if (GameObject.Transform.Position.Y > GameWorld.WorldSize.Height)
            {
                Respawn();
            }
        }

        private void Collision(Collider other)//Проверяет, если во врага попал объект с тэгом "Laser", Лазер уничтожается, а враг респавнится
        {
            if (other.GameObject.Tag == "Laser" || other.GameObject.Tag == "Shield")
            {
                other.GameObject.Destroy();
                Explode();
                Respawn();
            }
        }

        private void Explode()//Вызов анимации взрыва на месте смерти объекта и добавление поинтов игроку
        {
            GameObject explosion = new GameObject();
            explosion.AddComponent(new SpriteRenderer(3));
            explosion.AddComponent(new Animator());
            explosion.AddComponent(new Explosion(GameObject.Transform.Position));

            if (rnd.Next(0, 100) <= 10)
            {
                GameObject supplyCrate = new GameObject();
                supplyCrate.AddComponent(new SpriteRenderer());
                supplyCrate.AddComponent(new SupplyCrate(GameObject.Transform.Position));
                supplyCrate.AddComponent(new Collider());
                GameWorld.Instantiate(supplyCrate);
            }

            GameManager.AddPoints();
            GameWorld.Instantiate(explosion);
        }

        private void Respawn()//Метод респавна бота(По Y он всегда будет ресаться наверху формы)
        {
            int x = random.Next(0, GameWorld.WorldSize.Width - (int)spriteRenderer.Rectangle.Width);
            GameObject.Transform.Position = new Vector2(x, -spriteRenderer.Rectangle.Height);
        }
    }
}
