namespace IntoTheBreachBot
{
    public struct MechAction
    {
        public enum ActionType
        {
            Attack, Move, Repair
        }

        public int mechIndex;
        public Position targetPosition;
        public ActionType actionType;
    }
}
