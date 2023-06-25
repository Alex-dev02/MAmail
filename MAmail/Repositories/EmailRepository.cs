using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Services;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Repositories
{
    public class EmailRepository
    {
        private MAmailDBContext _db;
        private readonly int _pageSize = 5;

        public EmailRepository(MAmailDBContext db)
        {
            _db = db;
        }

        public async Task<int> Add(int senderId, EmailSendRequestDto email)
        {
            Email e = new Email()
            {
                Subject = email.Subject,
                Body = email.Body,
                TimeStamp = DateTime.Now,
                SenderId = senderId
            };

            await _db.Emails.AddAsync(e);
            await _db.SaveChangesAsync();

            return e.Id;
        }

        public async Task<List<EmailHeaderDto>?> GetReceivedEmails(List<int> emailIds, int pageNumber)
        {
            return await _db.Emails.Include(e => e.Sender).Where(e => emailIds.Contains(e.Id)).Select(e => new EmailHeaderDto()
            {
                Id = e.Id,
                Subject = e.Subject,
                Email = e.Sender.Email,
                ReceivedAt = e.TimeStamp
            }).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();
        }

        public async Task<List<EmailHeaderDto>> GetSentEmails(int userId, int pageNumber)
        {
            return await _db.Emails.Where(e => e.SenderId == userId).Select(e => new EmailHeaderDto()
            {
                Id = e.Id,
                Subject = e.Subject,
                ReceivedAt = e.TimeStamp
            }).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();
        }

        public async Task<Email?> GetEmailById(int userId, int emailId)
        {
            return await _db.Emails.FirstOrDefaultAsync(e => e.Id == emailId);
        }
    }
}
