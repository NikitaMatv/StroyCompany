using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyCompany.Components
{
    public partial class Order
    {
        public string Color
            {
            get
            {
                if(IsCompl == 2)
                {
                    return "#bcf5bc";
                }
                else
                {
                    return "";
                }
            }
            }
    }
}