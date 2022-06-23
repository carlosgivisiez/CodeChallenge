namespace CodeChallenge.Chatroom.Core
{
    public class Room
    {
        public virtual Guid Id { get; private set; }
        public virtual string Name { get; private set; }
        public virtual ICollection<Guid> MemberIds { get; private set; }
        public virtual ICollection<Message> Messages { get; private set; }

        public Room(string name)
        {
            ValidateName(name);

            Id = Guid.NewGuid();
            Name = name;
            MemberIds = new HashSet<Guid>();
            Messages = new HashSet<Message>();
        }

        public Room(Guid id, string name, ICollection<Guid> memberIds, ICollection<Message> messages)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id not provided");
            }

            ValidateName(name);

            Id = id;
            Name = name;
            MemberIds = memberIds.ToHashSet();
            Messages = messages;
        }

        private void ValidateName(string name)
        {
            if (name.Length < 1)
            {
                throw new ArgumentException("Name must contain at least one letter");
            }
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);

            if (Messages.Count > 50)
            {
                var oldest = Messages.MinBy(x => x.DateTime);

                Messages.Remove(oldest!);
            }
        }

        public void AddMember(Guid memberId)
        {
            MemberIds.Add(memberId);
        }

        public void RemoveMember(Guid memberId)
        {
            MemberIds.Remove(memberId);
        }
    }
}
