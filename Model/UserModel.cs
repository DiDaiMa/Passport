using System;

namespace Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public int Status { get; set; }
        public string BizCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateId { get; set; }
        public string TypeName { get; set; }
        public string TypeId { get; set; }
    }
}
