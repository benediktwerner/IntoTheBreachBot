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
            var mech = Mechs[action.MechIndex];

            switch (action.ActionType)
            {
                case ActionType.Attack:
                    mech.ExecuteAttack(this, action);
                    mech.HasShot = true;
                    break;

                case ActionType.Repair:
                    if (mech.Health < mech.MaxHealth)
                        mech.Health++;
                    mech.HasShot = true;
                    break;

                case ActionType.Move:
                    mech.Position = action.TargetPosition;
                    mech.HasMoved = true;
                    break;
            }
        }

        const int SCORE_GAME_OVER = -1000;

        public int ComputeEndOfTurn()
        {
            /*
             * TODO: Simulate End of Turn
             *  1. Fire Damage
             *  (2. Environment, pass as argument)
             *  3. Vek Attack (in order of array, check if alive)
             *  4. Vek Spawning
             */

            if (PowerGrid <= 0)
                return SCORE_GAME_OVER;

            /*
             * TODO: Score factors (in order of importance):
             *  (- Subgoals, pass as argument)
             *  - Power Grid
             *  - Vek Alive and Vek HP
             *  - Mech HP
             */

            return 0;
        }

        public Cell GetCell(Position pos)
        {
            return Board[(int) pos];
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
            {
                targetCell.Damage(amount, modifiers);
                return;
            }

            Entity entity = GetEntityOnCell(targetCell);

            Position pushedPosition = pushDirection.PushPosition(targetPosition);
            if (pushedPosition != targetPosition)
            {
                Cell pushedCell = GetCell(pushedPosition);
                if (pushedCell.HasEntity())
                {
                    Entity otherEntity = GetEntityOnCell(pushedCell);
                    otherEntity.Damage(1);
                    entity.Damage(1);
                }
                else if (pushedCell.IsWalkable())
                {
                    entity.Position = pushedPosition;
                }
                else
                {
                    entity.Damage(1);
                    PowerGrid -= pushedCell.Damage(1);
                }
            }

            entity.Damage(amount);
            targetCell.Damage(amount, modifiers);
            entity.ProcessEnterCell(GetCell(entity.Position)); // Handles water, fire, acid, etc.
        }
    }
}
