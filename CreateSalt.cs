using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Salt
{
     public class RasperryPi
    {
        public RasperryPi()
        {
            Server _s1 = new Server();
            System.Console.WriteLine($"MACADDRESS: {GetMacAddress()} ");
            System.Console.WriteLine("********************************");
            _s1.CreateSalt(HashMacAdress());

            
        }
        public int HashMacAdress()
        { 
            string _myMac = GetMacAddress();
            var hsMac = new HashSet<string>();
            hsMac.Add(_myMac);
            int _hashResult = 0;
            foreach(var item in hsMac)
            {
                System.Console.WriteLine($"GETTING HASHSET :{hsMac.GetHashCode()}");
                _hashResult = hsMac.GetHashCode();
            }      
        
            return _hashResult;
        }   
     
        public string GetMacAddress()
        {
            string _macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    _macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return _macAddresses;
        }
    }

    public class Server
    {
        public Server()
        {
           
        }
        public void CreateSalt(int hashedMac)
        { 
            //setting date => utcvalue 
            var _todayUtc = DateTime.UtcNow;
            System.Console.WriteLine("Day :"+ _todayUtc.Day);
            System.Console.WriteLine("Month:" +_todayUtc.Month);
            System.Console.WriteLine("Year : "+ _todayUtc.Year);
            int _todayRandom = _todayUtc.Day  +_todayUtc.Month+_todayUtc.Year;
            System.Console.WriteLine("-----------");
            System.Console.WriteLine(_todayRandom);
            Random _rd = new Random();
            int _randomizedToday = _rd.Next(0,_todayRandom);
            System.Console.WriteLine($"RANDOM VALUE BASED ON TODAY:");
            System.Console.WriteLine( _randomizedToday  );
            System.Console.WriteLine("RECEIVED HASHCODE:");
            System.Console.WriteLine(hashedMac);
            System.Console.WriteLine("RECEIVED(RASBERRY PHI:) + CREATED RANDOM :");
            Double _saltBasis = Convert.ToDouble(string.Format("{0}{1}", hashedMac, _randomizedToday));
            System.Console.WriteLine( _saltBasis );
            HashSet<Double> _salt = new HashSet<Double>();
            var result =  _salt.Add(_saltBasis);
            System.Console.WriteLine("SALT CREATED ?" + result);
            double _mySalt;
            foreach(var item in _salt)
            {
                _mySalt = item.GetHashCode();
                System.Console.WriteLine($"THIS IS YOUR SALT:{_mySalt} " );
                
            }   
        System.Console.WriteLine($"CHECKSUM CONTAINS ? {_saltBasis} ");  
            System.Console.WriteLine($"SALTBASIS PRESENT ? {_salt.Contains(_saltBasis)}");

        }

       
      

    }

    
    //client class
    public class Client
    {
         public Client()
         {
   
        
         
         }
    }
}