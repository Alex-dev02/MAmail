using MAmail.Dtos;
using MAmail.Entities;
using MAmail.Repositories;

namespace MAmail.Services
{
    public class EmailService
    {
        private UserService _userService;
        private EmailService _emailService;
        private RecipientService _recipientService;

        public EmailService(UserService userService, EmailService emailService, RecipientService recipientService)
        {
            _userService = userService;
            _emailService = emailService;
            _recipientService = recipientService;
        }

        public async void Send(int senderId, EmailSendRequestDto email)
        {
            List<User>? availableUsers = await _userService.GetUsersByEmail(email.RecipientsEmails);

            // no valid emails
            if (availableUsers == null)
                return;

            // register mail

            // create Recipient record for each user


        }
    }
}
