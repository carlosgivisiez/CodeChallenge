namespace CodeChallenge.Chatroom.Core
{
    public record struct RoomSummary
    {
        public Guid Id;
        public string Name;
        public int MembersCount;

        public RoomSummary(Guid id, string name, int membersCount)
        {
            Id = id;
            Name = name;
            MembersCount = membersCount;
        }
    }
}
