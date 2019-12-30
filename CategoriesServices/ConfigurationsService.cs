using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsService();
                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; }

        private ConfigurationsService()
        {

        }
        public Config GetConfig(string Key)
        {
            using (CBContext context = new CBContext())
            {
                //return context.Configurations.Where(x => x.Key == Key).FirstOrDefault();
                return context.Configurations.Find(Key);
            }
        }
        public int PageSize()
        {
            using (CBContext context = new CBContext())
            {

                var pageSizeConfig = context.Configurations.Find("PageSize");
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }
        public int ShopPageSize()
        {
            using (CBContext context = new CBContext())
            {

                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
