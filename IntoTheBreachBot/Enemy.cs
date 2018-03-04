namespace IntoTheBreachBot
{
    public enum AttackType
    {
        Web, Spike, Projectile, Artillery
    }

    public struct Enemy : Entity
    {
        private Position position;
        public Position Position { get => position; set => position = value; }

        private int health;
        public int Health { get => health; set => health = value; }

        private bool isAlive;
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private bool isMassive;
        public bool IsMassive { get => isMassive; set => isMassive = value; }

        private bool isFlying;
        public bool IsFlying { get => isFlying; set => isFlying = value; }

        private bool isPoisoned;
        public bool IsPoisoned { get => isPoisoned; set => isPoisoned = value; }

        private bool isBurning;
        public bool IsBurning { get => isBurning; set => isBurning = value; }

        public int AttackDammage;
        public Direction AttackDirection;
        public int AttackDistance;

        public AttackType AttackType;
    }
}
