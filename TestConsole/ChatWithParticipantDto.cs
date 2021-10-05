namespace TestConsole
{
    public class ChatWithParticipantDto
    {
        public ChatWithParticipantDto(int chatId, int participantId)
        {
            ChatId = chatId;
            ParticipantId = participantId;
        }
        
        public int ChatId { get; set; }

        public int ParticipantId { get; set; }
    }
}