﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class Explosion : Component
    {
        private Vector2 spawnPosition;

        public Explosion(Vector2 position)
        {
            this.spawnPosition = position;
        }

        public override void Awake()//Порождение аниматора и вызов анимации
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.ScaleFactor = 0.5f;
            GameObject.Transform.Position = spawnPosition;
            GameObject.Transform.Position += new Vector2(-25, -25);
            Animator animator = (Animator)GameObject.GetComponent("Animator");
            animator.AddAnimation(new Animation("Explosion", 15));
            animator.PlayAnimation("Explosion");
            animator.AnimationDoneEvent += OnAnimationDone;
        }

        public void OnAnimationDone(string name)//Если анимация закончена - уничтожение объекта
        {
            GameObject.Destroy();
        }
    }
}
