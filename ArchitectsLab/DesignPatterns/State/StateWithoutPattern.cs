using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DesignPatterns.State
{

    [TestFixture]
    public class StateWithoutPatternTests
    {
        [Test]
        public void Sample()
        {
            var rentalMethods = new RentalMethods(9);

            rentalMethods.GetApplication();

            rentalMethods.CheckApplication();
            rentalMethods.RentApartment();
            rentalMethods.DispenseKeys();
        }
    }


    public enum State
    {
        FULLY_RENTED = 0,
        WAITING = 1,
        GOT_APPLICATION = 2,
        APARTMENT_RENTED = 3,
    }

    public class RentalMethods
    {
        private readonly Random _random;
        private Int32 _numberApartments;
        private State _state = State.WAITING;

        public RentalMethods(Int32 n)
        {
            _numberApartments = n;
            _random = new Random(System.DateTime.Now.Millisecond);
        }

        public void GetApplication()
        {
            switch (_state)
            {
                case State.FULLY_RENTED:
                    Console.WriteLine("Sorry, we're fully rented.");
                    break;
                case State.WAITING:
                    _state = State.GOT_APPLICATION;
                    Console.WriteLine("Thanks for the application.");
                    break;
                case State.GOT_APPLICATION:
                    Console.WriteLine("We already got your application.");
                    break;
                case State.APARTMENT_RENTED:
                    Console.WriteLine("Hang on, we'ra renting you an apartment.");
                    break;
            }
        }

        public void CheckApplication()
        {
            var yesNo = _random.Next() % 10;

            switch (_state)
            {
                case State.FULLY_RENTED:
                    Console.WriteLine("Sorry, we're fully rented.");
                    break;
                case State.WAITING:
                    Console.WriteLine("You have to submit an application.");
                    break;
                case State.GOT_APPLICATION:
                    if (yesNo > 4 && _numberApartments > 0)
                    {
                        Console.WriteLine("Congratulations, you were approved.");
                        _state = State.APARTMENT_RENTED;
                        RentApartment();
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you were not approved.");
                        _state = State.WAITING;
                    }
                    break;
                case State.APARTMENT_RENTED:
                    Console.WriteLine("Hang on, we'ra renting you an apartment.");
                    break;
            }
        }

        public void RentApartment()
        {
            switch (_state)
            {
                case State.FULLY_RENTED:
                    Console.WriteLine("Sorry, we're fully rented.");
                    break;
                case State.WAITING:
                    Console.WriteLine("You have to submit an application.");
                    break;
                case State.GOT_APPLICATION:
                    Console.WriteLine("You must have your application checked.");
                    break;
                case State.APARTMENT_RENTED:
                    Console.WriteLine("Renting you an apartment....");
                    _numberApartments--;
                    DispenseKeys();
                    break;
            }

        }

        public void DispenseKeys()
        {
            switch (_state)
            {
                case State.FULLY_RENTED:
                    Console.WriteLine("Sorry, we're fully rented.");
                    break;
                case State.WAITING:
                    Console.WriteLine("You have to submit an application.");
                    break;
                case State.GOT_APPLICATION:
                    Console.WriteLine("You must have your application checked.");
                    break;
                case State.APARTMENT_RENTED:
                    Console.WriteLine("Here are your keys!");
                    _state = State.WAITING;
                    break;
            }

        }
    }
}
