using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("LogInfo")]
    public class LogInfo
    {
        [Key]
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginCreateTime { get; set; }
        public string LoginMsg { get; set; }
    }
}
