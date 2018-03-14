namespace IntoTheBreachBot
{
    public enum Weapon
    {
        Punch, Shoot, Artillery
    }

    public struct Mech : Entity
    {
        public int Health { get; set; }
        public Position Position { get; set; }
        public StatusEffects StatusEffects { get; set; }
        public EntityProperties EntityProperties { get; set; }

        public bool HasMoved;
        public bool HasShot;

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
