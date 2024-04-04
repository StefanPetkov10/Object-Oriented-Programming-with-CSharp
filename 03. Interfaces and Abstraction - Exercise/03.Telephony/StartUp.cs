using System;
using Telephony.Models;
using Telephony.Models.Interfaces;



string[] phoneNumbers = Console.ReadLine().Split();
string[] urls = Console.ReadLine().Split();

ICallable phone;

foreach (var phoneNumber in phoneNumbers)
{
    if (phoneNumber.Length == 10)
    {
        phone = new Smartphone();
    }
    else
    {
        phone = new StationaryPhone();
    }

    try
    {
        Console.WriteLine(phone.Call(phoneNumber));
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

IBrowsable browser = new Smartphone();

foreach (var url in urls)
{
    try
    {
        Console.WriteLine(browser.Browse(url));
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}