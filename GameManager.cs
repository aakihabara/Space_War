using Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    static class GameManager
    {
        public static List<GameObject> UIElements { get; set; } = new List<GameObject>();//Список элементов интерфейса

        public static List<GameObject> lives = new List<GameObject>();

        public static int LifeCount { get; set; }

        private static float xOffset = 10;//Расстояние между объектами

        private static GameObject currentLife;

        private static Button restartButton;

        private static Label scoreLabel;

        public static int score = 0;

        public static int livesCap = 5;

        public static void Initialize(Button button, Label label)
        {
            restartButton = button;
            scoreLabel = label;
        }



        /// Мы устанавливаем объекту изображение игрока,
        /// задаём ему начальные параметры (разрешение, расположение),
        /// и добавляем его в список объектов интерфейса
        public static void AddLife()//Добавление каждой жизни 
        {
            if(LifeCount == livesCap)
            {
                return;
            }
            currentLife = new GameObject();
            SpriteRenderer sR = new SpriteRenderer();
            sR.ScaleFactor = 0.5f;
            sR.SetSprite("player");
            currentLife.AddComponent(sR);
            UIElements.Add(currentLife);
            ///Transform нужен для корректного отображения на форме,
            ///т.к. если мы этого не сделаем, объекты просто будут наложены друг на друга
            currentLife.Transform.Translate(new Vector2(sR.Sprite.Width * sR.ScaleFactor * LifeCount + xOffset, 10));
            xOffset += 5;
            LifeCount++;
            lives.Add(currentLife);
        }

        public static void RemoveLife()//Удаление одной жизни игрока
        {
            UIElements.Remove(currentLife);

            LifeCount--;

            if(LifeCount > 0)
            {
                currentLife = lives[LifeCount - 1];
            }
            else
            {
                //Запуск окончания игры
                GameOver();
            }
        }

        public static void AddPoints()//Счётчик убийств
        {
            score++;
            scoreLabel.Text = "score: " + score.ToString();
        }

        public static void GameOver()//Показ кнопки и фона "GameOver"
        {
            GameObject gameOver = new GameObject();
            SpriteRenderer sR = new SpriteRenderer();
            sR.SetSprite("GameOver");
            gameOver.AddComponent(sR);
            UIElements.Add(gameOver);
            restartButton.Show();
        }

        public static void Reset()//Обнуление всех переменных при перезапуске и начале игры
        {
            xOffset = 10;
            UIElements.Clear();
            LifeCount = 0;
            lives.Clear();
            restartButton.Hide();
            score = 0;
            scoreLabel.Text = "score: " + score.ToString();
        }
    }
}
