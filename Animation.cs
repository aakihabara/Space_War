using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Animation
    {
        public Image[] Sprites { get; private set; }

        public string Name { get; private set; }

        public float Speed { get; private set; }

        public Animation(string dirName, float speed)//Задаёт название директории с анимацией и скорость анимации
        {
            this.Name = dirName;
            this.Speed = speed;

            string[] paths = Directory.GetFiles($@"sprites/{dirName}");//Берёт все файлы из папки анимации

            Sprites = new Image[paths.Length];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = Image.FromFile(paths[i]);
            }
        }
    }
}
