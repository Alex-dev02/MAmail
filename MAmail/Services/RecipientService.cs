using MAmail.Dtos;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class RecipientService
    {
        private RecipientRepository _recipientRepository;

        public RecipientService(RecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        public async Task Add(int emailId, int recipientId)
        {
            await _recipientRepository.Add(emailId, recipientId);
        }

        public async Task<List<int>> GetReceivedEmailIdsForRecipient(int recipientId)
        {
            return await _recipientRepository.GetReceivedEmailIdsForRecipient(recipientId);
        }
        public async Task<bool> HasAccessToEmail(int userId, int emailId)
        {
            return await _recipientRepository.HasAccessToEmail(userId, emailId);
        }

        public async Task<ActionInfo> MarkAsRead(int userId, int emailId)
        {
            return await _recipientRepository.MarkAsRead(userId, emailId);
        }

        public async Task<ActionInfo> ArchiveEmail(int userId, int emailId)
        {
            return await _recipientRepository.ArchiveEmail(userId, emailId);
        }

        public async Task<List<EmailHeaderDto>?> GetUnreadEmails(int userId, int pageNumber)
        {
            List<int> userEmails = await GetReceivedEmailIdsForRecipient(userId);

            if (userEmails == null || userEmails.Count == 0)
                return null;

            return await _recipientRepository.GetUnreadEmails(userEmails, pageNumber);
        }

        public async Task<List<EmailHeaderDto>?> GetArchivedEmails(int userId, int pageNumber)
        {
            List<int> userEmails = await _recipientRepository.GetArchivedEmailIdsForRecipient(userId);

            if (userEmails == null || userEmails.Count == 0)
                return null;

            return await _recipientRepository.GetArchivedEmails(userEmails, pageNumber);
        }

        public async Task<ActionInfo> DeleteEmail(int userId, int emailId)
        {
            return await _recipientRepository.DeleteEmail(userId, emailId);
        }
    }
}
