namespace TestConsole.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChatsStorage
    {
        private readonly List<ChatDto> _storage;

        public ChatsStorage(List<ChatDto> storage)
        {
            _storage = storage;
        }

        public IEnumerable<ChatDto> GetAll(Func<ChatDto, bool>? expression = null)
            => expression is not null
                ? _storage.Where(expression)
                : _storage;

        public IEnumerable<int> GetIds(Func<ChatDto, bool>? expression = null)
            => expression is not null
                ? _storage.Where(expression).Select(c => c.Id)
                : _storage.Select(c => c.Id);

        public void Add(ChatDto chat)
            => _storage.Add(chat);

        public bool UserIsInChat(int chatId, int userId)
            => _storage
                .Any(c => c.Id == chatId &&
                          c.User1Id == userId ||
                          c.User2Id == userId);

        public bool UserAndParticipantAreInChat(int userId, int participantId)
            => _storage
                .Any(c => c.User1Id == userId &&
                          c.User2Id == participantId ||
                          c.User2Id == userId &&
                          c.User1Id == participantId);
    }
}