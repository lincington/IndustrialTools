using Prism.Mvvm;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialTools.Common.Models
{
    public class ComplexInfoModel : BindableBase
    {
        private DbType key = DbType.MySql;
        /// <summary>
        /// Key值
        /// </summary>
        public DbType Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private string text="";
        /// <summary>
        /// Text值
        /// </summary>
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }
}
