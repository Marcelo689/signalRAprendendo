using GameHeros.Hubs;

namespace GameHeros.Models
{
    public abstract class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int AttackPoints { get; set; }
        public int Heal { get; set; }
        public bool Invunerable { get; set; }
        public bool Alive { get; set; }
        public string HeroImage { get; set; }
        public List<Skill> HeroSkills { get; set; } = new List<Skill>();    
        public void Initialize(int id, string name, int attack, int health)
        {
            this.Id = id;
            this.Name = name;
            AttackPoints = attack;
            HealthPoints = health;
            this.Alive = true;

        }

        public class Skill
        {
            public Skill()
            {
                if(ColdownRemaining > 0) ColdownRemaining -= 1;
            }
            public int Id { get; set; } 
            public string Name { get; set; }
            public string Image { get; set; }   
            public int Damage { get; set; }
            public int Defense { get; set; }    
            public int AttackPoints { get; set;}
            public int Heal { get; set; }
            public bool Invunerable { get; set; }
            public int Coldown { get; set; }
            public int ColdownRemaining { get; set; }

            public void Use(Hero player1, Hero player2)
            {
                if(Defense > 0)
                    player1.Defense      += this.Defense;
                if (AttackPoints > 0)
                    player1.AttackPoints += this.AttackPoints;
                if (player2.Damage > 0)
                    player2.Damage       += this.Damage;
                if (Heal > 0)
                    player1.HealthPoints += this.Heal;
                if (Invunerable)
                    player1.Invunerable  = player1.Invunerable;

                ColdownRemaining = Coldown;
                if (ColdownRemaining > 0)
                    ColdownRemaining -= 1;

            }
        }
    }
}
