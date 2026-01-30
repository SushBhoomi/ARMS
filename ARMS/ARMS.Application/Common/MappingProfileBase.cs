using ARMS.Core;
using AutoMapper;

namespace ARMS.Application.Common
{
    public abstract class MappingProfileBase<TModel, TViewModel> : Profile
        where TModel : Entity
        where TViewModel : ViewModelBase<TModel>
    {
        public MappingProfileBase()
        {

        }
    }
}
