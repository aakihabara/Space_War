﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class Shield : Component
    {
        SpriteRenderer spriteRenderer;

        private Transform parent;

        private Vector2 shieldOffset;

        public Shield(Transform parent, Vector2 shieldOffset)
        {
            this.parent = parent;
            this.shieldOffset = shieldOffset;
        }

        public override void Awake()
        {
            GameObject.Tag = "Shield";
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.SetSprite("shield");
            spriteRenderer.ScaleFactor= 0.7f;
        }

        public override void Update()//Каждый тик он следует за игроком
        {
            GameObject.Transform.Position = parent.Position + shieldOffset;
        }
    }
}
