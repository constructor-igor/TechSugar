using System;
using AutoMapper;

namespace AutoMapperConsole
{
    public class ClientOrder
    {
        public string Name { get; }
        public string Id { get; }

        public ClientOrder(string name, string id)
        {
            Name = name;
            Id = id;
        }

        #region Overrides of Object
        public override string ToString()
        {
            return $"[ClientOrder] Name={Name}, Id={Id}";
        }
        #endregion
    }

    public class EngineOrder
    {
        public string Name { get; }
        public string Id { get; }

        public EngineOrder(string name, string id)
        {
            Name = name;
            Id = id;
        }
        #region Overrides of Object
        public override string ToString()
        {
            return $"[EngineOrder] Name={Name}, Id={Id}";
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientOrder, EngineOrder>();
                cfg.CreateMap<EngineOrder, ClientOrder>();
            });
            // or var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());

            ClientOrder clientOrder = new ClientOrder("Joe", "1s0");

            IMapper mapper = Mapper.Configuration.CreateMapper();
            var engineOrder = mapper.Map<EngineOrder>(clientOrder);

            Console.WriteLine("client order (original order) : {0}", clientOrder);
            Console.WriteLine("engine order (by client order): {0}", engineOrder);
            Console.WriteLine("client order (by engine order): {0}", mapper.Map<ClientOrder>(engineOrder));
        }
    }
}
