namespace IntoTheBreachBot
{
    public enum Direction
    {
        North, East, South, West, None = -1
    }

    public static class DirectionExtensionMethods
    {
        public static Position PushPosition(this Direction direction, Position pos)
        {
            switch (direction)
            {
                case Direction.North:
                    if (pos.y == 0) break;
                    return new Position(pos.x, pos.y - 1);
                case Direction.East:
                    if (pos.x == 7) break;
                    return new Position(pos.x + 1, pos.y);
                case Direction.South:
                    if (pos.y == 7) break;
                    return new Position(pos.x, pos.y + 1);
                case Direction.West:
                    if (pos.x == 0) break;
                    return new Position(pos.x - 1, pos.y);
            }
            return pos;
        }

        public static Direction DirectionTo(this Position startPosition, Position targetPosition)
        {
            if (targetPosition.x > startPosition.x)
                return Direction.East;
            if (targetPosition.x < startPosition.x)
                return Direction.West;
            if (targetPosition.y > startPosition.y)
                return Direction.South;
            if (targetPosition.y < startPosition.y)
                return Direction.North;
            return Direction.None;
        }
    }
}
