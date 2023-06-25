using MAmail.Data;
using MAmail.Dtos;
using MAmail.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Repositories
{
    public class RecipientRepository
    {
        private MAmailDBContext _db;
        private readonly int _pageSize = 5;

        public RecipientRepository(MAmailDBContext db)
        {
            _db = db;
        }

        public async Task Add(int emailId, int recipientId)
        {
            Recipient recipient = new Recipient()
            {
                IsRead = false,
                IsArchived = false,
                URecipientId = recipientId,
                EmailId = emailId
            };

            await _db.Recipients.AddAsync(recipient);
            await _db.SaveChangesAsync();
        }

        public async Task<List<int>> GetReceivedEmailIdsForRecipient(int recipientId)
        {
            return await _db.Recipients.Where(r => r.URecipientId == recipientId && r.IsArchived == false)
                .Select(r => r.EmailId).ToListAsync();
        }

        public async Task<List<int>> GetArchivedEmailIdsForRecipient(int recipientId)
        {
            return await _db.Recipients.Where(r => r.URecipientId == recipientId && r.IsArchived == true)
                .Select(r => r.EmailId).ToListAsync();
        }

        public async Task<bool> HasAccessToEmail(int userId, int emailId)
        {
            return await _db.Recipients.Include(r => r.Email)
                .AnyAsync(r => r.EmailId == emailId && (r.URecipientId == userId || r.Email.SenderId == userId));
        }

        public async Task<ActionInfo> MarkAsRead(int userId, int emailId)
        {
            var res = await _db.Recipients.FirstOrDefaultAsync(r => r.URecipientId == userId && r.EmailId == emailId);

            if (res == null)
                return new ActionInfo()
                {
                    Success = false,
                    Message = "Invalid mail id!"
                };

            res.IsRead = true;

            await _db.SaveChangesAsync();

            return new ActionInfo()
            {
                Success = true,
                Message = "Marked as read successfully!"
            };
        }

        public async Task<ActionInfo> ArchiveEmail(int userId, int emailId)
        {
            var res = await _db.Recipients.FirstOrDefaultAsync(r => r.URecipientId == userId && r.EmailId == emailId);

            if (res == null)
                return new ActionInfo()
                {
                    Success = false,
                    Message = "Invalid mail id!"
                };

            res.IsArchived = true;

            await _db.SaveChangesAsync();

            return new ActionInfo()
            {
                Success = true,
                Message = "Archived successfully!"
            };
        }

        public async Task<List<EmailHeaderDto>?> GetUnreadEmails(List<int> userEmails, int pageNumber)
        {
            return await _db.Recipients.Include(r => r.Email).Include(r => r.Email.Sender)
                .Where(r => userEmails.Contains(r.EmailId) && !r.IsRead)
                .Select(r => new EmailHeaderDto()
                {
                    Id = r.Id,
                    Subject = r.Email.Subject,
                    Email = r.Email.Sender.Email,
                    ReceivedAt = r.Email.TimeStamp
                }).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();
        }

        public async Task<List<EmailHeaderDto>?> GetArchivedEmails(List<int> userEmails, int pageNumber)
        {
            return await _db.Recipients.Include(r => r.Email).Include(r => r.Email.Sender)
                .Where(r => userEmails.Contains(r.EmailId) && r.IsArchived)
                .Select(r => new EmailHeaderDto()
                {
                    Id = r.Id,
                    Subject = r.Email.Subject,
                    Email = r.Email.Sender.Email,
                    ReceivedAt = r.Email.TimeStamp
                }).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToListAsync();
        }

        public async Task<ActionInfo> DeleteEmail(int userId, int emailId)
        {
            var res = await _db.Recipients.FirstOrDefaultAsync(r => r.URecipientId == userId && r.EmailId == emailId);

            if (res == null)
                return new ActionInfo()
                {
                    Success = false,
                    Message = "Invalid email id!"
                };

            _db.Recipients.Remove(res);
            await _db.SaveChangesAsync();

            return new ActionInfo()
            {
                Success = true,
                Message = "Mail deleted successfully!"
            };
        }
    }
}
