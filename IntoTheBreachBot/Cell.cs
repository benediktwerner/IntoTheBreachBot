using System;

namespace IntoTheBreachBot
{
    public enum CellType
    {
        Empty, Plain, Water, Mountain, CrackedMountain, Forest, Dessert
    }

    [Flags]
    public enum CellModifiers
    {
        None = 0,
        Fire = 1 << 0,
        Acid = 1 << 1,
        Smoke = 1 << 2,
        UpcomingEnemy = 1 << 3
    }

    public struct Cell
    {
        public CellType CellType;
        public CellModifiers Modifiers;
        public int Buildings;
        public int MechIndex;
        public int EnemyIndex;
    }

    public static class CellExtensionMethods
    {
        // Returns grid damage
        public static int Damage(this Cell cell, int amount, CellModifiers modifiers = CellModifiers.None)
        {
            if (amount > 0)
            {
                switch (cell.CellType)
                {
                    case CellType.Mountain:
                        cell.CellType = CellType.CrackedMountain;
                        break;
                    case CellType.CrackedMountain:
                        cell.CellType = CellType.Plain;
                        break;
                    case CellType.Forest:
                        cell.CellType = CellType.Plain;
                        cell.Modifiers |= CellModifiers.Fire;
                        break;
                    case CellType.Dessert:
                        cell.CellType = CellType.Plain;
                        cell.Modifiers |= CellModifiers.Smoke;
                        break;
                }
            }

            int gridDamage = Math.Min(amount, cell.Buildings);
            cell.Buildings -= gridDamage;
            cell.Modifiers |= modifiers;
            return gridDamage;
        }

        public static bool HasEntity(this Cell cell)
        {
            return cell.MechIndex >= 0 || cell.EnemyIndex >= 0;
        }

        public static bool IsWalkable(this Cell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Plain:
                    return cell.Buildings == 0;
                case CellType.Forest:
                case CellType.Dessert:
                case CellType.Water:
                    return true;
                case CellType.Empty:
                case CellType.Mountain:
                case CellType.CrackedMountain:
                    return false;
            }
            throw new InvalidOperationException("Invalid cell type: " + cell.CellType);
        }
    }
}
