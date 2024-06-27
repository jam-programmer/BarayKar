namespace Domain.Entities.Business
{
    public class BusinessPictureEntity : BaseEntity
    {
        public string? Image { set; get; }
        public string? Title { set; get; }
        public string? Alt { set; get; }

        #region Relation
        public Guid? BusinessId { set; get; }
        public BusinessEntity? Business { set; get; }
        #endregion
    }
}
