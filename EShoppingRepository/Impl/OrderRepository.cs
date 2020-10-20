namespace EShoppingRepository.Impl
{
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;

    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }

        private readonly string DBString = null;
    }
}
