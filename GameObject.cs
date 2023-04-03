using Game.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class GameObject : IComparable<GameObject>
    {
        //private Image sprite;

        //private Graphics graphics;

        //private Transform transform;

        public Transform Transform { get; private set; }

        public string Tag { get; set; }

        /// <summary>
        /// Этот словарь будет хранить в себе все компоненты класса GameObject
        /// </summary>
        private Dictionary<string, Component> components = new Dictionary<string, Component>();

        public GameObject()
        {
            this.Transform = new Transform();
        }

        //Добавление компонента в словарь компонентов объекта
        public void AddComponent(Component component)
        {
            components.Add(component.ToString(), component);
            component.GameObject = this;
        }

        //Если находит компонент с указаным ключом, то возвращает его
        public Component GetComponent(string component)
        {
            if (components.ContainsKey(component))
            {
                return components[component];
            }

            return null;
        }

        public void Awake()//Вызов метода Awake() каждого компонента объекта
        {
            foreach (Component component in components.Values)
            {
                component.Awake();
            }
        }

        public void Start()//Вызов метода Start() каждого компонента объекта
        {
            foreach (Component component in components.Values)
            {
                if (component.IsEnabled)
                {
                    component.Start();
                }
            }
        }

        public void Destroy()//Уничтожение каждого компонента объекта
        {
            foreach (Component component in components.Values)
            {
                component.Destroy();
            }

            GameWorld.Destroy(this);
        }

        public void Update()//Обновление каждого компонента объекта
        {

            foreach (Component component in components.Values)
            {
                if (component.IsEnabled)
                {
                    component.Update();
                }
            }

        }

        public int CompareTo(GameObject other)//Сравнение по свойству SortOrder объектов
        {
            SpriteRenderer otherRenderer = (SpriteRenderer)other.GetComponent("SpriteRenderer");
            SpriteRenderer renderer = (SpriteRenderer)this.GetComponent("SpriteRenderer");

            if (otherRenderer != null && renderer != null)
            {
                if (renderer.SortOrder > otherRenderer.SortOrder)
                {
                    return 1;
                }
                else if (renderer.SortOrder < otherRenderer.SortOrder)
                {
                    return -1;
                }

                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
