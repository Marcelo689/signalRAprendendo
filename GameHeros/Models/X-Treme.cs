namespace GameHeros.Models
{
    public class Sasuke : Hero
    {
        public Sasuke()
        {
            Initialize(2, "Sasuke", 150, 400);

            HeroImage = "imgs\\sasuke\\hero2.jpg";

            HeroSkills.Add(new Skill
            {
                Id = 1,
                Name = "Susanno",
                Coldown = 5,
                Defense = 400,
                Image = "imgs\\sasuke\\Susanoo.jpg",
            }); 

        }
    }
}
