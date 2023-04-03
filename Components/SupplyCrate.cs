using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    enum SupplyType { Life, Shield }
    class SupplyCrate : Component
    {
        private Vector2 spawnPosition;

        private static Random rnd = new Random();

        private Collider collider;

        private SupplyType supplyType;

        public SupplyCrate(Vector2 position)
        {
            this.spawnPosition = position;
            supplyType = (SupplyType)rnd.Next(0, 2);
        }

        public override void Awake()
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.SortOrder = 1;
            spriteRenderer.ScaleFactor = 0.5f;
            spriteRenderer.SetSprite("supply-crate");
            GameObject.Transform.Position = spawnPosition;
            GameObject.Transform.Position += new Vector2(15, 15);
            collider = (Collider)GameObject.GetComponent("Collider");
            collider.CollisionHandler += Collision;
        }

        private void Collision(Collider other)//Проверяет, если игрок соприкасается с ящиком, то ящик удаляется
        {
            if (other.GameObject.Tag == "Player")
            {
                switch (supplyType)
                {
                    case SupplyType.Life:
                        GameManager.AddLife();//Прибавка 1 жизни
                        break;
                    case SupplyType.Shield:
                        (other.GameObject.GetComponent("Player") as Player).ApplyShield();//Добавление игроку щита (может быть несколько)
                        break;
                }

                GameObject.Destroy();
            }
        }
    }
}
