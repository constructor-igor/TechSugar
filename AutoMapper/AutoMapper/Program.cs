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
        public int Id { get; }

        public EngineOrder(string name, int id)
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
        public class ClientEngineResolver : IValueResolver<ClientOrder, EngineOrder, int>
        {
            public int Resolve(ClientOrder source, EngineOrder destination, int member, ResolutionContext context)
            {
                return Convert.ToInt32(source.Id);
            }
        }
        public class EngineClientResolver : IValueResolver<EngineOrder, ClientOrder, string>
        {
            public string Resolve(EngineOrder source, ClientOrder destination, string member, ResolutionContext context)
            {
                return source.Id.ToString();
            }
        }

        static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientOrder, EngineOrder>()
                    .ForMember(dest=>dest.Id, opt=> opt.ResolveUsing<ClientEngineResolver>());
                cfg.CreateMap<EngineOrder, ClientOrder>()
                    .ForMember(dest => dest.Id, opt => opt.ResolveUsing<EngineClientResolver>());
            });
            // or var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());

            ClientOrder clientOrder = new ClientOrder("Joe", "10");

            IMapper mapper = Mapper.Configuration.CreateMapper();
            var engineOrder = mapper.Map<EngineOrder>(clientOrder);

            Console.WriteLine("client order (original order) : {0}", clientOrder);
            Console.WriteLine("engine order (by client order): {0}", engineOrder);
            Console.WriteLine("client order (by engine order): {0}", mapper.Map<ClientOrder>(engineOrder));
        }
    }
}
