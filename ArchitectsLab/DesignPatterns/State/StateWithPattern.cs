using System;
using NUnit.Framework;

namespace DesignPatterns.State
{
    [TestFixture]
    public class StateWithPatternTests
    {
        [Test]
        public void Sample()
        {
            var automat = new Automat(9);

            automat.GotApplication();
            automat.CheckApplication();
            automat.RentApartment();
        }
    }

    public interface IAutomatState
    {
        String GotApplication();
        String CheckApplication();
        String RentApartment();
        String DispenseKeys();
    }
    public interface IAutomat
    {
        void GotApplication();
        void CheckApplication();
        void RentApartment();

        void SetState(IAutomatState s);
        IAutomatState GetWaitingState();
        IAutomatState GetGotApplicationState();
        IAutomatState GetApartmentRentedState();
        IAutomatState GetFullyRentedState();

        Int32 Count { get; set; }
    }

    public class Automat : IAutomat
    {
        private IAutomatState _waitingState;
        private IAutomatState _gotApplicationState;
        private IAutomatState _apartmentRentedState;
        private IAutomatState _fullyRentedState;
        private IAutomatState _state;
        private Int32 _count;

        public Automat(Int32 n)
        {
            _count = n;
            _waitingState = new WaitingState(this);
            _gotApplicationState = new GotApplicationState(this);
            _apartmentRentedState = new ApartmentRentedState(this);
            _fullyRentedState = new FullyRentedState(this);
            _state = _waitingState;
        }

        public void GotApplication()
        {
            Console.WriteLine(_state.GotApplication());
        }

        public void CheckApplication()
        {
            Console.WriteLine(_state.CheckApplication());
        }

        public void RentApartment()
        {
            Console.WriteLine(_state.RentApartment());
            Console.WriteLine(_state.DispenseKeys());
        }

        public void SetState(IAutomatState s) { _state = s; }

        public IAutomatState GetWaitingState() { return _waitingState; }

        public IAutomatState GetGotApplicationState() { return _gotApplicationState; }

        public IAutomatState GetApartmentRentedState() { return _apartmentRentedState; }

        public IAutomatState GetFullyRentedState() { return _fullyRentedState; }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }

    public class WaitingState : IAutomatState
    {
        private Automat _automat;

        public WaitingState(Automat automat)
        {
            _automat = automat;
        }

        public String GotApplication()
        {
            _automat.SetState(_automat.GetGotApplicationState());
            return "Thanks for the application.";
        }

        public String CheckApplication() { return "You have to submit an application."; }

        public String RentApartment() { return "You have to submit an application."; }

        public String DispenseKeys() { return "You have to submit an application."; }
    }

    public class GotApplicationState : IAutomatState
    {
        private Automat _automat;
        private readonly Random _random;

        public GotApplicationState(Automat automat)
        {
            _automat = automat;
            _random = new Random(System.DateTime.Now.Millisecond);
        }

        public String GotApplication() { return "We already got your application."; }

        public String CheckApplication()
        {
            var yesNo = _random.Next() % 10;

            if (yesNo > 4 && _automat.Count > 0)
            {
                _automat.SetState(_automat.GetApartmentRentedState());
                return "Congratulations, you were approved.";
            }
            else
            {
                _automat.SetState(_automat.GetWaitingState());
                return "Sorry, you were not approved.";
            }
        }

        public String RentApartment() { return "You must have your application checked."; }

        public String DispenseKeys() { return "You must have your application checked."; }
    }

    public class ApartmentRentedState : IAutomatState
    {
        private Automat _automat;

        public ApartmentRentedState(Automat automat)
        {
            _automat = automat;
        }

        public String GotApplication() { return "Hang on, we'ra renting you an apartmeny."; }

        public String CheckApplication() { return "Hang on, we'ra renting you an apartmeny."; }

        public String RentApartment()
        {
            _automat.Count = _automat.Count - 1;
            return "Renting you an apartment....";
        }

        public String DispenseKeys()
        {
            if (_automat.Count <= 0)
                _automat.SetState(_automat.GetFullyRentedState());
            else
                _automat.SetState(_automat.GetWaitingState());
            return "Here are your keys!";
        }
    }

    public class FullyRentedState : IAutomatState
    {
        private Automat _automat;

        public FullyRentedState(Automat automat)
        {
            _automat = automat;
        }

        public String GotApplication() { return "Sorry, we're fully rented."; }

        public String CheckApplication() { return "Sorry, we're fully rented."; }

        public String RentApartment() { return "Sorry, we're fully rented."; }

        public String DispenseKeys() { return "Sorry, we're fully rented."; }
    }
}
