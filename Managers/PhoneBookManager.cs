using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook
{
    public class PhoneBookManager
    {
        private readonly IDatabaseService _databaseService;

        public PhoneBookManager(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool IsPhoneNumberAlreadyAddedToPhoneBook(string phoneNumber)
        {
            try
            {
                List<User> users = _databaseService.GetAllUsers();
                bool asw = users.Any(user => user.PhoneNumber == phoneNumber);
                return asw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}