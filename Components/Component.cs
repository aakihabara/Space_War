using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    public abstract class Component
    {
        public GameObject GameObject { get; set; }

        public bool IsEnabled { get; set; } = true;

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
