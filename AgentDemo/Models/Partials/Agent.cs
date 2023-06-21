using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentDemo.Models
{
    partial class Agent
    {
        public int? QuantityForYear
        {
            get
            {
                int Count = this.ProductSale.Sum(x=>x.ProductCount); return Count;
            }
        }
        public int? Sum
        {
            get
            {
                int Count = this.ProductSale.Sum(x=> x.ProductCount);
                int Sum = (int)this.ProductSale.Sum(x => x.Product.MinCostForAgent);
                int All = Count * Sum;
                return All;
            }
        }

        public string Procent
        {
            get
            {
                int Count = this.ProductSale.Sum(x => x.ProductCount);
                int Sum = (int)this.ProductSale.Sum(x => x.Product.MinCostForAgent);
                int All = Count * Sum;
                string color;
                if(All >=0 && All <= 1000)
                {
                    color = "0%";
                    return color;
                }
                else if(All >=1000 && All <= 5000)
                {
                    color = "5%";
                    return color;
                }
                else if(All >= 5000 && All <= 15000)
                {
                    color = "10%";
                    return color;
                }
                else if (All >= 15000 && All <= 50000)
                {
                    color = "20%";
                    return color;
                }
                else
                {
                    color = "25%";
                    return color;
                }
            }
        }

        public string Color
        {
            get
            {
                int Count = this.ProductSale.Sum(x => x.ProductCount);
                int Sum = (int)this.ProductSale.Sum(x => x.Product.MinCostForAgent);
                int All = Count * Sum;
                if(All > 50000)
                {
                    return "#91E668";
                }
                else
                {
                    return "#FDFDFD";
                }
            }
        }
    }
}
