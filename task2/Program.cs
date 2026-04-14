using System;

namespace task2
{
    interface ICommandCentre
    {
        void RegisterAircraft(Aircraft aircraft);
        void RegisterRunway(Runway runway);
        bool RequestLanding(Aircraft aircraft);
    }
    class AirportControl : ICommandCentre
    {
        private Aircraft _aircraft;
        private Runway _runway;

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircraft = aircraft;
        }

        public void RegisterRunway(Runway runway)
        {
            _runway = runway;
        }

        public bool RequestLanding(Aircraft aircraft)
        {
            if (_runway.IsFree)
            {
                _runway.IsFree = false;
                return true;
            }
            return false;
        }
    }
    abstract class AirportComponent
    {
        protected ICommandCentre _mediator;

        public AirportComponent(ICommandCentre mediator)
        {
            _mediator = mediator;
        }
    }

    class Aircraft : AirportComponent
    {
        public string Name { get; }

        public Aircraft(string name, ICommandCentre mediator) : base(mediator)
        {
            Name = name;
        }

        public void Land()
        {
            Console.WriteLine($"{Name}: Запит на посадку");
            if (_mediator.RequestLanding(this))
            {
                Console.WriteLine($"{Name}: Посадка дозволена");
            }
            else
            {
                Console.WriteLine($"{Name}: Смуга зайнята");
            }
        }
    }

    class Runway : AirportComponent
    {
        public bool IsFree { get; set; }

        public Runway(ICommandCentre mediator) : base(mediator)
        {
            IsFree = true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AirportControl control = new AirportControl();

            Runway runway = new Runway(control);
            Aircraft plane1 = new Aircraft("Boeing", control);
            Aircraft plane2 = new Aircraft("Airbus", control);

            control.RegisterRunway(runway);
            control.RegisterAircraft(plane1);

            plane1.Land();
            plane2.Land();

            Console.WriteLine("\nСмуга звільнилася");
            runway.IsFree = true;
            plane2.Land();

            Console.ReadKey();
        }
    }
}