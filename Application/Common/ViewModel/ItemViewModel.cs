
namespace Application.Common.ViewModel
{
    public class ItemViewModel<TTypeOne, TTypeTwo>
    {
        public TTypeOne Id { set; get; }
        public TTypeTwo? Title { set; get; }
    }
}
