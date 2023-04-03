using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Components
{
    class Player : Component
    {
        private SpriteRenderer spriteRenderer;

        ///<summary>
        /// Примечание
        /// Я не использую аниматор для игрока т.к. в моей
        /// версии курса нет нужных спрайтов для анимации
        ///</summary>

        //private Animator animator;

        private Vector2 velocity;

        private float speed;

        private bool canShoot;

        private float timeSinceLastShot;

        private float shootCoolDown = 1f;//Длительность задержки перед выстрелом

        private Collider collider;

        private bool immortal = false;

        private float immortalDuration = 3f; //Длительность бессмертия

        private float immortalTime;

        private float blinkCooldown = 0.1f;

        private float timeSinceLastBlink;

        public override void Awake()//Метод, который вызывается при создании игрока
        {
            GameObject.Tag = "Player";
            speed = 400;
            canShoot = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            collider = (Collider)GameObject.GetComponent("Collider");
            collider.CollisionHandler += Collision;
            spriteRenderer.SetSprite("player");
            spriteRenderer.ScaleFactor = 0.7f;
            //animator = (Animator)GameObject.GetComponent("Animator");
            //animator.AddAnimation(new Animation("PlayerFly", 10));
            //animator.PlayAnimation("PlayerFly");
            Reset();
        }

        public override void Update()//Все методы, которые вызываются каждый тик
        {
            Immortality();
            GetInput();
            Move();
            ScreenLimits();
            ScreenWarp();
            HandleShootCoolDown();
        }

        private void HandleShootCoolDown()//КД на выстреле, т.к. иначе он будет стрелять каждый тик, а здесь - каждую секунду
        {
            if (!canShoot)
            {
                timeSinceLastShot += OptTime.DeltaTime;
            }
            if(timeSinceLastShot >= shootCoolDown)
            {
                canShoot = true;
            }
        }

        private void GetInput()//Назначение кнопок
        {
            velocity = Vector2.Zero;

            if(Keyboard.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            if (Keyboard.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }

            if (Keyboard.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }

            if (Keyboard.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }
            if (Keyboard.IsKeyDown(Keys.Z))
            {
                Shoot();
            }

            velocity = Vector2.Normalize(velocity);
        }

        private void Move()//Перемещение игрока по форме
        {
            GameObject.Transform.Translate(velocity * speed * OptTime.DeltaTime);
        }

        public void ApplyShield()//Установка игроку щита, который защищает от 1 попадания
        {
            GameObject gO = new GameObject();
            gO.AddComponent(new Shield(GameObject.Transform, new Vector2(-27, -27)));
            gO.AddComponent(new SpriteRenderer(3));
            gO.AddComponent(new Collider());

            GameWorld.Instantiate(gO);
        }

        private void ScreenLimits()//Блокировка перемещения по Y
        {
            if (GameObject.Transform.Position.Y < 0)
            {
                GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, 0);
            }

            if (GameObject.Transform.Position.Y > GameWorld.WorldSize.Height - spriteRenderer.Rectangle.Height)
            {
                GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, GameWorld.WorldSize.Height - spriteRenderer.Rectangle.Height);
            }
        }

        private void ScreenWarp()//Телепорт по X
        {
            if (GameObject.Transform.Position.X + spriteRenderer.Rectangle.Width < 0)
            {
                GameObject.Transform.Position = new Vector2(GameWorld.WorldSize.Width, GameObject.Transform.Position.Y);
            }

            if (GameObject.Transform.Position.X > GameWorld.WorldSize.Width)
            {
                GameObject.Transform.Position = new Vector2(0 - spriteRenderer.Rectangle.Width, GameObject.Transform.Position.Y);
            }
        }

        private void Shoot()//Создание лазера, который летит вперёд
        {
            if (canShoot)
            {
                canShoot = false;
                timeSinceLastShot = 0;
                GameObject laser = new GameObject();
                Vector2 spawnPosition = new Vector2(GameObject.Transform.Position.X + 
                    (spriteRenderer.Rectangle.Width / 2) - 5, GameObject.Transform.Position.Y - 85f);
                laser.AddComponent(new Laser("laser", new Vector2(0, -1), spawnPosition));
                laser.AddComponent(new SpriteRenderer(1));
                laser.AddComponent(new Collider());
                GameWorld.Instantiate(laser);
            }
        }

        /// <summary>
        /// Проверяет, если игрок столкнулся с врагом (И он не бессмертный), то -хп у игрока
        /// до тех пор, пока их не 0, иначе происходит удаление объекта игрока
        /// </summary>
        private void Collision(Collider other)
        {
            if(other.GameObject.Tag == "Enemy" && !immortal)
            {
                immortal= true;
                GameManager.RemoveLife();
                Explode();
                if (GameManager.LifeCount != 0)
                {
                    Reset();
                }
                else
                {
                    RemovePlayer();
                }
            }
        }

        public void Immortality()//Бессмертие игрока на время его перерождения
        {
            if (immortal)
            {
                timeSinceLastBlink += OptTime.DeltaTime;

                if(timeSinceLastBlink >= blinkCooldown)
                {
                    spriteRenderer.IsEnabled = !spriteRenderer.IsEnabled;//Переключаем видимость спрайта
                    timeSinceLastBlink= 0;
                }

                immortalTime += OptTime.DeltaTime;

                if(immortalTime >= immortalDuration)
                {
                    immortal = false;
                    spriteRenderer.IsEnabled = true;
                    immortalTime = 0;
                    timeSinceLastBlink= 0;
                }
            }
        }

        private void Reset()//Перерождение игрока
        {
            GameObject.Transform.Position = new Vector2((GameWorld.WorldSize.Width / 2) -
            (spriteRenderer.Rectangle.Width / 2), (GameWorld.WorldSize.Height) - 
            (spriteRenderer.Rectangle.Height));
        }

        private void Explode()//Вызов анимации взрыва на месте смерти объекта
        {
            GameObject explosion = new GameObject();
            explosion.AddComponent(new SpriteRenderer());
            explosion.AddComponent(new Animator());
            explosion.AddComponent(new Explosion(GameObject.Transform.Position));
            GameWorld.Instantiate(explosion);
        }

        private void RemovePlayer()//Уничтожение объекта
        {
            GameObject.Destroy();
        }

        public override string ToString()
        {
            return "Player";
        }
    }
}
