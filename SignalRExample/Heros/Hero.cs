namespace SignalRExample.Heros
{
    public abstract class Hero
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AttackPoints { get; set; }

        public int HealthPoints { get; set; }

        public bool Alive { get; set; }

        public void Initialize(int id, string name, int attack, int health)
        {
            this.Id = id;
            this.Name = name;
            AttackPoints = attack;
            HealthPoints = health;
            this.Alive = true;
        }
    }
}
