using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    public delegate void AnimationDoneDelegate(string name);
    class Animator : Component
    {
        private SpriteRenderer spriteRenderer;

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        private Animation currentAnimation;

        private float timeElapsed;

        private int currentIndex;

        public override void Awake()
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
        }
        public AnimationDoneDelegate AnimationDoneEvent { get; set; }

        public override void Update()
        {
            if(currentAnimation != null)//Если анимация не закончилась
            {
                timeElapsed += OptTime.DeltaTime;

                currentIndex = (int)(timeElapsed * currentAnimation.Speed);//Текущий индекс спрайта в анимации

                if(currentIndex > currentAnimation.Sprites.Length - 1)//Если закончились спрайты анимации в папке, индекс переходит вначало
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                    OnAnimationDone(currentAnimation.Name);
                }

                spriteRenderer.Sprite = currentAnimation.Sprites[currentIndex];
            }
        }

        public void AddAnimation(Animation animation)//Добавить анимацию в словарь анимаций(для её последующего запуска)
        {
            animations.Add(animation.Name, animation);
        }

        public void PlayAnimation(string animationName)//Запустить требуемую анимацию
        {
            currentAnimation = animations[animationName];

            if(spriteRenderer != null)
            {
                spriteRenderer.Sprite = currentAnimation.Sprites[0];
            }
        }

        public override string ToString()
        {
            return "Animator";
        }

        /// <summary>
        /// Если у нас есть какое-то событие, связанное с окончанием 
        /// анимации, то мы его выполняем
        /// </summary>
        public void OnAnimationDone(string name)
        {
            if(AnimationDoneEvent != null)
            {
                AnimationDoneEvent.Invoke(name);
            }
        }
    }
}
