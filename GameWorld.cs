using Game.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class GameWorld
    {
        public static Graphics Graphics { get; private set; }

        private Color backgroundColor;

        private static List<GameObject> gameObjects = new List<GameObject>();//Список объектов мира

        private BufferedGraphics backBuffer;

        public static Size WorldSize { get; private set; }

        public static bool Debug { get; set; } = false;



        public GameWorld(Rectangle displayRectangle, Graphics graphics)
        {
            WorldSize = displayRectangle.Size;
            backgroundColor = ColorTranslator.FromHtml("#1C0A29");
            this.backBuffer = BufferedGraphicsManager.Current.Allocate(graphics, displayRectangle);
            Graphics = backBuffer.Graphics;
            Initialize();
        }

        public void Initialize()//Порождение объектов
        {
            gameObjects.Clear();
            GameManager.Reset();
            Collider.colliders.Clear();

            GameObject player = new GameObject();
            player.AddComponent(new SpriteRenderer(2));
            player.AddComponent(new Player());
            player.AddComponent(new Collider());
            player.AddComponent(new Animator());
            gameObjects.Add(player);

            GameObject enemy = new GameObject();
            enemy.AddComponent(new SpriteRenderer(1));
            enemy.AddComponent(new Enemy());
            enemy.AddComponent(new Collider());
            enemy.AddComponent(new Animator());
            gameObjects.Add(enemy);

            GameObject background1 = new GameObject();//Создание объекта фона и добавление его в массив объектов проекта
            GameObject background3 = new GameObject();
            GameObject background2 = new GameObject();

            background1.AddComponent(new Background("Bg1_Trans", Vector2.Zero,150, background3.Transform));
            background1.AddComponent(new SpriteRenderer());
            gameObjects.Add(background1);

            background2.AddComponent(new Background("Bg2_Trans", new Vector2(0, -768), 150, background1.Transform));
            background2.AddComponent(new SpriteRenderer());
            gameObjects.Add(background2);

            background3.AddComponent(new Background("Bg3_Trans", new Vector2(0, -768 * 2), 150, background2.Transform));
            background3.AddComponent(new SpriteRenderer());
            gameObjects.Add(background3);

            GameObject smoke1 = new GameObject();
            smoke1.AddComponent(new BackgroundElement("space-smoke_01"));
            smoke1.AddComponent(new SpriteRenderer());
            gameObjects.Add(smoke1);

            //GameObject smoke2 = new GameObject();
            //smoke2.AddComponent(new BackgroundElement("space-smoke_02"));
            //smoke2.AddComponent(new SpriteRenderer());
            //gameObjects.Add(smoke2);

            GameObject planet1 = new GameObject();
            planet1.AddComponent(new BackgroundElement("planet_01"));
            planet1.AddComponent(new SpriteRenderer());
            gameObjects.Add(planet1);

            //GameObject planet2 = new GameObject();
            //planet2.AddComponent(new BackgroundElement("planet_02"));
            //planet2.AddComponent(new SpriteRenderer());
            //gameObjects.Add(planet2);

            for (int i = 0; i < 3; i++)
            {
                GameManager.AddLife();
            }

            //gameObjects.Sort();
            Awake();
            Start();
        }

        private void Awake()//Вызов метода Awake(только 1 раз при создании экземпляра)
        {

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }

        }

        private void Start()//Вызов метода Start(только 1 раз при создании экземпляра)
        {

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }

        }

        public void Update()//Действие, которое происходит каждый тик
        {
            OptTime.CalcDeltaTime();

            Graphics.Clear(backgroundColor);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            for (int i = 0; i < GameManager.UIElements.Count; i++)
            {
                GameManager.UIElements[i].Update();
            }

            backBuffer.Render();

        }

        public static void Instantiate(GameObject gO)//Создание экземпляра объекта
        {
            gO.Awake();
            gO.Start();
            gameObjects.Add(gO);
            gameObjects.Sort();
        }

        public static void Destroy(GameObject gO)//Уничтожение объекта
        {
            gameObjects.Remove(gO);
        }
    }
}
