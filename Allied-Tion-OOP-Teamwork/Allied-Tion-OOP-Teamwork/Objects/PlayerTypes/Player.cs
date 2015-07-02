using System;
using System.Collections.Generic;
using AlliedTionOOP.GUI.IngameGraphics;
using AlliedTionOOP.Interfaces;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.Objects.PlayerTypes
{
    public abstract class Player : Creature, ICollect, IExperienceEarnable, IHeal, ISpeed
    {
        private const int PlayerTopLeftX = 5;
        private const int PlayerTopLeftY = 5;

        private int levelUpExperience = 150;

        private readonly List<Item> inventory;

        protected Player(Texture2D image, int energy, int focus, int speed)
            : base(image, PlayerTopLeftX, PlayerTopLeftY, energy, focus, 0)
        {
            this.CurrentEnergy = energy;
            this.TotalEnergy = energy;
            this.TotalFocus = focus;
            this.CurrentFocus = focus;
            this.Experience = 0;
            this.CurrentLevel = 1;
            this.inventory = new List<Item>();
            this.Speed = new Point(speed, speed);
        }

        public Point Speed { get; set; }

        public IEnumerable<Item> Inventory
        {
            get { return this.inventory; }
        }

        public int LevelUpExperience
        {
            get { return this.levelUpExperience; }
        }

        public int Experience { get; set; }

        public int CurrentLevel { get; set; }

        public void AddItemToInventory(Item itemToAdd)
        {
            this.inventory.Add(itemToAdd);
        }

        public void LevelUp()
        {
            this.CurrentLevel++;
            this.levelUpExperience += 150;
            this.Experience -= this.CurrentLevel * 150;
            this.TotalFocus += 10;
            this.TotalEnergy += 10;
            this.CurrentEnergy += 15;
            this.CurrentFocus += 15;
        }

        public void GetFocus(Beer beer)
        {
            this.CurrentFocus = Math.Min(this.TotalFocus, this.CurrentFocus + beer.FocusRestore);
            this.inventory.Remove(beer);
        }

        public void DiskUpgrade(DiskUpgrade disk)
        {
            this.CurrentFocus = Math.Min(this.TotalFocus, this.CurrentFocus + disk.FocusIncrease);
            this.inventory.Remove(disk);
        }

        public void MemoryUpgrade(MemoryUpgrade memory)
        {
            this.CurrentFocus = Math.Min(this.TotalFocus, this.CurrentFocus + memory.FocusIncrease);
            this.inventory.Remove(memory);
        }

        public void NakovBook(NakovBook book)
        {
            this.CurrentFocus = Math.Min(this.TotalFocus, this.CurrentFocus + book.FocusIncrease);
            this.inventory.Remove(book);
        }

        public void GetEnergy(RedBull redbull)
        {
            this.CurrentEnergy = Math.Min(this.TotalEnergy, this.CurrentEnergy + redbull.EnergyRestore);
            this.inventory.Remove(redbull);
        }

        public void ProcessorUpgrade(ProcessorUpgrade processor)
        {
            this.CurrentEnergy = Math.Min(this.TotalEnergy, this.CurrentEnergy + processor.EnergyIncrease);
            this.inventory.Remove(processor);
        }

        public void Resharper(Resharper resharper)
        {
            this.CurrentEnergy = Math.Min(this.TotalEnergy, this.CurrentEnergy + resharper.EnergyIncrease);
            this.inventory.Remove(resharper);
        }

        public override void Attack(Creature enemy)
        {
            base.Attack(enemy);

            this.Experience += enemy.ExperienceToGive;

            if (this.Experience >= this.levelUpExperience)
            {
                LevelUp();
            }
        }
    }
}
