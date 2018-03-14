using System;

namespace IntoTheBreachBot
{
    public enum ActionType
    {
        Attack, Move, Repair
    }

    public struct MechAction
    {
        public int MechIndex;
        public Position TargetPosition;
        public ActionType ActionType;

        public override string ToString()
        {
            switch (ActionType)
            {
                case ActionType.Attack:
                    return $"Mech {MechIndex} attacks {TargetPosition}";
                case ActionType.Move:
                    return $"Mech {MechIndex} moves to {TargetPosition}";
                case ActionType.Repair:
                    return $"Mech {MechIndex} repairs himself";
            }
            throw new InvalidOperationException("Invalid ActionType: " + ActionType);
        }
    }
}
