namespace IntoTheBreachBot
{
    public interface Entity
    {
        Position Position { get; set; }
        int Health { get; set; }
        bool IsAlive { get; set; }
        bool IsMassive { get; set; }
        bool IsFlying { get; set; }
        bool IsPoisoned { get; set; }
        bool IsBurning { get; set; }
    }

    public static class EntityExtensionMethods
    {
        public static void Damage(this Entity entity, int amount)
        {
            entity.Health -= amount;
            if (entity.Health <= 0)
                entity.IsAlive = false;
        }

        public static void ProcessEnterCell(this Entity entity, Cell cell)
        {
            if (!entity.IsFlying && !entity.IsMassive && cell.cellType == CellType.Water)
                entity.IsAlive = false;
            else if (!entity.IsFlying && cell.cellType == CellType.Empty)
                entity.IsAlive = false;
            else if (cell.modifiers.HasFlag(CellModifiers.Acid))
                entity.IsPoisoned = true;
            else if (cell.modifiers.HasFlag(CellModifiers.Fire))
                entity.IsBurning = true;
        }
    }
}
