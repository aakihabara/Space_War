using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    public delegate void CollisionEventHandler(Collider other);
    public class Collider : Component
    {
        private SpriteRenderer spriteRenderer;

        public event CollisionEventHandler CollisionHandler;

        public static List<Collider> colliders { get; set; } = new List<Collider>();

        public List<Collider> otherColliders = new List<Collider>();

        public override void Awake()
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");

            colliders.Add(this);
        }

        public override void Update()//Проверка объектов на столкновение
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                OnCollision(colliders[i]);
            }
        }

        /// <summary>
        /// Если 2 разных объекта столкнулись, 
        /// то мы выполняем соответствующий делегат
        /// </summary>
        private void OnCollision(Collider other)
        {
            RectangleF intersection = RectangleF.Intersect(spriteRenderer.Rectangle, other.spriteRenderer.Rectangle);
            if (other != this)
            {
                    if ((intersection.Width > 0 || intersection.Height > 0) && CollisionHandler != null)
                    {
                        otherColliders.Add(other);
                        CollisionHandler.Invoke(other);
                        otherColliders.Remove(other);
                    }
                    if ((intersection.Width <= 0 || intersection.Height <= 0) && CollisionHandler != null)
                    {
                        otherColliders.Remove(other);
                    }
            }
        }

        public override void Destroy()
        {
            colliders.Remove(this);
        }

        public override string ToString()
        {
            return "Collider";
        }
    }
}
