namespace Domain.Entities.Business
{
    public class BusinessPropertyEntity:BaseEntity
    {
        public string? Key { set; get; }
        public string? Value { set; get; }
        #region Relation
        public Guid? BusinessId { set; get; }
        public BusinessEntity? Business { set; get; }
        #endregion
    }
}
