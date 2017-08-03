using System;
using AutoMapper;

namespace AutoMapperConsole
{
    public class ClientOrder
    {
        public string Name { get; }

        public ClientOrder(string name)
        {
            Name = name;
        }

        #region Overrides of Object
        public override string ToString()
        {
            return $"Name={Name}";
        }
        #endregion
    }

    public class EngineOrder
    {
        public string Name { get; }

        public EngineOrder(string name)
        {
            Name = name;
        }
        #region Overrides of Object
        public override string ToString()
        {
            return $"Name={Name}";
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ClientOrder, EngineOrder>());
            // or var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());

            ClientOrder clientOrder = new ClientOrder("Joe");

            IMapper mapper = Mapper.Configuration.CreateMapper();
            var engineOrder = mapper.Map<ClientOrder>(clientOrder);

            Console.WriteLine("client order: {0}", clientOrder);
            Console.WriteLine("engine order: {0}", engineOrder);
        }
    }
}
