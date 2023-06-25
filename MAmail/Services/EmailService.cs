using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class EmailService
    {
        private UserService _userService;
        private EmailRepository _emailRepository;
        private RecipientService _recipientService;

        public EmailService(UserService userService, EmailRepository emailRepository, RecipientService recipientService)
        {
            _userService = userService;
            _emailRepository = emailRepository;
            _recipientService = recipientService;
        }

        public async Task Send(int senderId, EmailSendRequestDto email)
        {

            List<User>? availableUsers = await _userService.GetUsersByEmail(email.RecipientsEmails);

            // no valid emails
            if (availableUsers == null)
                return;

            // register mail
            int emailId = await _emailRepository.Add(senderId, email);

            // create Recipient record for each user
            List<Task> tasks = new List<Task>();
            foreach (User user in availableUsers)
            {
                tasks.Add(Task.Run(async () =>
                     {
                         await _recipientService.Add(emailId, user.Id);
                     }
                 ));
            }
            await Task.WhenAll(tasks);
        }
        
        public async Task<List<EmailHeaderDto>?> GetReceivedEmails(int userId, int pageNumber)
        {
            List<int> emailIdsToGet = await _recipientService.GetReceivedEmailIdsForRecipient(userId);

            if (emailIdsToGet == null || emailIdsToGet.Count == 0)
                return null;

            return await _emailRepository.GetReceivedEmails(emailIdsToGet, pageNumber);
        }

        public async Task<List<EmailHeaderDto>?> GetSentEmails(int userId, int pageNumber)
        {
            return await _emailRepository.GetSentEmails(userId, pageNumber);
        }

        public async Task<Email?> GetEmailById(int userId, int emailId)
        {
            // check if user has access to the specific email
            bool existsAndHasAccess = await _recipientService.HasAccessToEmail(userId, emailId);

            if (!existsAndHasAccess)
                return null;

            return await _emailRepository.GetEmailById(userId, emailId);
        }
    }
}
