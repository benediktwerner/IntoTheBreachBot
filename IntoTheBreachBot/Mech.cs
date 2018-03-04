namespace IntoTheBreachBot
{
    public enum Weapon
    {
        Punch, Shoot, Artillery
    }

    public struct Mech : Entity
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

        public bool HasMoved;
        public bool HasActed;

        public int MaxHealth;
        public int Moves;
        public bool Armor;

        public Weapon Weapon;
    }

    public static class MechExtensionMethods
    {
        public static void ExecuteAttack(this Mech mech, GameState gameState, MechAction action)
        {
            switch (mech.Weapon)
            {
                case Weapon.Punch:
                    gameState.Damage(action.targetPosition, 2, mech.Position.DirectionTo(action.targetPosition));
                    break;
                case Weapon.Shoot:
                    gameState.Damage(action.targetPosition, 1, mech.Position.DirectionTo(action.targetPosition));
                    break;
                case Weapon.Artillery:
                    gameState.Damage(action.targetPosition, 1);
                    gameState.Damage(new Position(action.targetPosition, 0, -1), 0, Direction.North);
                    gameState.Damage(new Position(action.targetPosition, 1, 0), 0, Direction.East);
                    gameState.Damage(new Position(action.targetPosition, 0, 1), 0, Direction.South);
                    gameState.Damage(new Position(action.targetPosition, -1, 0), 0, Direction.West);
                    break;
            }
        }
    }
}
