namespace TestConsole.Services
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class UserChatsStorage
    {
        private List<ChatDto> _mockDb = new();
        private readonly ConcurrentDictionary<int, HashSet<ChatWithParticipantDto>> _storage = new();

        public void InitializeBool(List<ChatDto> chats)
        {
            _mockDb = new List<ChatDto>();
            foreach (var c in chats)
            {
                _ = (_storage
                         .TryAdd(c.User1Id, new HashSet<ChatWithParticipantDto> {new(c.Id, c.User2Id)}) ||
                     _storage[c.User1Id]
                         .Add(new ChatWithParticipantDto(c.Id, c.User2Id))) &&
                    (_storage
                         .TryAdd(c.User1Id, new HashSet<ChatWithParticipantDto> {new(c.Id, c.User2Id)}) ||
                     _storage[c.User1Id]
                         .Add(new ChatWithParticipantDto(c.Id, c.User2Id)));
            }
        }
        
        public void InitializeVoid(List<ChatDto> chats)
        {
            _mockDb = new List<ChatDto>();
            foreach (var c in chats)
            {
                if (_storage.ContainsKey(c.User1Id))
                    _storage[c.User1Id].Add(new ChatWithParticipantDto(c.Id, c.User2Id));
                else
                    _storage.TryAdd(c.User1Id, new HashSet<ChatWithParticipantDto> {new(c.Id, c.User2Id)});

                if (_storage.ContainsKey(c.User2Id))
                    _storage[c.User2Id].Add(new ChatWithParticipantDto(c.Id, c.User2Id));
                else
                    _storage.TryAdd(c.User2Id, new HashSet<ChatWithParticipantDto> {new(c.Id, c.User2Id)});
            }
        }

        public IEnumerable<ChatWithParticipantDto> GetChats(int userId)
            => _storage
                   .GetValueOrDefault(userId)?
                   .ToList()
               ?? new List<ChatWithParticipantDto>();

        public IEnumerable<int> GetChatIds(int userId)
            => _storage
                   .GetValueOrDefault(userId)?
                   .Select(c => c.ChatId)
                   .ToList()
               ?? new List<int>();

        public bool AddBool(ChatDto chat)
            => (_storage
                    .TryAdd(chat.User1Id, new HashSet<ChatWithParticipantDto> {new(chat.Id, chat.User2Id)}) ||
                _storage[chat.User1Id]
                    .Add(new ChatWithParticipantDto(chat.Id, chat.User2Id))) &&
               (_storage
                    .TryAdd(chat.User1Id, new HashSet<ChatWithParticipantDto> {new(chat.Id, chat.User2Id)}) ||
                _storage[chat.User1Id]
                    .Add(new ChatWithParticipantDto(chat.Id, chat.User2Id)));

        public void AddVoid(ChatDto chat)
        {
            if (_storage.ContainsKey(chat.User1Id))
                _storage[chat.User1Id].Add(new ChatWithParticipantDto(chat.Id, chat.User2Id));
            else
                _storage.TryAdd(chat.User1Id, new HashSet<ChatWithParticipantDto> {new(chat.Id, chat.User2Id)});

            if (_storage.ContainsKey(chat.User2Id))
                _storage[chat.User2Id].Add(new ChatWithParticipantDto(chat.Id, chat.User2Id));
            else
                _storage.TryAdd(chat.User2Id, new HashSet<ChatWithParticipantDto> {new(chat.Id, chat.User2Id)});
        }

        public bool UserHasChat(int userId, int chatId)
            => _storage
                   .GetValueOrDefault(userId)?
                   .Any(x => x.ChatId == chatId)
               ?? false;

        public bool UserHasChatWithParticipant(int userId, int participantId)
            => _storage
                   .GetValueOrDefault(userId)?
                   .Any(x => x.ParticipantId == participantId)
               ?? false;
    }
}