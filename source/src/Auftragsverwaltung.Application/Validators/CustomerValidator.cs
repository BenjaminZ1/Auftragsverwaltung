using System.Text.RegularExpressions;
using Auftragsverwaltung.Application.Dtos;
using FluentValidation;

namespace Auftragsverwaltung.Application.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Email).Must(x => x != null && IsEmailValid(x)).WithMessage("Email ungültig");
            RuleFor(x => x.Website).Must(x => x != null && IsWebsiteValid(x)).WithMessage("Website ungültig");
            RuleFor(x => x.CustomerNumber).Must(x => x != null && IsCustomerNumberValid(x)).WithMessage("Kundennummer ungültig");
            //RuleFor(x => x.Password).Must(x => x != null && IsPasswordValid(x)).WithMessage("Passwort ungültig");
        }

        private bool IsEmailValid(string email)
        {
            //deckt 99.9% ab
            Regex regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }

        private bool IsCustomerNumberValid(string customerNumber)
        {
            Regex regex = new Regex(@"^CU[0-9]{5}");
            Match match = regex.Match(customerNumber);
            if (match.Success)
                return true;
            return false;
        }

        private bool IsWebsiteValid(string website)
        {
            Regex regex = new Regex(@"^(http\:\/\/|https\:\/\/)?([a-z0-9][a-z0-9\-]*\.)+[a-z0-9][a-z0-9\-]*([/][a-zA-Z0-9\-\.\=&]*)*$");
            Match match = regex.Match(website);
            if (match.Success)
                return true;
            return false;
        }

        private bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"@^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
            Match match = regex.Match(password);
            if (match.Success)
                return true;
            return false;
        }
    }
}
