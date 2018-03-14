using System;

namespace IntoTheBreachBot
{
    [Flags]
    public enum StatusEffects
    {
        None = 0,
        Burning = 1 << 0,
        Poisoned = 1 << 1,
        Webbed = 1 << 2
    }

    [Flags]
    public enum EntityProperties
    {
        None = 0,
        Massive = 1 << 0,
        Flying = 1 << 1
    }

    public interface Entity
    {
        int Health { get; set; }
        Position Position { get; set; }
        StatusEffects StatusEffects { get; set; }
        EntityProperties EntityProperties { get; set; }
    }

    public static class EntityExtensionMethods
    {
        public static bool IsAlive(this Entity entity) => entity.Health > 0;

        public static bool IsBurning(this Entity entity) => entity.StatusEffects.HasFlag(StatusEffects.Burning);
        public static bool IsPoisoned(this Entity entity) => entity.StatusEffects.HasFlag(StatusEffects.Poisoned);
        public static bool IsWebbed(this Entity entity) => entity.StatusEffects.HasFlag(StatusEffects.Webbed);

        public static bool IsMassive(this Entity entity) => entity.EntityProperties.HasFlag(EntityProperties.Massive);
        public static bool IsFlying(this Entity entity) => entity.EntityProperties.HasFlag(EntityProperties.Flying);

        public static void Damage(this Entity entity, int amount)
        {
            entity.Health -= amount;
        }

        public static void ProcessEnterCell(this Entity entity, Cell cell)
        {
            if (!entity.IsFlying())
            {
                if (!entity.IsMassive() && cell.CellType == CellType.Water)
                {
                    entity.Health = 0;
                    return;
                }
                if (cell.CellType == CellType.Empty)
                {
                    entity.Health = 0;
                    return;
                }
            }
            if (cell.Modifiers.HasFlag(CellModifiers.Acid))
                entity.StatusEffects |= StatusEffects.Poisoned;
            if (cell.Modifiers.HasFlag(CellModifiers.Fire))
                entity.StatusEffects |= StatusEffects.Burning;
        }
}
}
