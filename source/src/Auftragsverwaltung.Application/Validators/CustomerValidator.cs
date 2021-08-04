using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Auftragsverwaltung.Application.Dtos;
using FluentValidation;

namespace Auftragsverwaltung.Application.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(x => x.Email).Must(IsEmailValid).WithMessage("Email ungültig");
        }

        //Implementieren Sie eine Validierung für Email-Adressen. Wenn Ihre Validierung nicht alle Fälle gemäss RFC 5322 abdeckt (weil z.B. das RegEx-Pattern für die letzten 1% Spezialfälle zu kompliziert würde) dokumentieren Sie, welche Fälle nicht abgedeckt sind.

        //from https://emailregex.com/

        private bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }




        //wenn create: Check Kundennummer
        //Die Adressnummer muss zwingend mit dem Präfix «CU» beginnen (CU=Customer). Anschliessend soll eine 5stellige Nummer folgen.



        //Check Website
        //Die Validierung für die Eingabe der Website soll folgende Formate zulassen:
        //www.google.com
        //http://www.google.com
        //https://www.google.com
        //google.com
        //Wahlweise soll eine Subdomain angegeben werden können(gilt für alle vier Formate) –
        //bspw.: https://policies.google.com
        //Die Adresse soll auch beliebig mit einem Pfad sowie Parameter ergänzt werden können(dies auch wieder für alle Formate) – bspw.: https://policies.google.com/technologies/voice?hl=de&gl=ch



        //Check Passwort
        //8 Zeichen, Zweingend einen Gross und einen Kleinbuchstaben, Zweingend eine Zahl


    }
}
