namespace IntoTheBreachBot
{
    public enum AttackType
    {
        Web, Spike, Projectile, Artillery
    }

    public struct Enemy : Entity
    {
        public int Health { get; set; }
        public Position Position { get; set; }
        public StatusEffects StatusEffects { get; set; }
        public EntityProperties EntityProperties { get; set; }

        public int AttackDammage;
        public Direction AttackDirection;
        public int AttackDistance;

        public AttackType AttackType;
    }
}
