using System.Runtime.CompilerServices;
using static GameHeros.Models.Hero;

namespace GameHeros.Models
{
    public class Naruto : Hero
    {
        public Naruto()
        {
            Initialize(1, "Naruto", 100, 500);

            HeroImage = "imgs\\naruto\\hero1.webp";

            HeroSkills.Add(new Skill
            {
                Id = 1,
                Damage = 200,
                Coldown = 2,
                Name = "Gigant Rasegan",
                Image = "imgs\\naruto\\gigantrasegan.webp",
            });
            HeroSkills.Add(new Skill
            {
                Id = 2,
                Name = "Shadow Clone",
                Coldown = 1,
                AttackPoints = 50,
                Defense = 50,
                Image = "imgs\\naruto\\shadownclone.png",
            });
            HeroSkills.Add(new Skill
            {
                Id = 3,
                AttackPoints = 100,
                Defense = 100,
                Heal = 250,
                Coldown = 5,
                Name = "Kyuubi Boost",
                Image = "imgs\\naruto\\kyuubiboost.webp",
            });
        }
    }
}
