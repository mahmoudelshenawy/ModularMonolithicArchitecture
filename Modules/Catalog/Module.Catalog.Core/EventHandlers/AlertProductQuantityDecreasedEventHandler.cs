using MediatR;
using Microsoft.Extensions.Logging;
using Module.Catalog.Core.Events;
using Module.Users.Shared.UserApiInterfaces;
using Shared.Core.Common;
using Shared.Core.Interfaces;
using Shared.Models.Models;

namespace Module.Catalog.Core.EventHandlers
{
    internal class AlertProductQuantityDecreasedEventHandler : INotificationHandler<AlertProductQuantityDecreasedEvent>
    {
        private readonly ILogger<AlertProductQuantityDecreasedEventHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IUserPublicApi _userPublicApi;
        private readonly ICurrentUserService _currentUserService;

        public AlertProductQuantityDecreasedEventHandler(ILogger<AlertProductQuantityDecreasedEventHandler> logger,
            IEmailService emailService,
            IUserPublicApi userPublicApi,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _emailService = emailService;
            _userPublicApi = userPublicApi;
            _currentUserService = currentUserService;
        }

        public async Task Handle(AlertProductQuantityDecreasedEvent notification, CancellationToken cancellationToken)
        {
            //logging
            _logger.LogInformation("Product Catalog Module Domain Event: {DomainEvent}", notification.GetType().Name);
            _logger.LogInformation("units in stock are lower than or equal to the alert quantity " + notification._product.AlertQuantity);

            var Currentuser = await _userPublicApi.GetUserDetails(_currentUserService.userId);
            var Publisheruser = await _userPublicApi.GetUserDetails(notification._product.CreatedBy);
            if (Currentuser == null && !Currentuser.Roles.Contains(RolesNameConstants.Administrator))
                return;

            var userEmailOptions = new UserEmailOptions
            {
                Subject = "Alert Product Quantity",
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{user}}" , Currentuser.Username),
                    new KeyValuePair<string, string>("{{productName}}" , notification._product.Name),
                    new KeyValuePair<string, string>("{{productSku}}" , notification._product.Sku),
                    new KeyValuePair<string, string>("{{quantity}}" , notification._product.UnitsInStock.ToString()),
                    new KeyValuePair<string, string>("{{alertQuantity}}" , notification._product.AlertQuantity.ToString()),
                }
            };
            userEmailOptions.ToEmails.Add(Currentuser.Email);
            userEmailOptions.ToEmails.Add(Publisheruser.Email);
            //sendMail
            await _emailService.SendAlertQuantityToAdmin(userEmailOptions);
            //Send Notifications


            return;
        }
    }
}
