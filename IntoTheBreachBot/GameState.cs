using System;

namespace IntoTheBreachBot
{
    public class GameState
    {
        public Cell[] Board;
        public int PowerGrid;

        public Mech[] Mechs;
        public Enemy[] Enemies;

        public GameState Clone()
        {
            return new GameState
            {
                Board = (Cell[])Board.Clone(),
                PowerGrid = PowerGrid,
                Mechs = (Mech[])Mechs.Clone(),
                Enemies = (Enemy[])Enemies.Clone()
            };
        }

        public void ExecuteAction(MechAction action)
        {
            var mech = Mechs[action.mechIndex];

            switch (action.actionType)
            {
                case MechAction.ActionType.Attack:
                    mech.ExecuteAttack(this, action);
                    mech.HasActed = true;
                    break;

                case MechAction.ActionType.Repair:
                    if (mech.Health < mech.MaxHealth)
                        mech.Health++;
                    mech.HasActed = true;
                    break;

                case MechAction.ActionType.Move:
                    mech.Position = action.targetPosition;
                    mech.HasMoved = true;
                    break;
            }
        }

        public int ComputeEndOfTurn()
        {
            // TODO
            return 0;
        }

        public Cell GetCell(Position pos)
        {
            return Board[pos.x + pos.y * 8];
        }

        public Entity GetEntityOnCell(Cell cell)
        {
            if (cell.EnemyIndex >= 0)
                return Enemies[cell.EnemyIndex];
            else if (cell.MechIndex >= 0)
                return Mechs[cell.MechIndex];
            throw new InvalidOperationException("Cell does not contain an entity");
        }

        public void Damage(Position targetPosition, int amount, Direction pushDirection = Direction.None, CellModifiers modifiers = CellModifiers.None)
        {
            Cell targetCell = GetCell(targetPosition);
            if (!targetCell.HasEntity())
                return;

            Entity entity = GetEntityOnCell(targetCell);

            Position pushedPosition = pushDirection.PushPosition(targetPosition);
            if (pushedPosition != targetPosition)
            {
                Cell pushedCell = GetCell(pushedPosition);
                if (pushedCell.HasEntity())
                {
                    Entity otherEntity = GetEntityOnCell(pushedCell);
                    otherEntity.Damage(1);
                }
                else if (pushedCell.IsWalkable())
                {
                    entity.Position = pushedPosition;
                }
                else
                {
                    entity.Damage(1);
                    PowerGrid -= pushedCell.Damage(1, CellModifiers.None);
                }
            }

            entity.Damage(amount);
            targetCell.Damage(amount, modifiers);
            entity.ProcessEnterCell(GetCell(entity.Position));
        }
    }
}
