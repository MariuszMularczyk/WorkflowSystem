
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Data;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Application
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;


        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public object GetUserMessages(Guid userId)
        {
            List<Message> obj = _messageRepository.GetUserMessages(userId);
            List<MessageDto> obj2 = new List<MessageDto>();

            foreach (var message in obj)
            {
                obj2.Add(new MessageDto()
                {
                    Id = message.Id,
                    FromUserId = message.FromUserId,
                    ToUserId = message.ToUserId,
                    Subject = message.Subject,
                    Content = message.Content,
                    SentOn = message.SentOn,
                    MessageType = message.MessageType,
                    IsActive = message.IsActive,
                    IsRead = message.IsRead,
                    FromUser = new GetUserDataDto()
                    {
                        FirstName = message.FromUser.FirstName,
                        LastName = message.FromUser.FirstName,
                        Id = message.FromUser.Id,
                        DateOfBirth = message.FromUser.DateOfBirth,
                        Email = message.FromUser.Email,
                        UserType = message.FromUser.UserType,
                    },
                    ToUser = new GetUserDataDto()
                    {
                        FirstName = message.ToUser.FirstName,
                        LastName = message.ToUser.FirstName,
                        Id = message.ToUser.Id,
                        DateOfBirth = message.ToUser.DateOfBirth,
                        Email = message.ToUser.Email,
                        UserType = message.ToUser.UserType,
                    },

                });
            }
            return obj2;
        }

        public object GetUserSentMessages(Guid userId)
        {
            List<Message> obj = _messageRepository.GetUserSentMessages(userId);
            List<MessageDto> obj2 = new List<MessageDto>();

            foreach (var message in obj)
            {
                obj2.Add(new MessageDto()
                {
                    Id = message.Id,
                    FromUserId = message.FromUserId,
                    ToUserId = message.ToUserId,
                    Subject = message.Subject,
                    Content = message.Content,
                    SentOn = message.SentOn,
                    MessageType = message.MessageType,
                    IsActive = message.IsActive,
                    IsRead = message.IsRead,
                    FromUser = new GetUserDataDto() { 
                        FirstName = message.FromUser.FirstName,
                        LastName = message.FromUser.FirstName,
                        Id = message.FromUser.Id,
                        DateOfBirth = message.FromUser.DateOfBirth,
                        Email = message.FromUser.Email,
                        UserType = message.FromUser.UserType,
                    },
                    ToUser = new GetUserDataDto()
                    {
                        FirstName = message.ToUser.FirstName,
                        LastName = message.ToUser.FirstName,
                        Id = message.ToUser.Id,
                        DateOfBirth = message.ToUser.DateOfBirth,
                        Email = message.ToUser.Email,
                        UserType = message.ToUser.UserType,
                    },

                });
            }
            return obj2;
        }

        public Message GetMessageDetails(Guid id)
        {
            Message message = _messageRepository.GetMessageDetails(id);
            if (message == null)
            {
                throw new KeyNotFoundException();
            }

            return new Message();
        }

        public void AddMessage(SendMessageDto model)
        {
            Message message = new Message()
            {
                Content = model.Content,
                Subject = model.Subject,
                FromUserId = model.FromUserId,
                MessageType = model.MessageType,
                ToUserId = model.ToUserId.Value
            };
            message.SentOn = DateTime.Now;

            _messageRepository.AddMessage(message);

        }

        public void MarkMessageAsRead(Guid id)
        {
            Message userMessage = _messageRepository.GetMessageDetails(id);
            userMessage.IsRead = true;
            _messageRepository.UpdateMessage(userMessage);
        }

        public void DeleteMessage(Guid id)
        {
            Message userMessage = _messageRepository.GetMessageDetails(id);
            userMessage.IsActive = false;
            _messageRepository.UpdateMessage(userMessage);
        }

    }
}
