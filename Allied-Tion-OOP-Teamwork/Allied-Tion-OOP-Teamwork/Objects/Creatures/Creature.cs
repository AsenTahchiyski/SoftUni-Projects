using System;
using AlliedTionOOP.Interfaces;
using AlliedTionOOP.Objects.PlayerTypes;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.Objects.Creatures
{
    public abstract class Creature : Object, IDestroyable, IMoveable, IExperienceGiving, IAttack
    {
        private bool isAlive;

        protected Creature(Texture2D image, int topLeftX, int topLeftY, int energy, int focus, int experienceToGive)
            : base(image, topLeftX, topLeftY)
        {
            this.CurrentEnergy = energy;
            this.TotalEnergy = energy;
            this.TotalFocus = focus;
            this.CurrentFocus = focus;
            this.ExperienceToGive = experienceToGive;
            this.IsAlive = true;
        }

        public bool IsAlive
        {
            get { return CurrentFocus > 0 && CurrentEnergy > 0; }
            set { this.isAlive = value; }
        }

        public int TotalEnergy { get; set; } // Total Damage
        
        public int CurrentEnergy { get; set; } // Damage

        public int TotalFocus { get; set; }  // Total Health

        public int CurrentFocus { get; set; } // Current Health
        
        public int ExperienceToGive { get; private set; }

        public virtual void Attack(Creature enemy)
        {
            enemy.CurrentFocus = Math.Max(0,enemy.CurrentFocus - this.CurrentFocus);
            enemy.CurrentEnergy = Math.Max(0,enemy.CurrentEnergy - this.CurrentEnergy);
            
            if (enemy.CurrentFocus <= 0 || enemy.CurrentEnergy <= 0)
            {
                enemy.IsAlive = false;
            }
        }

        public void Move(int assignToPositionX, int assignToPositionY)
        {
            this.TopLeftX += assignToPositionX;
            this.TopLeftY += assignToPositionY;
        }
    }
}
