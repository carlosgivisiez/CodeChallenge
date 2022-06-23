namespace CodeChallenge.Chatroom.Core
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public Guid OwnerId { get; private set; }
        public DateTime DateTime { get; private set; }
        public Guid? QuoteeMessageId { get; private set; }
        public bool IsCommand => Content.StartsWith("/");

        public Message(string content, Guid ownerId, Guid? quoteeMessageId = null)
        {
            Validate(content, ownerId, quoteeMessageId);

            Id = Guid.NewGuid();
            Content = content;
            OwnerId = ownerId;
            DateTime = DateTime.Now;
            QuoteeMessageId = quoteeMessageId;
        }

        public Message(Guid id, string content, Guid ownerId, DateTime dateTime, Guid? quoteeMessageId = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id not provided");
            }

            Validate(content, ownerId, quoteeMessageId);

            Id = id;
            Content = content;
            OwnerId = ownerId;
            DateTime = dateTime;
            QuoteeMessageId = quoteeMessageId;
        }

        private void Validate(string content, Guid ownerId, Guid? quoteeMessageId)
        {
            if (content.Length < 1)
            {
                throw new ArgumentException("Content must contain at least one letter");
            }

            if (ownerId == Guid.Empty)
            {
                throw new ArgumentException("Owner id not provided");
            }

            if (quoteeMessageId == Guid.Empty)
            {
                throw new ArgumentException("Quotee message id not provided");
            }
        }
    }
}
