namespace TestConsole
{
    public class ChatDto
    {
        public ChatDto(int id, int user1Id, int user2Id)
        {
            Id = id;
            User1Id = user1Id;
            User2Id = user2Id;
        }
        public int Id { get; set; }

        public int User1Id { get; set; }

        public int User2Id { get; set; }
    }
}