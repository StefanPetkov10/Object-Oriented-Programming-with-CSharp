using _03.TelephonyWithAllFolders.Models.Interfaces;
using System;
using System.Linq;


namespace _03.TelephonyWithAllFolders.Models;

public class StationaryPhone : ICallable
{
    public string Call(string phoneNumber)
    {
        if (!ValidatePhoneNumber(phoneNumber))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Dialing... {phoneNumber}";
    }

    private bool ValidatePhoneNumber(string phoneNumber)
        => phoneNumber.All(c => char.IsDigit(c));
}