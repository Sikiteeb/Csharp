using System;
using System.Runtime.InteropServices;

namespace Diffie_Hellman
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Prime: 
            Console.WriteLine("Please input a prime: ");
            var prime = Console.ReadLine();
            var primeInt = checkInputUlong(prime);
            if(primeInt < 0) goto Prime;
            if (CheckPrime.CheckingPrime(primeInt)) Console.WriteLine("Thank you for entering a prime number!");
            else
            {
                Console.WriteLine("It seem like you have not entered a prime, please try again.");
                goto Prime;
            }
            Console.WriteLine("Please enter a generator value: ");
            Generator:
            var generator = Console.ReadLine();
            var generatorInt = checkInputUlong(generator);
            if(generatorInt < 0) goto Generator;
            Actor1:
            Console.WriteLine("Please enter a number for actor 1:");
            var act1 = Console.ReadLine();
            var act1Int = checkInputUlong(act1);
            if(act1Int < 0) goto Actor1;
            Actor2:
            Console.WriteLine("Please enter a number for actor 2:");
            var act2 = Console.ReadLine();
            var act2Int = checkInputUlong(act2);
            if(act2Int < 0) goto Actor2;
            var act1Pub = CalculatePublic(primeInt, generatorInt, act1Int);
            var act2Pub = CalculatePublic(primeInt, generatorInt, act2Int);
            Console.WriteLine("The public values for actor 1 is " + act1Pub);
            Console.WriteLine("The public values for actor 2 is " + act2Pub);
            Console.WriteLine("Actor 1 and Actor 2 will now exchange keys");
            var act1Priv = CalculatePrivate(act1Pub, act2Int, primeInt);
            var act2Priv = CalculatePrivate(act2Pub, act1Int, primeInt);
            Console.WriteLine("Actor 1 and Actor 2 calculate their respective keys");
            Console.WriteLine("Actor 1: " + act1Priv + ", Actor 2: " + act2Priv);
            if(act1Priv == act2Priv) Console.WriteLine("Seems like it all went well.");
            else Console.WriteLine("Oh no!");
        }

        static ulong checkInputUlong(string a)
        {
            try
            {
                var attempt = UInt64.Parse(a);
                return attempt;
            }
            catch (Exception e)
            {
                Console.WriteLine("Seems like you just tried to input something that is not a/an (positive) integer. Try again!");
                return Convert.ToUInt64(-1);
            }
        }
        
        static ulong CalculatePublic(ulong p, ulong g, ulong x)
        {
            ulong result = 0;
            result = Convert.ToUInt64(Math.Pow(g, x) % p);
            return result;
        }

        static ulong CalculatePrivate(ulong y, ulong a, ulong p)
        {
            ulong result = 0;
            result = Convert.ToUInt64(Math.Pow(y, a) % p);
            return result;
        }
    }
}
