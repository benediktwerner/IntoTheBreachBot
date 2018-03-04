using System;

namespace IntoTheBreachBot
{
    public struct Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Position(Position pos, int xDiff, int yDiff)
        {
            x = pos.x + xDiff;
            y = pos.y + yDiff;
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }
    }

    public enum CellType
    {
        Empty, Plain, Water, Mountain, BrokenMountain, Forest, Dessert,
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
        public CellType cellType;
        public CellModifiers modifiers;
        public int buildings;
        public int mechIndex;
        public int enemyIndex;
    }

    public static class CellExtensionMethods
    {
        // Returns grid damage
        public static int RecieveDamage(this Cell cell, int amount, CellModifiers modifiers)
        {
            if (amount > 0)
            {
                switch (cell.cellType)
                {
                    case CellType.Mountain:
                        cell.cellType = CellType.BrokenMountain;
                        break;
                    case CellType.BrokenMountain:
                        cell.cellType = CellType.Plain;
                        break;
                    case CellType.Forest:
                        cell.cellType = CellType.Plain;
                        cell.modifiers |= CellModifiers.Fire;
                        break;
                    case CellType.Dessert:
                        cell.cellType = CellType.Plain;
                        cell.modifiers |= CellModifiers.Smoke;
                        break;
                }
            }

            int gridDamage = Math.Min(amount, cell.buildings);
            cell.buildings -= gridDamage;
            cell.modifiers |= modifiers;
            return gridDamage;
        }

        public static bool HasEntity(this Cell cell)
        {
            return cell.mechIndex < 0 && cell.enemyIndex < 0;
        }

        public static bool IsWalkable(this Cell cell)
        {
            switch (cell.cellType)
            {
                case CellType.Plain:
                case CellType.Forest:
                case CellType.Dessert:
                case CellType.Water:
                    return cell.buildings == 0;
                default:
                    return false;
            }
        }
    }
}
