using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart1
{
    public partial class Deafult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();
            hero.Name = "Hero";
            hero.Health = 100;
            hero.DamageMaximum = 15;
            hero.AttackBonus = false;

            Character monster = new Character();
            monster.Name = "Monster";
            monster.Health = 100;
            monster.DamageMaximum = 12;
            monster.AttackBonus = true;

            Dice dice = new Dice();

            //Bonus Attack
            if (hero.AttackBonus == true)
            {
                monster.Defend(hero.Attack(dice));
                resultLabel.Text = "<strong>Hero has an attack bonus!</strong><br>";
            }
            if (monster.AttackBonus == true)
            {
                hero.Defend(hero.Attack(dice));
                resultLabel.Text = "<strong>Monster has an attack bonus!</strong><br>";
            }


            //While loop
            while (hero.Health > 0 && monster.Health > 0)
            {
                monster.Defend(hero.Attack(dice));
                hero.Defend(hero.Attack(dice));

                displayResults(hero);
                displayResults(monster);
            }

            displayResult(monster, hero);

        }

        private void displayResult(Character opp1, Character opp2)
        {
            string result = "";
            if (opp1.Health <= 0) result = String.Format("{0} wins the battle by defeating {1}!", opp2.Name, opp1.Name);
            if (opp2.Health <= 0) result = String.Format("{0} wins the battle by defeating {1}!", opp1.Name, opp2.Name);
            resultLabel.Text += "<br>" + result;        
        }

        private void displayResults(Character character)
        {
            resultLabel.Text += String.Format("Name: {0} - Health: {1} - Damage Maximum: {2} - Attack Bonus: {3}<br><br>", character.Name, character.Health, character.DamageMaximum.ToString(), character.AttackBonus.ToString());
        }

        class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int DamageMaximum { get; set; }
            public bool AttackBonus { get; set; }

            public int Attack(Dice dice)
            {
                dice.Sides = this.DamageMaximum;
                return dice.Roll();
            }

            public void Defend(int damage)
            {
                this.Health -= damage;
            }
        }

        class Dice
        {
            public int Sides { get; set; }

            Random random = new Random();
            public int Roll()
            {
                return random.Next(this.Sides);
            }
        }
    }
}