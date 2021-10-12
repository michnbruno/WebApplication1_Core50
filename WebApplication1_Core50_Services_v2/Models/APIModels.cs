using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace WebApplication1_Core50.Models
{

    public class APISendSql
    {

        public string p_sqlTable { set; get; }
     //   public string p_entity_sql { set; get; }
        public string p_sqlWhere { set; get; }
        public string p_sqlOrder { set; get; }

    }

    public class APISend
    {
        public string p_entity { set; get; }
        public string p_recId { set; get; }
        public dynamic record { set; get; }


    }

}
