
using Domain.Enum;

namespace Domain.Entities.Business
{
    public class BusinessTimeEntity:BaseEntity
    {
        public string? Time { set; get; }
        public DayEnum Day { set; get; }

        #region Relation
        public Guid? BusinessId { set; get; }
        public BusinessEntity? Business { set; get; }    
        #endregion
    }
}
