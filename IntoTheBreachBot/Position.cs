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

        public static explicit operator Position(System.Int32 i)
        {
            return new Position(i % 8, i / 8);
        }

        public static explicit operator System.Int32(Position p)
        {
            return p.y * 8 + p.x;
        }

        public override bool Equals(object obj)
        {
            return obj is Position && this == (Position)obj;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            return $"({x}|{y})";
        }
    }
}
