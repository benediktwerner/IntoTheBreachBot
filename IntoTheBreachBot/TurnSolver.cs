using System.Collections.Generic;
using System.Linq;

namespace IntoTheBreachBot
{
    public class TurnSolver
    {
        List<MechAction> actions = new List<MechAction>(6);
        GameState gameState;

        int bestActionsScore;
        MechAction[] bestActions;

        public MechAction[] SolveTurn(GameState gameState)
        {
            this.gameState = gameState;
            NextAction();
            return bestActions.ToArray();
        }

        void NextAction()
        {
            List<MechAction> availableActions = new List<MechAction>();

            foreach (var mech in gameState.Mechs)
            {
                availableActions.AddRange(GetAvailableActions(mech));
            }

            if (availableActions.Count == 0)
            {
                int score = gameState.ComputeEndOfTurn();
                if (score > bestActionsScore)
                {
                    bestActionsScore = score;
                    bestActions = actions.ToArray();
                }
                return;
            }

            GameState previousGameState = gameState.Clone();
            foreach (MechAction action in availableActions)
            {
                gameState.ExecuteAction(action);
                actions.Add(action);

                NextAction();

                actions.RemoveAt(actions.Count - 1);
                gameState = previousGameState.Clone();
            }
        }

        List<MechAction> GetAvailableActions(Mech mech)
        {
            // TODO
            return null;
        }
    }
}
